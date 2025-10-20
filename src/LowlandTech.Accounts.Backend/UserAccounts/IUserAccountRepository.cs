namespace LowlandTech.Accounts.Backend.UserAccounts;

public interface IUserAccountRepository
{
    Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<UserAccount>> ListAsync(CancellationToken ct = default);
    Task<UserAccount> AddAsync(UserAccount entity, CancellationToken ct = default);
    Task UpdateAsync(UserAccount entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
