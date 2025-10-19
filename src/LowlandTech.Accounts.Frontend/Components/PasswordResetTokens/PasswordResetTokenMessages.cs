
namespace LowlandTech.Accounts.Frontend.Components.PasswordResetTokens;

// Actions & Events for PasswordResetToken
// These are table-prefixed to avoid collisions across plugins/tables.

// Query/List cycle
public sealed record PasswordResetTokenInitAction(int Page, int PageSize, string? Query, string? Sort, string? Dir) : IRequest<Unit>;
public sealed record PasswordResetTokenInitEvent(IReadOnlyList<PasswordResetTokenDto> Items, int Page, int PageSize, string? Query, string? Sort, string? Dir) : INotification;

// Optimistic set-items (optional; useful for local updates)
public sealed record PasswordResetTokenSetItemsAction(IReadOnlyList<PasswordResetTokenDto> Items) : IRequest<Unit>;

// Bindable setters → reducers (dispatched from State public setters)
public sealed record PasswordResetTokenSetPageAction(int Page) : IRequest<Unit>;
public sealed record PasswordResetTokenSetPageSizeAction(int PageSize) : IRequest<Unit>;
public sealed record PasswordResetTokenSetQueryAction(string? Query) : IRequest<Unit>;
public sealed record PasswordResetTokenSetSortAction(string? Sort, string? Dir) : IRequest<Unit>;
