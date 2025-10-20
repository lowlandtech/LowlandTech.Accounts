namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding ApiKeys test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultApiKeySeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<ApiKey>
        {
            new ApiKey
            {
                Id = Guid.NewGuid(),
                Name = "Test ApiKey 001",
                IsActive = true
            },
            new ApiKey
            {
                Id = Guid.NewGuid(),
                Name = "Test ApiKey 002",
                IsActive = true
            },
            new ApiKey
            {
                Id = Guid.NewGuid(),
                Name = "Test ApiKey 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
