
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC01_AccountLifecycle;

[Scenario(
    specId: "VCHIP-8010-UC01-SC07",
    title: "SECURITY_2FA",
    given: "an authenticated account",
    when: "they enable two-factor",
    then: "Event TwoFactorEnabled")]
public sealed class SC07_TestingSecurity2Fa : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they enable two-factor


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event TwoFactorEnabled", "UAC007")]
    public async Task Should_EventTwoFactorEnabled()
    {
        // Event TwoFactorEnabled
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
