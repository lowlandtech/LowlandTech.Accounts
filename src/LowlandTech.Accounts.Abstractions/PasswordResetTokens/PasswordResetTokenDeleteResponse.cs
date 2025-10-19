
namespace LowlandTech.Accounts.Abstractions.PasswordResetTokens;

public sealed class DeletePasswordResetTokenResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

