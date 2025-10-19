
namespace LowlandTech.Accounts.Abstractions.RecoveryCodes;

public sealed class DeleteRecoveryCodeResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

