
namespace LowlandTech.Accounts.Abstractions.Devices;

public sealed class DeleteDeviceResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

