namespace LowlandTech.Accounts.Backend.AuditEvents;


public sealed class AuditEventRepository : IAuditEventRepository
{
    private readonly AccountsContext _db;

    public AuditEventRepository(AccountsContext db) => _db = db;

    public async Task<AuditEvent?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<AuditEvent>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<AuditEvent>> ListAsync(CancellationToken ct = default)
        => await _db.Set<AuditEvent>().AsNoTracking().ToListAsync(ct);

    public async Task<AuditEvent> AddAsync(AuditEvent entity, CancellationToken ct = default)
    {
        _db.Set<AuditEvent>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(AuditEvent entity, CancellationToken ct = default)
    {
        _db.Set<AuditEvent>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<AuditEvent>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
