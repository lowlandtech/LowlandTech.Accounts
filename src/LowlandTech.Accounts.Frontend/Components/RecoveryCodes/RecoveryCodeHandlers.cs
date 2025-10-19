namespace LowlandTech.Accounts.Frontend.Components.RecoveryCodes;

// INIT → calls API, then publishes InitEvent
public sealed class RecoveryCodeInitHandler(RecoveryCodeApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<RecoveryCodeInitAction, Unit>
{
    private readonly RecoveryCodeApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(RecoveryCodeInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new RecoveryCodeInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load RecoveryCodes: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading RecoveryCodes: {ex.Message}");
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

