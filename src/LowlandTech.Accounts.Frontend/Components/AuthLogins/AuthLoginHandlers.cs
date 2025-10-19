namespace LowlandTech.Accounts.Frontend.Components.AuthLogins;

// INIT → calls API, then publishes InitEvent
public sealed class AuthLoginInitHandler(AuthLoginApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<AuthLoginInitAction, Unit>
{
    private readonly AuthLoginApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(AuthLoginInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new AuthLoginInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load AuthLogins: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading AuthLogins: {ex.Message}");
        }
        return Unit.Value;
    }
}

// Optimistic set-items (lets UI push a fresh list without refetching)
public sealed class AuthLoginSetItemsHandler(AuthLoginPageState state)
    : IRequestHandler<AuthLoginSetItemsAction, Unit>
{
    public Task<Unit> Handle(AuthLoginSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class AuthLoginSetPageHandler(AuthLoginPageState state)
    : IRequestHandler<AuthLoginSetPageAction, Unit>
{
    public Task<Unit> Handle(AuthLoginSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class AuthLoginSetPageSizeHandler(AuthLoginPageState state)
    : IRequestHandler<AuthLoginSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(AuthLoginSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class AuthLoginSetQueryHandler(AuthLoginPageState state)
    : IRequestHandler<AuthLoginSetQueryAction, Unit>
{
    public Task<Unit> Handle(AuthLoginSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class AuthLoginSetSortHandler(AuthLoginPageState state)
    : IRequestHandler<AuthLoginSetSortAction, Unit>
{
    public Task<Unit> Handle(AuthLoginSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

