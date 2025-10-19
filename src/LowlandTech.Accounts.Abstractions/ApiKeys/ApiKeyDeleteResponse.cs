
namespace LowlandTech.Accounts.Abstractions.ApiKeys;

public sealed class DeleteApiKeyResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

