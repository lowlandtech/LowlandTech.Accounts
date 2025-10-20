
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC01_AccountLifecycle;

[Scenario(
    specId: "VCHIP-8010-UC01-SC06",
    title: "API_KEYS",
    given: "an authenticated account",
    when: "they generate an API key",
    then: "Event ApiKeyCreated")]
public sealed class SC06_TestingApiKeys : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they generate an API key


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event ApiKeyCreated", "UAC006")]
    public async Task Should_EventApiKeyCreated()
    {
        // Event ApiKeyCreated
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
