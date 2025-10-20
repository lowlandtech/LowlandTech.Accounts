namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding AccountPreferences test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultAccountPreferenceSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<AccountPreference>
        {
            new AccountPreference
            {
                Id = Guid.NewGuid(),
                Name = "Test AccountPreference 001",
                IsActive = true
            },
            new AccountPreference
            {
                Id = Guid.NewGuid(),
                Name = "Test AccountPreference 002",
                IsActive = true
            },
            new AccountPreference
            {
                Id = Guid.NewGuid(),
                Name = "Test AccountPreference 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
