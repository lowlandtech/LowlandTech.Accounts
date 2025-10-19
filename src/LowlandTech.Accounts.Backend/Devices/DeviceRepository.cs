namespace LowlandTech.Accounts.Backend.Devices;


public sealed class DeviceRepository : IDeviceRepository
{
    private readonly AccountsContext _db;

    public DeviceRepository(AccountsContext db) => _db = db;

    public async Task<Device?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<Device>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Device>> ListAsync(CancellationToken ct = default)
        => await _db.Set<Device>().AsNoTracking().ToListAsync(ct);

    public async Task<Device> AddAsync(Device entity, CancellationToken ct = default)
    {
        _db.Set<Device>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(Device entity, CancellationToken ct = default)
    {
        _db.Set<Device>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<Device>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
