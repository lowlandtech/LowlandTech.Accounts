namespace LowlandTech.Accounts.Backend.AccountPreferences;


public sealed class AccountPreferenceRepository : IAccountPreferenceRepository
{
    private readonly AccountsContext _db;

    public AccountPreferenceRepository(AccountsContext db) => _db = db;

    public async Task<AccountPreference?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<AccountPreference>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<AccountPreference>> ListAsync(CancellationToken ct = default)
        => await _db.Set<AccountPreference>().AsNoTracking().ToListAsync(ct);

    public async Task<AccountPreference> AddAsync(AccountPreference entity, CancellationToken ct = default)
    {
        _db.Set<AccountPreference>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(AccountPreference entity, CancellationToken ct = default)
    {
        _db.Set<AccountPreference>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<AccountPreference>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
