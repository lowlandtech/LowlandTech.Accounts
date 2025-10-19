
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC05_Preferences;


[Scenario(
    specId: "VCHIP-8010-UC05-SC02",
    title: "REMOVE_PREFERENCE",
    given: "an authenticated account",
    when: "they remove a preference",
    then: "Event PreferenceRemoved")]
public sealed class VCHIP-8010_UC05_SC02Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they remove a preference


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event PreferenceRemoved", "UAC002")]
    public async Task Should_EventPreferenceRemoved()
    {        // Event PreferenceRemoved
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
