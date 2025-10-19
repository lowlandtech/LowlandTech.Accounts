namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;


public sealed class EmailVerificationTokenRepository : IEmailVerificationTokenRepository
{
    private readonly AccountsContext _db;

    public EmailVerificationTokenRepository(AccountsContext db) => _db = db;

    public async Task<EmailVerificationToken?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<EmailVerificationToken>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<EmailVerificationToken>> ListAsync(CancellationToken ct = default)
        => await _db.Set<EmailVerificationToken>().AsNoTracking().ToListAsync(ct);

    public async Task<EmailVerificationToken> AddAsync(EmailVerificationToken entity, CancellationToken ct = default)
    {
        _db.Set<EmailVerificationToken>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(EmailVerificationToken entity, CancellationToken ct = default)
    {
        _db.Set<EmailVerificationToken>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<EmailVerificationToken>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
