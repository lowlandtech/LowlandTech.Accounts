
namespace LowlandTech.Accounts.Abstractions.PasswordResetTokens;

public sealed class RetrieveAllPasswordResetTokenResponse
{
    public IReadOnlyList<PasswordResetTokenDto> Items { get; init; } = Array.Empty<PasswordResetTokenDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

