
namespace LowlandTech.Accounts.Abstractions.Sessions;

public sealed class DeleteSessionResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

