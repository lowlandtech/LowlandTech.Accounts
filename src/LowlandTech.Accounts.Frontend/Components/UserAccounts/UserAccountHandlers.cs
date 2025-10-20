namespace LowlandTech.Accounts.Frontend.Components.UserAccounts;

// INIT → calls API, then publishes InitEvent
public sealed class UserAccountInitHandler(UserAccountApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<UserAccountInitAction, Unit>
{
    private readonly UserAccountApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(UserAccountInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new UserAccountInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load UserAccounts: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading UserAccounts: {ex.Message}");
        }
        return Unit.Value;
    }
}

// Optimistic set-items (lets UI push a fresh list without refetching)
public sealed class UserAccountSetItemsHandler(UserAccountPageState state)
    : IRequestHandler<UserAccountSetItemsAction, Unit>
{
    public Task<Unit> Handle(UserAccountSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class UserAccountSetPageHandler(UserAccountPageState state)
    : IRequestHandler<UserAccountSetPageAction, Unit>
{
    public Task<Unit> Handle(UserAccountSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class UserAccountSetPageSizeHandler(UserAccountPageState state)
    : IRequestHandler<UserAccountSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(UserAccountSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class UserAccountSetQueryHandler(UserAccountPageState state)
    : IRequestHandler<UserAccountSetQueryAction, Unit>
{
    public Task<Unit> Handle(UserAccountSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class UserAccountSetSortHandler(UserAccountPageState state)
    : IRequestHandler<UserAccountSetSortAction, Unit>
{
    public Task<Unit> Handle(UserAccountSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

