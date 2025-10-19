
namespace LowlandTech.Accounts.Frontend.Components.Addresses;

// INIT → calls API, then publishes InitEvent
public sealed class AddressInitHandler(IAddressApi api, IMediator mediator, AppState app)
    : IRequestHandler<AddressInitAction, Unit>
{
    private readonly IAddressApi _api = api;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(AddressInitAction request, CancellationToken ct)
    {
        var resp = await _api.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
        if (resp.IsSuccessStatusCode && resp.Content is not null)
        {
            await _mediator.Publish(new AddressInitEvent(resp.Content.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            return Unit.Value;
        }
        try
        {
            var pd = await resp.Error?.GetContentAsAsync<ProblemDetails>();
            var title = pd?.Title ?? $"HTTP {(int)resp.StatusCode}";
            _app.ToastError(title + (string.IsNullOrWhiteSpace(pd?.Detail) ? string.Empty : $": {pd.Detail}"));
        }
        catch
        {
            _app.ToastError($"HTTP {(int)resp.StatusCode}: {resp.ReasonPhrase}");
        }
        return Unit.Value;
    }
}

// Optimistic set-items (lets UI push a fresh list without refetching)
public sealed class AddressSetItemsHandler(AddressPageState state)
    : IRequestHandler<AddressSetItemsAction, Unit>
{
    public Task<Unit> Handle(AddressSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class AddressSetPageHandler(AddressPageState state)
    : IRequestHandler<AddressSetPageAction, Unit>
{
    public Task<Unit> Handle(AddressSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class AddressSetPageSizeHandler(AddressPageState state)
    : IRequestHandler<AddressSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(AddressSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class AddressSetQueryHandler(AddressPageState state)
    : IRequestHandler<AddressSetQueryAction, Unit>
{
    public Task<Unit> Handle(AddressSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class AddressSetSortHandler(AddressPageState state)
    : IRequestHandler<AddressSetSortAction, Unit>
{
    public Task<Unit> Handle(AddressSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

