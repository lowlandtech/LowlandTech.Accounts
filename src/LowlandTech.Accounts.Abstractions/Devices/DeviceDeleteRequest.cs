
namespace LowlandTech.Accounts.Abstractions.Devices;

public sealed class DeleteDeviceRequest : IRequest<DeleteDeviceResponse>
{
    [Required]
    public Guid Id { get; set; }
}

