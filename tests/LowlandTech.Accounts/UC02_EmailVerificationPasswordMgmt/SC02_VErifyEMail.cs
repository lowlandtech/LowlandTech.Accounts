
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC02_Email_verification_And_password_mgmt;


[Scenario(
    specId: "VCHIP-8010-UC02-SC02",
    title: "VERIFY_EMAIL",
    given: "a pending verification token for the account",
    when: "they verify email",
    then: "Event EmailVerified and State Account and State EmailVerificationToken")]
public sealed class VCHIP-8010_UC02_SC02Steps : WhenUsingDatabase<FoundryContext>
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
    {        // Event EmailVerified
        // TODO: Implement assertion
        await Task.CompletedTask;    }    [Fact]
    [Then("State Account", "UAC004")]
    public async Task Should_StateAccount()
    {        // State Account
        // TODO: Implement assertion
        await Task.CompletedTask;    }    [Fact]
    [Then("State EmailVerificationToken", "UAC005")]
    public async Task Should_StateEmailVerificationToken()
    {        // State EmailVerificationToken
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
