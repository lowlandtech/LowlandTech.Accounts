
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC07_APiKeys;

[Scenario(
    specId: "VCHIP-8010-UC07-SC02",
    title: "DISABLE_APIKEY",
    given: "a compromised API key",
    when: "they disable it",
    then: "Event ApiKeyDisabled")]
public sealed class SC02_WhenTestingDIsableAPikey : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // a compromised API key


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they disable it


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event ApiKeyDisabled", "UAC002")]
    public async Task Should_EventApiKeyDisabled()
    {
        // Event ApiKeyDisabled
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
