
namespace LowlandTech.Accounts.Frontend.Components.Sessions;

// INIT → calls API, then publishes InitEvent
public sealed class SessionInitHandler(ISessionApi api, IMediator mediator, AppState app)
    : IRequestHandler<SessionInitAction, Unit>
{
    private readonly ISessionApi _api = api;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(SessionInitAction request, CancellationToken ct)
    {
        var resp = await _api.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
        if (resp.IsSuccessStatusCode && resp.Content is not null)
        {
            await _mediator.Publish(new SessionInitEvent(resp.Content.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
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
public sealed class SessionSetItemsHandler(SessionPageState state)
    : IRequestHandler<SessionSetItemsAction, Unit>
{
    public Task<Unit> Handle(SessionSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class SessionSetPageHandler(SessionPageState state)
    : IRequestHandler<SessionSetPageAction, Unit>
{
    public Task<Unit> Handle(SessionSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class SessionSetPageSizeHandler(SessionPageState state)
    : IRequestHandler<SessionSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(SessionSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class SessionSetQueryHandler(SessionPageState state)
    : IRequestHandler<SessionSetQueryAction, Unit>
{
    public Task<Unit> Handle(SessionSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class SessionSetSortHandler(SessionPageState state)
    : IRequestHandler<SessionSetSortAction, Unit>
{
    public Task<Unit> Handle(SessionSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

