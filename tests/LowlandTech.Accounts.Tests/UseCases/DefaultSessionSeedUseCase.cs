namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding Sessions test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultSessionSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<Session>
        {
            new Session
            {
                Id = Guid.NewGuid(),
                Name = "Test Session 001",
                IsActive = true
            },
            new Session
            {
                Id = Guid.NewGuid(),
                Name = "Test Session 002",
                IsActive = true
            },
            new Session
            {
                Id = Guid.NewGuid(),
                Name = "Test Session 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
