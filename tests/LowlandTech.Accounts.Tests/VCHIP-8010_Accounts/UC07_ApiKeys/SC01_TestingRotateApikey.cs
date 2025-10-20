
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC07_ApiKeys;

[Scenario(
    specId: "VCHIP-8010-UC07-SC01",
    title: "ROTATE_APIKEY",
    given: "an existing API key",
    when: "they rotate it",
    then: "Event ApiKeyRotated")]
public sealed class SC01_TestingRotateApikey : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an existing API key


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they rotate it


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event ApiKeyRotated", "UAC001")]
    public async Task Should_EventApiKeyRotated()
    {
        // Event ApiKeyRotated
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
