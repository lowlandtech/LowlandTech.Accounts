
namespace LowlandTech.Accounts.Frontend.Components.Addresses;

// Actions & Events for Address
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record AddressInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record AddressInitEvent(IReadOnlyList<AddressDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record AddressSetItemsAction(IReadOnlyList<AddressDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record AddressSetPageAction(int Page) : IRequest<Unit>;
public sealed record AddressSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record AddressSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record AddressSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
