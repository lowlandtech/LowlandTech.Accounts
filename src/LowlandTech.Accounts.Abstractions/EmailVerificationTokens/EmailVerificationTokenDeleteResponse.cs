
namespace LowlandTech.Accounts.Abstractions.EmailVerificationTokens;

public sealed class DeleteEmailVerificationTokenResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

