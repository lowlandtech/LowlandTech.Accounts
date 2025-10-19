
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC01_Account_lifecycle;


[Scenario(
    specId: "VCHIP-8010-UC01-SC05",
    title: "ADD_ADDRESS",
    given: "an authenticated account",
    when: "they add a shipping address",
    then: "Event AddressAdded")]
public sealed class VCHIP-8010_UC01_SC05Steps : WhenUsingDatabase<FoundryContext>
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
    {        // Event AddressAdded
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
