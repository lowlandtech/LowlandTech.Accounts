
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC04_Addresses;

[Scenario(
    specId: "VCHIP-8010-UC04-SC01",
    title: "UPDATE_ADDRESS",
    given: "an authenticated account with an address",
    when: "they update it",
    then: "Event AddressUpdated")]
public sealed class SC01_WhenTestingUPdateADdress : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an authenticated account with an address


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they update it


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event AddressUpdated", "UAC001")]
    public async Task Should_EventAddressUpdated()
    {
        // Event AddressUpdated
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
