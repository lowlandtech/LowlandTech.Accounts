
namespace LowlandTech.Accounts.Frontend.Components.RecoveryCodes;

// Actions & Events for RecoveryCode
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record RecoveryCodeInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record RecoveryCodeInitEvent(IReadOnlyList<RecoveryCodeDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record RecoveryCodeSetItemsAction(IReadOnlyList<RecoveryCodeDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record RecoveryCodeSetPageAction(int Page) : IRequest<Unit>;
public sealed record RecoveryCodeSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record RecoveryCodeSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record RecoveryCodeSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
