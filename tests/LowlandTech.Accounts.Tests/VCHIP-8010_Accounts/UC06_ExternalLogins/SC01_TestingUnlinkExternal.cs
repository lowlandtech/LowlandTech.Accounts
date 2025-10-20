
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC06_ExternalLogins;

[Scenario(
    specId: "VCHIP-8010-UC06-SC01",
    title: "UNLINK_EXTERNAL",
    given: "an account with a linked Google login",
    when: "they unlink it",
    then: "Event ExternalLoginUnlinked")]
public sealed class SC01_TestingUnlinkExternal : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an account with a linked Google login


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they unlink it


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event ExternalLoginUnlinked", "UAC001")]
    public async Task Should_EventExternalLoginUnlinked()
    {
        // Event ExternalLoginUnlinked
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
