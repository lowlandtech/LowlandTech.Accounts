
namespace LowlandTech.Accounts.Frontend.Infrastructure.State.Tables;

// Actions & Events for Device
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record DeviceInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record DeviceInitEvent(IReadOnlyList<DeviceDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record DeviceSetItemsAction(IReadOnlyList<DeviceDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record DeviceSetPageAction(int Page) : IRequest<Unit>;
public sealed record DeviceSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record DeviceSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record DeviceSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
