
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC02_EmailVerificationPasswordMgmt;

[Scenario(
    specId: "VCHIP-8010-UC02-SC02",
    title: "VERIFY_EMAIL",
    given: "a pending verification token for the account",
    when: "they verify email",
    then: "Event EmailVerified and State UserAccount and State EmailVerificationToken")]
public sealed class SC02_TestingVerifyEmail : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // a pending verification token for the account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they verify email


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event EmailVerified", "UAC003")]
    public async Task Should_EventEmailVerified()
    {
        // Event EmailVerified
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
    [Fact]
    [Then("State UserAccount", "UAC004")]
    public async Task Should_StateUserAccount()
    {
        // State UserAccount
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
    [Fact]
    [Then("State EmailVerificationToken", "UAC005")]
    public async Task Should_StateEmailVerificationToken()
    {
        // State EmailVerificationToken
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
