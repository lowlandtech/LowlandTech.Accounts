
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC02_EmailVerificationPasswordMgmt;

[Scenario(
    specId: "VCHIP-8010-UC02-SC03",
    title: "PASSWORD_RESET",
    given: "a user forgets password",
    when: "they request a reset",
    then: "Event PasswordResetRequested and Event PasswordResetCompleted")]
public sealed class SC03_WhenTestingPAsswordREset : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // a user forgets password


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they request a reset


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event PasswordResetRequested", "UAC003")]
    public async Task Should_EventPasswordResetRequested()
    {
        // Event PasswordResetRequested
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
    [Fact]
    [Then("Event PasswordResetCompleted", "UAC004")]
    public async Task Should_EventPasswordResetCompleted()
    {
        // Event PasswordResetCompleted
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
