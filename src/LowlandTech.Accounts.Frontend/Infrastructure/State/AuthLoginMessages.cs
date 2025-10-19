
namespace LowlandTech.Accounts.Frontend.Infrastructure.State.Tables;

// Actions & Events for AuthLogin
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record AuthLoginInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record AuthLoginInitEvent(IReadOnlyList<AuthLoginDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record AuthLoginSetItemsAction(IReadOnlyList<AuthLoginDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record AuthLoginSetPageAction(int Page) : IRequest<Unit>;
public sealed record AuthLoginSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record AuthLoginSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record AuthLoginSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
