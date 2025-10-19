
namespace LowlandTech.Accounts.Frontend.Components.Accounts;

// INIT → calls API, then publishes InitEvent
public sealed class AccountInitHandler(IAccountApi api, IMediator mediator, AppState app)
    : IRequestHandler<AccountInitAction, Unit>
{
    private readonly IAccountApi _api = api;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(AccountInitAction request, CancellationToken ct)
    {
        var resp = await _api.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
        if (resp.IsSuccessStatusCode && resp.Content is not null)
        {
            await _mediator.Publish(new AccountInitEvent(resp.Content.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
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
public sealed class AccountSetItemsHandler(AccountPageState state)
    : IRequestHandler<AccountSetItemsAction, Unit>
{
    public Task<Unit> Handle(AccountSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class AccountSetPageHandler(AccountPageState state)
    : IRequestHandler<AccountSetPageAction, Unit>
{
    public Task<Unit> Handle(AccountSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class AccountSetPageSizeHandler(AccountPageState state)
    : IRequestHandler<AccountSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(AccountSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class AccountSetQueryHandler(AccountPageState state)
    : IRequestHandler<AccountSetQueryAction, Unit>
{
    public Task<Unit> Handle(AccountSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class AccountSetSortHandler(AccountPageState state)
    : IRequestHandler<AccountSetSortAction, Unit>
{
    public Task<Unit> Handle(AccountSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

