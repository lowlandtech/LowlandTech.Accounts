namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding PasswordResetTokens test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultPasswordResetTokenSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<PasswordResetToken>
        {
            new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                Name = "Test PasswordResetToken 001",
                IsActive = true
            },
            new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                Name = "Test PasswordResetToken 002",
                IsActive = true
            },
            new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                Name = "Test PasswordResetToken 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
