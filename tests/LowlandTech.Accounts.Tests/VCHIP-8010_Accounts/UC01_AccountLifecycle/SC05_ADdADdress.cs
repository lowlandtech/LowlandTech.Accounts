
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC01_AccountLifecycle;

[Scenario(
    specId: "VCHIP-8010-UC01-SC05",
    title: "ADD_ADDRESS",
    given: "an authenticated account",
    when: "they add a shipping address",
    then: "Event AddressAdded")]
public sealed class SC05_WhenTestingADdADdress : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they add a shipping address


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event AddressAdded", "UAC005")]
    public async Task Should_EventAddressAdded()
    {
        // Event AddressAdded
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
