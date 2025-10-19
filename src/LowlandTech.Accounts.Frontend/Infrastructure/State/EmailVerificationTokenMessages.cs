
namespace LowlandTech.Accounts.Frontend.Infrastructure.State.Tables;

// Actions & Events for EmailVerificationToken
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record EmailVerificationTokenInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record EmailVerificationTokenInitEvent(IReadOnlyList<EmailVerificationTokenDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record EmailVerificationTokenSetItemsAction(IReadOnlyList<EmailVerificationTokenDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record EmailVerificationTokenSetPageAction(int Page) : IRequest<Unit>;
public sealed record EmailVerificationTokenSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record EmailVerificationTokenSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record EmailVerificationTokenSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
