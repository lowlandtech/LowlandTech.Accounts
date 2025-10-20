namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding AuditEvents test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultAuditEventSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<AuditEvent>
        {
            new AuditEvent
            {
                Id = Guid.NewGuid(),
                Name = "Test AuditEvent 001",
                IsActive = true
            },
            new AuditEvent
            {
                Id = Guid.NewGuid(),
                Name = "Test AuditEvent 002",
                IsActive = true
            },
            new AuditEvent
            {
                Id = Guid.NewGuid(),
                Name = "Test AuditEvent 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
