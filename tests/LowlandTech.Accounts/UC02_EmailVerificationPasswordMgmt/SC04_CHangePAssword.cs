
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC02_Email_verification_And_password_mgmt;


[Scenario(
    specId: "VCHIP-8010-UC02-SC04",
    title: "CHANGE_PASSWORD",
    given: "an authenticated account",
    when: "they change password",
    then: "Event PasswordChanged")]
public sealed class VCHIP-8010_UC02_SC04Steps : WhenUsingDatabase<FoundryContext>
{
    // Test data fields
    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they change password


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event PasswordChanged", "UAC005")]
    public async Task Should_EventPasswordChanged()
    {        // Event PasswordChanged
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
