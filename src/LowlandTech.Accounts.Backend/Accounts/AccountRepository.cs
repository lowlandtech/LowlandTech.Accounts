namespace LowlandTech.Accounts.Backend.Accounts;


public sealed class AccountRepository : IAccountRepository
{
    private readonly AccountsContext _db;

    public AccountRepository(AccountsContext db) => _db = db;

    public async Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<Account>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Account>> ListAsync(CancellationToken ct = default)
        => await _db.Set<Account>().AsNoTracking().ToListAsync(ct);

    public async Task<Account> AddAsync(Account entity, CancellationToken ct = default)
    {
        _db.Set<Account>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(Account entity, CancellationToken ct = default)
    {
        _db.Set<Account>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<Account>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
