namespace LowlandTech.Accounts.Backend.PasswordResetTokens;


public sealed class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly AccountsContext _db;

    public PasswordResetTokenRepository(AccountsContext db) => _db = db;

    public async Task<PasswordResetToken?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<PasswordResetToken>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<PasswordResetToken>> ListAsync(CancellationToken ct = default)
        => await _db.Set<PasswordResetToken>().AsNoTracking().ToListAsync(ct);

    public async Task<PasswordResetToken> AddAsync(PasswordResetToken entity, CancellationToken ct = default)
    {
        _db.Set<PasswordResetToken>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(PasswordResetToken entity, CancellationToken ct = default)
    {
        _db.Set<PasswordResetToken>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<PasswordResetToken>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
