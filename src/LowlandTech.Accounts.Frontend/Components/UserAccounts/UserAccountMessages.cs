
namespace LowlandTech.Accounts.Frontend.Components.UserAccounts;

// Actions & Events for UserAccount
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record UserAccountInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record UserAccountInitEvent(IReadOnlyList<UserAccountDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record UserAccountSetItemsAction(IReadOnlyList<UserAccountDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record UserAccountSetPageAction(int Page) : IRequest<Unit>;
public sealed record UserAccountSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record UserAccountSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record UserAccountSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
