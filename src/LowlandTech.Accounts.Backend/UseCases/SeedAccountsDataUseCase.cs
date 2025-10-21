
namespace LowlandTech.Accounts.Backend.UseCases;

/// <summary>
/// Seeds Accounts database with 100 rows of realistic fake data using Bogus.
/// </summary>
public sealed class SeedAccountsDataUseCase(AccountsContext db) : IUseCase
{

    public async Task SeedAsync()
    {
        await Task.CompletedTask;
    }
}

