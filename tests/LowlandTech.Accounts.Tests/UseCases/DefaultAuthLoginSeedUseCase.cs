namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding AuthLogins test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultAuthLoginSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<AuthLogin>
        {
            new AuthLogin
            {
                Id = Guid.NewGuid(),
                Name = "Test AuthLogin 001",
                IsActive = true
            },
            new AuthLogin
            {
                Id = Guid.NewGuid(),
                Name = "Test AuthLogin 002",
                IsActive = true
            },
            new AuthLogin
            {
                Id = Guid.NewGuid(),
                Name = "Test AuthLogin 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
