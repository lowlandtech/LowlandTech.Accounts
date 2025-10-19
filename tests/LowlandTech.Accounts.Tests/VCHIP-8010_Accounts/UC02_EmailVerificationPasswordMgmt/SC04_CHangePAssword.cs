
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC02_EmailVerificationPasswordMgmt;

[Scenario(
    specId: "VCHIP-8010-UC02-SC04",
    title: "CHANGE_PASSWORD",
    given: "an authenticated account",
    when: "they change password",
    then: "Event PasswordChanged")]
public sealed class SC04_WhenTestingCHangePAssword : WhenUsingDatabase<AccountsContext>
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
    {
        // Event PasswordChanged
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
