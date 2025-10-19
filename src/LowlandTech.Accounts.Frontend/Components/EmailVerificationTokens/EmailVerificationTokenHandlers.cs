
namespace LowlandTech.Accounts.Frontend.Components.EmailVerificationTokens;

// INIT → calls API, then publishes InitEvent
public sealed class EmailVerificationTokenInitHandler(IEmailVerificationTokenApi api, IMediator mediator, AppState app)
    : IRequestHandler<EmailVerificationTokenInitAction, Unit>
{
    private readonly IEmailVerificationTokenApi _api = api;
    private readonly IMediator _mediator = mediator;
    private readonly AppState _app = app;

    public async Task<Unit> Handle(EmailVerificationTokenInitAction request, CancellationToken ct)
    {
        var resp = await _api.ListAsync(request.Page, request.PageSize, request.Query, request.Sort, request.Dir, ct);
        if (resp.IsSuccessStatusCode && resp.Content is not null)
        {
            await _mediator.Publish(new EmailVerificationTokenInitEvent(resp.Content.Items, request.Page, request.PageSize, request.Query, request.Sort, request.Dir), ct);
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
public sealed class EmailVerificationTokenSetItemsHandler(EmailVerificationTokenPageState state)
    : IRequestHandler<EmailVerificationTokenSetItemsAction, Unit>
{
    public Task<Unit> Handle(EmailVerificationTokenSetItemsAction request, CancellationToken ct)
    {
        // Use internal reducer via property set (Items has private set)
        state.GetType()
             .GetProperty("Items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
             ?.SetValue(state, request.Items);
        return Task.FromResult(Unit.Value);
    }
}

// Reducers for bindable setters
public sealed class EmailVerificationTokenSetPageHandler(EmailVerificationTokenPageState state)
    : IRequestHandler<EmailVerificationTokenSetPageAction, Unit>
{
    public Task<Unit> Handle(EmailVerificationTokenSetPageAction r, CancellationToken _) { state.ReducePage(r.Page); return Task.FromResult(Unit.Value); }
}

public sealed class EmailVerificationTokenSetPageSizeHandler(EmailVerificationTokenPageState state)
    : IRequestHandler<EmailVerificationTokenSetPageSizeAction, Unit>
{
    public Task<Unit> Handle(EmailVerificationTokenSetPageSizeAction r, CancellationToken _) { state.ReducePageSize(r.PageSize); return Task.FromResult(Unit.Value); }
}

public sealed class EmailVerificationTokenSetQueryHandler(EmailVerificationTokenPageState state)
    : IRequestHandler<EmailVerificationTokenSetQueryAction, Unit>
{
    public Task<Unit> Handle(EmailVerificationTokenSetQueryAction r, CancellationToken _) { state.ReduceQuery(r.Query); return Task.FromResult(Unit.Value); }
}

public sealed class EmailVerificationTokenSetSortHandler(EmailVerificationTokenPageState state)
    : IRequestHandler<EmailVerificationTokenSetSortAction, Unit>
{
    public Task<Unit> Handle(EmailVerificationTokenSetSortAction r, CancellationToken _) { state.ReduceSort(r.Sort, r.Dir); return Task.FromResult(Unit.Value); }
}

