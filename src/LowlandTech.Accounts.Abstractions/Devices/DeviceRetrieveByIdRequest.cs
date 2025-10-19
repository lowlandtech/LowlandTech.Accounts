
namespace LowlandTech.Accounts.Abstractions.Devices;

public sealed class RetrieveDeviceByIdRequest : IRequest<RetrieveDeviceByIdResponse>
{
    public Guid Id { get; set; }
}

