namespace LowlandTech.Accounts.Backend.AuditEvents;

public interface IAuditEventRepository
{
    Task<AuditEvent?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<AuditEvent>> ListAsync(CancellationToken ct = default);
    Task<AuditEvent> AddAsync(AuditEvent entity, CancellationToken ct = default);
    Task UpdateAsync(AuditEvent entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
