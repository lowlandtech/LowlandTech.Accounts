namespace LowlandTech.Accounts.Backend.Accounts;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Account>> ListAsync(CancellationToken ct = default);
    Task<Account> AddAsync(Account entity, CancellationToken ct = default);
    Task UpdateAsync(Account entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
