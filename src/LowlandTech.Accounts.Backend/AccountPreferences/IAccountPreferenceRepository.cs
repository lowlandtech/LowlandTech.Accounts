namespace LowlandTech.Accounts.Backend.AccountPreferences;

public interface IAccountPreferenceRepository
{
    Task<AccountPreference?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<AccountPreference>> ListAsync(CancellationToken ct = default);
    Task<AccountPreference> AddAsync(AccountPreference entity, CancellationToken ct = default);
    Task UpdateAsync(AccountPreference entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
