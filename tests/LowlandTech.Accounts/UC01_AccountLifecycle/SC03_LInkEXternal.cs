
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC01_Account_lifecycle;


[Scenario(
    specId: "VCHIP-8010-UC01-SC03",
    title: "LINK_EXTERNAL",
    given: "an existing account",
    when: "they link a Google login",
    then: "Event ExternalLoginLinked")]
public sealed class VCHIP-8010_UC01_SC03Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an existing account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they link a Google login


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event ExternalLoginLinked", "UAC003")]
    public async Task Should_EventExternalLoginLinked()
    {        // Event ExternalLoginLinked
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
