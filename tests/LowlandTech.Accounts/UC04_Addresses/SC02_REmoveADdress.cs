
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC04_Addresses;


[Scenario(
    specId: "VCHIP-8010-UC04-SC02",
    title: "REMOVE_ADDRESS",
    given: "an authenticated account",
    when: "they remove an address",
    then: "Event AddressRemoved")]
public sealed class VCHIP-8010_UC04_SC02Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they remove an address


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event AddressRemoved", "UAC002")]
    public async Task Should_EventAddressRemoved()
    {        // Event AddressRemoved
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
