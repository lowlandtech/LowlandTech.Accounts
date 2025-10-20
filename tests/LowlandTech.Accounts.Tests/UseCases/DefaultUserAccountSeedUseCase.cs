namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding UserAccounts test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultUserAccountSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<UserAccount>
        {
            new UserAccount
            {
                Id = Guid.NewGuid(),
                Name = "Test UserAccount 001",
                IsActive = true
            },
            new UserAccount
            {
                Id = Guid.NewGuid(),
                Name = "Test UserAccount 002",
                IsActive = true
            },
            new UserAccount
            {
                Id = Guid.NewGuid(),
                Name = "Test UserAccount 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
