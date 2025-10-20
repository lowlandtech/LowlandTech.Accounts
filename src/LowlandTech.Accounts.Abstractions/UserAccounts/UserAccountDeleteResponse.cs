
namespace LowlandTech.Accounts.Abstractions.UserAccounts;

public sealed class DeleteUserAccountResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

