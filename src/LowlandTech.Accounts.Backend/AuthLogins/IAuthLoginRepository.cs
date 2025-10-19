namespace LowlandTech.Accounts.Backend.AuthLogins;

public interface IAuthLoginRepository
{
    Task<AuthLogin?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<AuthLogin>> ListAsync(CancellationToken ct = default);
    Task<AuthLogin> AddAsync(AuthLogin entity, CancellationToken ct = default);
    Task UpdateAsync(AuthLogin entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
