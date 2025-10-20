namespace LowlandTech.Accounts.Backend.UserAccounts;


public sealed class UserAccountRepository : IUserAccountRepository
{
    private readonly AccountsContext _db;

    public UserAccountRepository(AccountsContext db) => _db = db;

    public async Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<UserAccount>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<UserAccount>> ListAsync(CancellationToken ct = default)
        => await _db.Set<UserAccount>().AsNoTracking().ToListAsync(ct);

    public async Task<UserAccount> AddAsync(UserAccount entity, CancellationToken ct = default)
    {
        _db.Set<UserAccount>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(UserAccount entity, CancellationToken ct = default)
    {
        _db.Set<UserAccount>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<UserAccount>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
