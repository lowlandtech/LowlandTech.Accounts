
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC01_AccountLifecycle;

[Scenario(
    specId: "VCHIP-8010-UC01-SC03",
    title: "LINK_EXTERNAL",
    given: "an existing account",
    when: "they link a Google login",
    then: "Event ExternalLoginLinked")]
public sealed class SC03_TestingLinkExternal : WhenUsingDatabase<AccountsContext>
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
    {
        // Event ExternalLoginLinked
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
