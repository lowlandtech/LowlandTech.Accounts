
namespace LowlandTech.Accounts.Frontend.Components.AuditEvents;

// INIT → calls API, then publishes InitEvent
public sealed class AuditEventInitHandler(IAuditEventApi api, IMediator mediator, AppState app)
    : IRequestHandler<AuditEventInitAction, Unit>
{
    private readonly IAuditEventApi _api = api;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(AuditEventInitAction request, CancellationToken ct)
    {
        var resp = await _api.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
        if (resp.IsSuccessStatusCode && resp.Content is not null)
        {
            await _mediator.Publish(new AuditEventInitEvent(resp.Content.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
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
public sealed class AuditEventSetItemsHandler(AuditEventPageState state)
    : IRequestHandler<AuditEventSetItemsAction, Unit>
{
    public Task<Unit> Handle(AuditEventSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class AuditEventSetPageHandler(AuditEventPageState state)
    : IRequestHandler<AuditEventSetPageAction, Unit>
{
    public Task<Unit> Handle(AuditEventSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class AuditEventSetPageSizeHandler(AuditEventPageState state)
    : IRequestHandler<AuditEventSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(AuditEventSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class AuditEventSetQueryHandler(AuditEventPageState state)
    : IRequestHandler<AuditEventSetQueryAction, Unit>
{
    public Task<Unit> Handle(AuditEventSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class AuditEventSetSortHandler(AuditEventPageState state)
    : IRequestHandler<AuditEventSetSortAction, Unit>
{
    public Task<Unit> Handle(AuditEventSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

