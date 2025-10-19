
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC01_AccountLifecycle;

[Scenario(
    specId: "VCHIP-8010-UC01-SC02",
    title: "LOGIN",
    given: "an existing registered account",
    when: "they login",
    then: "Event LoggedIn")]
public sealed class SC02_WhenTestingLOgin : WhenUsingDatabase<AccountsContext>
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
    {
        // Event LoggedIn
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
