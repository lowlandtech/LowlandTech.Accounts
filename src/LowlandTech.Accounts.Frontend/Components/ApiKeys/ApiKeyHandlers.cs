
namespace LowlandTech.Accounts.Frontend.Components.ApiKeys;

// INIT → calls API, then publishes InitEvent
public sealed class ApiKeyInitHandler(IApiKeyApi api, IMediator mediator, AppState app)
    : IRequestHandler<ApiKeyInitAction, Unit>
{
    private readonly IApiKeyApi _api = api;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(ApiKeyInitAction request, CancellationToken ct)
    {
        var resp = await _api.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
        if (resp.IsSuccessStatusCode && resp.Content is not null)
        {
            await _mediator.Publish(new ApiKeyInitEvent(resp.Content.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
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
public sealed class ApiKeySetItemsHandler(ApiKeyPageState state)
    : IRequestHandler<ApiKeySetItemsAction, Unit>
{
    public Task<Unit> Handle(ApiKeySetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class ApiKeySetPageHandler(ApiKeyPageState state)
    : IRequestHandler<ApiKeySetPageAction, Unit>
{
    public Task<Unit> Handle(ApiKeySetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class ApiKeySetPageSizeHandler(ApiKeyPageState state)
    : IRequestHandler<ApiKeySetPageSizeAction, Unit>
{
    public Task<Unit> Handle(ApiKeySetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class ApiKeySetQueryHandler(ApiKeyPageState state)
    : IRequestHandler<ApiKeySetQueryAction, Unit>
{
    public Task<Unit> Handle(ApiKeySetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class ApiKeySetSortHandler(ApiKeyPageState state)
    : IRequestHandler<ApiKeySetSortAction, Unit>
{
    public Task<Unit> Handle(ApiKeySetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

