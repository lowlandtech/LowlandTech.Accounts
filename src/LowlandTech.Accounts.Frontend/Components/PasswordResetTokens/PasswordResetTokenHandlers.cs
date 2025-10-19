namespace LowlandTech.Accounts.Frontend.Components.PasswordResetTokens;

// INIT → calls API, then publishes InitEvent
public sealed class PasswordResetTokenInitHandler(PasswordResetTokenApiService apiService, IMediator mediator, AppState app)
    : IRequestHandler<PasswordResetTokenInitAction, Unit>
{
    private readonly PasswordResetTokenApiService _apiService = apiService;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(PasswordResetTokenInitAction request, CancellationToken ct)
    {
        try
        {
            var result = await _apiService.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
            if (result is not null)
            {
                await _mediator.Publish(new PasswordResetTokenInitEvent(result.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
            }
            else
            {
                _app.ToastError("Failed to load PasswordResetTokens: No data returned");
            }
        }
        catch (HttpRequestException ex)
        {
            var statusCode = ex.StatusCode.HasValue ? $"HTTP {(int)ex.StatusCode.Value}" : "HTTP Error";
            _app.ToastError($"{statusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            _app.ToastError($"Error loading PasswordResetTokens: {ex.Message}");
        }
        return Unit.Value;
    }
}

// Optimistic set-items (lets UI push a fresh list without refetching)
public sealed class PasswordResetTokenSetItemsHandler(PasswordResetTokenPageState state)
    : IRequestHandler<PasswordResetTokenSetItemsAction, Unit>
{
    public Task<Unit> Handle(PasswordResetTokenSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class PasswordResetTokenSetPageHandler(PasswordResetTokenPageState state)
    : IRequestHandler<PasswordResetTokenSetPageAction, Unit>
{
    public Task<Unit> Handle(PasswordResetTokenSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class PasswordResetTokenSetPageSizeHandler(PasswordResetTokenPageState state)
    : IRequestHandler<PasswordResetTokenSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(PasswordResetTokenSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class PasswordResetTokenSetQueryHandler(PasswordResetTokenPageState state)
    : IRequestHandler<PasswordResetTokenSetQueryAction, Unit>
{
    public Task<Unit> Handle(PasswordResetTokenSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class PasswordResetTokenSetSortHandler(PasswordResetTokenPageState state)
    : IRequestHandler<PasswordResetTokenSetSortAction, Unit>
{
    public Task<Unit> Handle(PasswordResetTokenSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

