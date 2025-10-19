namespace LowlandTech.Accounts.Backend.AuthLogins;


public sealed class AuthLoginRepository : IAuthLoginRepository
{
    private readonly AccountsContext _db;

    public AuthLoginRepository(AccountsContext db) => _db = db;

    public async Task<AuthLogin?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<AuthLogin>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<AuthLogin>> ListAsync(CancellationToken ct = default)
        => await _db.Set<AuthLogin>().AsNoTracking().ToListAsync(ct);

    public async Task<AuthLogin> AddAsync(AuthLogin entity, CancellationToken ct = default)
    {
        _db.Set<AuthLogin>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(AuthLogin entity, CancellationToken ct = default)
    {
        _db.Set<AuthLogin>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<AuthLogin>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
