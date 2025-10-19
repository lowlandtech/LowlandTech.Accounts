namespace LowlandTech.Accounts.Frontend.Components.AccountPreferences;

// INIT → calls API, then publishes InitEvent
public sealed class AccountPreferenceInitHandler(AccountPreferenceApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<AccountPreferenceInitAction, Unit>
{
    private readonly AccountPreferenceApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(AccountPreferenceInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new AccountPreferenceInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load AccountPreferences: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading AccountPreferences: {ex.Message}");
        }
        return Unit.Value;
    }
}

// Optimistic set-items (lets UI push a fresh list without refetching)
public sealed class AccountPreferenceSetItemsHandler(AccountPreferencePageState state)
    : IRequestHandler<AccountPreferenceSetItemsAction, Unit>
{
    public Task<Unit> Handle(AccountPreferenceSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class AccountPreferenceSetPageHandler(AccountPreferencePageState state)
    : IRequestHandler<AccountPreferenceSetPageAction, Unit>
{
    public Task<Unit> Handle(AccountPreferenceSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class AccountPreferenceSetPageSizeHandler(AccountPreferencePageState state)
    : IRequestHandler<AccountPreferenceSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(AccountPreferenceSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class AccountPreferenceSetQueryHandler(AccountPreferencePageState state)
    : IRequestHandler<AccountPreferenceSetQueryAction, Unit>
{
    public Task<Unit> Handle(AccountPreferenceSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class AccountPreferenceSetSortHandler(AccountPreferencePageState state)
    : IRequestHandler<AccountPreferenceSetSortAction, Unit>
{
    public Task<Unit> Handle(AccountPreferenceSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

