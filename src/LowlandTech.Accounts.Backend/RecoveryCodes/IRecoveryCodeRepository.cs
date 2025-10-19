namespace LowlandTech.Accounts.Backend.RecoveryCodes;

public interface IRecoveryCodeRepository
{
    Task<RecoveryCode?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<RecoveryCode>> ListAsync(CancellationToken ct = default);
    Task<RecoveryCode> AddAsync(RecoveryCode entity, CancellationToken ct = default);
    Task UpdateAsync(RecoveryCode entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
