namespace LowlandTech.Accounts.Backend.Sessions;

public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Session>> ListAsync(CancellationToken ct = default);
    Task<Session> AddAsync(Session entity, CancellationToken ct = default);
    Task UpdateAsync(Session entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
