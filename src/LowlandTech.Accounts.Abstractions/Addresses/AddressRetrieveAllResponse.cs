
namespace LowlandTech.Accounts.Abstractions.Addresses;

public sealed class RetrieveAllAddressResponse
{
    public IReadOnlyList<AddressDto> Items { get; init; } = Array.Empty<AddressDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

