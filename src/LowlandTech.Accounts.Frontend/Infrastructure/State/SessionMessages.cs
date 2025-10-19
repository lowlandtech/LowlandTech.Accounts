
namespace LowlandTech.Accounts.Frontend.Infrastructure.State.Tables;

// Actions & Events for Session
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record SessionInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record SessionInitEvent(IReadOnlyList<SessionDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record SessionSetItemsAction(IReadOnlyList<SessionDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record SessionSetPageAction(int Page) : IRequest<Unit>;
public sealed record SessionSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record SessionSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record SessionSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
