
namespace LowlandTech.Accounts.Abstractions.Accounts;

public sealed class DeleteAccountResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

