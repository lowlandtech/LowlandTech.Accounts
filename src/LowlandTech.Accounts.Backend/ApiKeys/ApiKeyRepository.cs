namespace LowlandTech.Accounts.Backend.ApiKeys;


public sealed class ApiKeyRepository : IApiKeyRepository
{
    private readonly AccountsContext _db;

    public ApiKeyRepository(AccountsContext db) => _db = db;

    public async Task<ApiKey?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Set<ApiKey>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<ApiKey>> ListAsync(CancellationToken ct = default)
        => await _db.Set<ApiKey>().AsNoTracking().ToListAsync(ct);

    public async Task<ApiKey> AddAsync(ApiKey entity, CancellationToken ct = default)
    {
        _db.Set<ApiKey>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task UpdateAsync(ApiKey entity, CancellationToken ct = default)
    {
        _db.Set<ApiKey>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var set = _db.Set<ApiKey>();
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
