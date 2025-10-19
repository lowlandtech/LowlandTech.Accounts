
namespace LowlandTech.Accounts.Frontend.Infrastructure.State.Tables;

// Actions & Events for AccountPreference
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record AccountPreferenceInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record AccountPreferenceInitEvent(IReadOnlyList<AccountPreferenceDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record AccountPreferenceSetItemsAction(IReadOnlyList<AccountPreferenceDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record AccountPreferenceSetPageAction(int Page) : IRequest<Unit>;
public sealed record AccountPreferenceSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record AccountPreferenceSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record AccountPreferenceSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
