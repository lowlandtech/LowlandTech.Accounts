namespace LowlandTech.Accounts.Backend.ApiKeys;

public interface IApiKeyRepository
{
    Task<ApiKey?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<ApiKey>> ListAsync(CancellationToken ct = default);
    Task<ApiKey> AddAsync(ApiKey entity, CancellationToken ct = default);
    Task UpdateAsync(ApiKey entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
