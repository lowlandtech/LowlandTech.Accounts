
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC09_Devices_And_sessions;


[Scenario(
    specId: "VCHIP-8010-UC09-SC02",
    title: "REVOKE_ALL_SESSIONS",
    given: "an authenticated account",
    when: "they revoke all sessions",
    then: "Event SessionsRevoked")]
public sealed class VCHIP-8010_UC09_SC02Steps : WhenUsingDatabase<FoundryContext>
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
    {        // Event SessionsRevoked
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
