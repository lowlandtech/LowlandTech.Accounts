
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC10_AccountStatusCompliance;

[Scenario(
    specId: "VCHIP-8010-UC10-SC02",
    title: "EXPORT_DELETE",
    given: "an authenticated account",
    when: "they export then delete account",
    then: "Event AccountExportRequested and Event AccountDeleted")]
public sealed class SC02_WhenTestingEXportDElete : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they export then delete account


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event AccountExportRequested", "UAC003")]
    public async Task Should_EventAccountExportRequested()
    {
        // Event AccountExportRequested
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
    [Fact]
    [Then("Event AccountDeleted", "UAC004")]
    public async Task Should_EventAccountDeleted()
    {
        // Event AccountDeleted
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
