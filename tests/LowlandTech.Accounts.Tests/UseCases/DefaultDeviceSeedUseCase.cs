namespace LowlandTech.Accounts.Tests.UseCases;

using LowlandTech.Accounts.Domain;

/// <summary>
/// Default use case for seeding Devices test data.
/// Seeds basic records with Id, Name, and IsActive fields.
/// </summary>
public class DefaultDeviceSeedUseCase(AccountsContext db) : IUseCase
{
    public async Task SeedAsync()
    {
        var items = new List<Device>
        {
            new Device
            {
                Id = Guid.NewGuid(),
                Name = "Test Device 001",
                IsActive = true
            },
            new Device
            {
                Id = Guid.NewGuid(),
                Name = "Test Device 002",
                IsActive = true
            },
            new Device
            {
                Id = Guid.NewGuid(),
                Name = "Test Device 003",
                IsActive = false
            }
        };

        await db.Upsert(items).SaveChangesAsync();
    }
}
