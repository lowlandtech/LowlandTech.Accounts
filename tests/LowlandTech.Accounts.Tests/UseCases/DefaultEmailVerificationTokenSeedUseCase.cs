namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding EmailVerificationTokens test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultEmailVerificationTokenSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<EmailVerificationToken>
        {
            new EmailVerificationToken
            {
                Id = Guid.NewGuid(),
                Name = "Test EmailVerificationToken 001",
                IsActive = true
            },
            new EmailVerificationToken
            {
                Id = Guid.NewGuid(),
                Name = "Test EmailVerificationToken 002",
                IsActive = true
            },
            new EmailVerificationToken
            {
                Id = Guid.NewGuid(),
                Name = "Test EmailVerificationToken 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
