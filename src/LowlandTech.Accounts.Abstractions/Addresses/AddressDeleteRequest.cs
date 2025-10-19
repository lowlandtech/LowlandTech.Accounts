
namespace LowlandTech.Accounts.Abstractions.Addresses;

public sealed class DeleteAddressRequest : IRequest<DeleteAddressResponse>
{
    [Required]
    public Guid Id { get; set; }
}

