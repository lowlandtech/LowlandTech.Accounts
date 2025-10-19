
namespace LowlandTech.Accounts.Frontend.Components.RecoveryCodes;

// INIT → calls API, then publishes InitEvent
public sealed class RecoveryCodeInitHandler(IRecoveryCodeApi api, IMediator mediator, AppState app)
    : IRequestHandler<RecoveryCodeInitAction, Unit>
{
    private readonly IRecoveryCodeApi _api = api;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(RecoveryCodeInitAction request, CancellationToken ct)
    {
        var resp = await _api.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
        if (resp.IsSuccessStatusCode && resp.Content is not null)
        {
            await _mediator.Publish(new RecoveryCodeInitEvent(resp.Content.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
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
public sealed class RecoveryCodeSetItemsHandler(RecoveryCodePageState state)
    : IRequestHandler<RecoveryCodeSetItemsAction, Unit>
{
    public Task<Unit> Handle(RecoveryCodeSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class RecoveryCodeSetPageHandler(RecoveryCodePageState state)
    : IRequestHandler<RecoveryCodeSetPageAction, Unit>
{
    public Task<Unit> Handle(RecoveryCodeSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class RecoveryCodeSetPageSizeHandler(RecoveryCodePageState state)
    : IRequestHandler<RecoveryCodeSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(RecoveryCodeSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class RecoveryCodeSetQueryHandler(RecoveryCodePageState state)
    : IRequestHandler<RecoveryCodeSetQueryAction, Unit>
{
    public Task<Unit> Handle(RecoveryCodeSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class RecoveryCodeSetSortHandler(RecoveryCodePageState state)
    : IRequestHandler<RecoveryCodeSetSortAction, Unit>
{
    public Task<Unit> Handle(RecoveryCodeSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

