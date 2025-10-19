
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC05_Preferences;


[Scenario(
    specId: "VCHIP-8010-UC05-SC01",
    title: "SET_PREFERENCE",
    given: "an authenticated account",
    when: "they set a preference",
    then: "Event PreferenceSet")]
public sealed class VCHIP-8010_UC05_SC01Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they set a preference


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event PreferenceSet", "UAC001")]
    public async Task Should_EventPreferenceSet()
    {        // Event PreferenceSet
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
