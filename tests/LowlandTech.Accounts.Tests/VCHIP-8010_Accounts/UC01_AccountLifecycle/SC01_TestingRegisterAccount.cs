
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC01_AccountLifecycle;

[Scenario(
    specId: "VCHIP-8010-UC01-SC01",
    title: "REGISTER_ACCOUNT",
    given: "a new user with email/password",
    when: "they register",
    then: "Event AccountRegistered")]
public sealed class SC01_TestingRegisterAccount : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // a new user with email/password


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they register


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event AccountRegistered", "UAC001")]
    public async Task Should_EventAccountRegistered()
    {
        // Event AccountRegistered
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
