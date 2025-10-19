
namespace LowlandTech.Accounts.Frontend.Components.AuditEvents;

// Actions & Events for AuditEvent
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record AuditEventInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record AuditEventInitEvent(IReadOnlyList<AuditEventDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record AuditEventSetItemsAction(IReadOnlyList<AuditEventDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record AuditEventSetPageAction(int Page) : IRequest<Unit>;
public sealed record AuditEventSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record AuditEventSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record AuditEventSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
