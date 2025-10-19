
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC06_ExternalLogins;

[Scenario(
    specId: "VCHIP-8010-UC06-SC02",
    title: "REFRESH_EXTERNAL",
    given: "an expiring external login",
    when: "the token is refreshed",
    then: "Event ExternalLoginRefreshed")]
public sealed class SC02_WhenTestingREfreshEXternal : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an expiring external login


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // the token is refreshed


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event ExternalLoginRefreshed", "UAC002")]
    public async Task Should_EventExternalLoginRefreshed()
    {
        // Event ExternalLoginRefreshed
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
