
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC10_Account_status_And_compliance;


[Scenario(
    specId: "VCHIP-8010-UC10-SC01",
    title: "DEACTIVATE_REACTIVATE",
    given: "an active account",
    when: "they deactivate then reactivate",
    then: "Event AccountDeactivated and Event AccountReactivated")]
public sealed class VCHIP-8010_UC10_SC01Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an active account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they deactivate then reactivate


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event AccountDeactivated", "UAC001")]
    public async Task Should_EventAccountDeactivated()
    {        // Event AccountDeactivated
        // TODO: Implement assertion
        await Task.CompletedTask;    }    [Fact]
    [Then("Event AccountReactivated", "UAC002")]
    public async Task Should_EventAccountReactivated()
    {        // Event AccountReactivated
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
