
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC06_External_logins;


[Scenario(
    specId: "VCHIP-8010-UC06-SC01",
    title: "UNLINK_EXTERNAL",
    given: "an account with a linked Google login",
    when: "they unlink it",
    then: "Event ExternalLoginUnlinked")]
public sealed class VCHIP-8010_UC06_SC01Steps : WhenUsingDatabase<FoundryContext>
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
    {        // Event ExternalLoginUnlinked
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
