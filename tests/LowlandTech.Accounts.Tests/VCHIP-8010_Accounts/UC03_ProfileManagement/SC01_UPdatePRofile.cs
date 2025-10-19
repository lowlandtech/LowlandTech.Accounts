
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC03_ProfileManagement;

[Scenario(
    specId: "VCHIP-8010-UC03-SC01",
    title: "UPDATE_PROFILE",
    given: "an authenticated account",
    when: "they update profile fields",
    then: "Event ProfileUpdated")]
public sealed class SC01_WhenTestingUPdatePRofile : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they update profile fields


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event ProfileUpdated", "UAC001")]
    public async Task Should_EventProfileUpdated()
    {
        // Event ProfileUpdated
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
