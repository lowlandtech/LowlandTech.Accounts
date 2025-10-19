namespace LowlandTech.Accounts.Frontend.Components.ApiKeys;

// INIT → calls API, then publishes InitEvent
public sealed class ApiKeyInitHandler(ApiKeyApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<ApiKeyInitAction, Unit>
{
    private readonly ApiKeyApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(ApiKeyInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new ApiKeyInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load ApiKeys: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading ApiKeys: {ex.Message}");
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

