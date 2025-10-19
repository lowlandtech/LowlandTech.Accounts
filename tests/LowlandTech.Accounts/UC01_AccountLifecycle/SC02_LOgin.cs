
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC01_Account_lifecycle;


[Scenario(
    specId: "VCHIP-8010-UC01-SC02",
    title: "LOGIN",
    given: "an existing registered account",
    when: "they login",
    then: "Event LoggedIn")]
public sealed class VCHIP-8010_UC01_SC02Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an existing registered account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they login


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event LoggedIn", "UAC002")]
    public async Task Should_EventLoggedIn()
    {        // Event LoggedIn
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
