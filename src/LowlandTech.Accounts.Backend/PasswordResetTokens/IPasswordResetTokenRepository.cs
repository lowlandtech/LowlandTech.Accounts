namespace LowlandTech.Accounts.Backend.PasswordResetTokens;

public interface IPasswordResetTokenRepository
{
    Task<PasswordResetToken?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<PasswordResetToken>> ListAsync(CancellationToken ct = default);
    Task<PasswordResetToken> AddAsync(PasswordResetToken entity, CancellationToken ct = default);
    Task UpdateAsync(PasswordResetToken entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
