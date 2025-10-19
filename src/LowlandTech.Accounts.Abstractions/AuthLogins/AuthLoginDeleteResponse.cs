
namespace LowlandTech.Accounts.Abstractions.AuthLogins;

public sealed class DeleteAuthLoginResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

