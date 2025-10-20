namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding Addresses test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultAddressSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<Address>
        {
            new Address
            {
                Id = Guid.NewGuid(),
                Name = "Test Address 001",
                IsActive = true
            },
            new Address
            {
                Id = Guid.NewGuid(),
                Name = "Test Address 002",
                IsActive = true
            },
            new Address
            {
                Id = Guid.NewGuid(),
                Name = "Test Address 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
