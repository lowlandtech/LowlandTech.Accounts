
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC02_Email_verification_And_password_mgmt;


[Scenario(
    specId: "VCHIP-8010-UC02-SC01",
    title: "SEND_VERIFY_EMAIL",
    given: "an authenticated account",
    when: "they request verification",
    then: "Event EmailVerificationSent and State EmailVerificationToken")]
public sealed class VCHIP-8010_UC02_SC01Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they request verification


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event EmailVerificationSent", "UAC001")]
    public async Task Should_EventEmailVerificationSent()
    {        // Event EmailVerificationSent
        // TODO: Implement assertion
        await Task.CompletedTask;    }    [Fact]
    [Then("State EmailVerificationToken", "UAC002")]
    public async Task Should_StateEmailVerificationToken()
    {        // State EmailVerificationToken
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
