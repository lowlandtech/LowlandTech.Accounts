
namespace LowlandTech.Accounts.Abstractions.Devices;

public sealed class RetrieveAllDeviceResponse
{
    public IReadOnlyList<DeviceDto> Items { get; init; } = Array.Empty<DeviceDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

