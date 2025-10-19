namespace LowlandTech.Accounts.Frontend.Components.Addresses;

// INIT → calls API, then publishes InitEvent
public sealed class AddressInitHandler(AddressApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<AddressInitAction, Unit>
{
    private readonly AddressApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(AddressInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new AddressInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load Addresses: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading Addresses: {ex.Message}");
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

