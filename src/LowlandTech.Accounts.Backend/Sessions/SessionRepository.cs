namespace LowlandTech.Accounts.Backend.Sessions;


public sealed class SessionRepository : ISessionRepository
{
    private readonly AccountsContext _db;

    public SessionRepository(AccountsContext db) => _db = db;

    public async Task<Session?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<Session>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Session>> ListAsync(CancellationToken ct = default)
        => await _db.Set<Session>().AsNoTracking().ToListAsync(ct);

    public async Task<Session> AddAsync(Session entity, CancellationToken ct = default)
    {
        _db.Set<Session>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(Session entity, CancellationToken ct = default)
    {
        _db.Set<Session>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<Session>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
