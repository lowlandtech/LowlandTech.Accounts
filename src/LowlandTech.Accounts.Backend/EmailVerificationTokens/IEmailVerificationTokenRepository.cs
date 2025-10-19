namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;

public interface IEmailVerificationTokenRepository
{
    Task<EmailVerificationToken?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<EmailVerificationToken>> ListAsync(CancellationToken ct = default);
    Task<EmailVerificationToken> AddAsync(EmailVerificationToken entity, CancellationToken ct = default);
    Task UpdateAsync(EmailVerificationToken entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
