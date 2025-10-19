
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC09_DevicesSessions;

[Scenario(
    specId: "VCHIP-8010-UC09-SC02",
    title: "REVOKE_ALL_SESSIONS",
    given: "an authenticated account",
    when: "they revoke all sessions",
    then: "Event SessionsRevoked")]
public sealed class SC02_WhenTestingREvokeALlSEssions : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // an authenticated account


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they revoke all sessions


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event SessionsRevoked", "UAC002")]
    public async Task Should_EventSessionsRevoked()
    {
        // Event SessionsRevoked
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
