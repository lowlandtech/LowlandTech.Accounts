
namespace LowlandTech.Accounts.Abstractions.Addresses;

public sealed class DeleteAddressResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

