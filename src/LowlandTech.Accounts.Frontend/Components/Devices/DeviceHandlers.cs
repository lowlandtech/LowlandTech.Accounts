namespace LowlandTech.Accounts.Frontend.Components.Devices;

// INIT → calls API, then publishes InitEvent
public sealed class DeviceInitHandler(DeviceApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<DeviceInitAction, Unit>
{
    private readonly DeviceApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(DeviceInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new DeviceInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load Devices: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading Devices: {ex.Message}");
        }
        return Unit.Value;
    }
}

// Optimistic set-items (lets UI push a fresh list without refetching)
public sealed class DeviceSetItemsHandler(DevicePageState state)
    : IRequestHandler<DeviceSetItemsAction, Unit>
{
    public Task<Unit> Handle(DeviceSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class DeviceSetPageHandler(DevicePageState state)
    : IRequestHandler<DeviceSetPageAction, Unit>
{
    public Task<Unit> Handle(DeviceSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class DeviceSetPageSizeHandler(DevicePageState state)
    : IRequestHandler<DeviceSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(DeviceSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class DeviceSetQueryHandler(DevicePageState state)
    : IRequestHandler<DeviceSetQueryAction, Unit>
{
    public Task<Unit> Handle(DeviceSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class DeviceSetSortHandler(DevicePageState state)
    : IRequestHandler<DeviceSetSortAction, Unit>
{
    public Task<Unit> Handle(DeviceSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

