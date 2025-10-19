
namespace LowlandTech.Accounts.Frontend.Components.Accounts;

// Actions & Events for Account
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record AccountInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record AccountInitEvent(IReadOnlyList<AccountDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record AccountSetItemsAction(IReadOnlyList<AccountDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record AccountSetPageAction(int Page) : IRequest<Unit>;
public sealed record AccountSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record AccountSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record AccountSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
