
namespace LowlandTech.Accounts.Frontend.Components.ApiKeys;

// Actions & Events for ApiKey
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record ApiKeyInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record ApiKeyInitEvent(IReadOnlyList<ApiKeyDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record ApiKeySetItemsAction(IReadOnlyList<ApiKeyDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record ApiKeySetPageAction(int Page) : IRequest<Unit>;
public sealed record ApiKeySetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record ApiKeySetQueryAction(string? Query) : IRequest<Unit>;
public sealed record ApiKeySetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
