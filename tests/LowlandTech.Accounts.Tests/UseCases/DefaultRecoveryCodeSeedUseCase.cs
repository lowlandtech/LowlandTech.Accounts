namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding RecoveryCodes test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultRecoveryCodeSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<RecoveryCode>
        {
            new RecoveryCode
            {
                Id = Guid.NewGuid(),
                Name = "Test RecoveryCode 001",
                IsActive = true
            },
            new RecoveryCode
            {
                Id = Guid.NewGuid(),
                Name = "Test RecoveryCode 002",
                IsActive = true
            },
            new RecoveryCode
            {
                Id = Guid.NewGuid(),
                Name = "Test RecoveryCode 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
