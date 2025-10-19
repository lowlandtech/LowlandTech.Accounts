namespace LowlandTech.Accounts.Backend.RecoveryCodes;


public sealed class RecoveryCodeRepository : IRecoveryCodeRepository
{
    private readonly AccountsContext _db;

    public RecoveryCodeRepository(AccountsContext db) => _db = db;

    public async Task<RecoveryCode?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<RecoveryCode>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<RecoveryCode>> ListAsync(CancellationToken ct = default)
        => await _db.Set<RecoveryCode>().AsNoTracking().ToListAsync(ct);

    public async Task<RecoveryCode> AddAsync(RecoveryCode entity, CancellationToken ct = default)
    {
        _db.Set<RecoveryCode>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(RecoveryCode entity, CancellationToken ct = default)
    {
        _db.Set<RecoveryCode>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<RecoveryCode>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
