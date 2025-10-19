namespace LowlandTech.Accounts.Backend.Addresses;


public sealed class AddressRepository : IAddressRepository
{
    private readonly AccountsContext _db;

    public AddressRepository(AccountsContext db) => _db = db;

    public async Task<Address?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<Address>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Address>> ListAsync(CancellationToken ct = default)
        => await _db.Set<Address>().AsNoTracking().ToListAsync(ct);

    public async Task<Address> AddAsync(Address entity, CancellationToken ct = default)
    {
        _db.Set<Address>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(Address entity, CancellationToken ct = default)
    {
        _db.Set<Address>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<Address>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
