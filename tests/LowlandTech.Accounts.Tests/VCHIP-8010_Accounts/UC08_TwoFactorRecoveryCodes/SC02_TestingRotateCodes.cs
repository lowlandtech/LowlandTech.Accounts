
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC08_TwoFactorRecoveryCodes;

[Scenario(
    specId: "VCHIP-8010-UC08-SC02",
    title: "ROTATE_CODES",
    given: "existing codes",
    when: "they rotate codes",
    then: "Event RecoveryCodesRotated")]
public sealed class SC02_TestingRotateCodes : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // existing codes


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they rotate codes


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event RecoveryCodesRotated", "UAC002")]
    public async Task Should_EventRecoveryCodesRotated()
    {
        // Event RecoveryCodesRotated
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
