namespace LowlandTech.Accounts.Backend.Devices;

public interface IDeviceRepository
{
    Task<Device?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Device>> ListAsync(CancellationToken ct = default);
    Task<Device> AddAsync(Device entity, CancellationToken ct = default);
    Task UpdateAsync(Device entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
