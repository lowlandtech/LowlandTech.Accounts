
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC08_TwoFactorRecoveryCodes;

[Scenario(
    specId: "VCHIP-8010-UC08-SC01",
    title: "GENERATE_CODES",
    given: "2FA is enabled",
    when: "they generate codes",
    then: "Event RecoveryCodesGenerated")]
public sealed class SC01_WhenTestingGEnerateCOdes : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // 2FA is enabled


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they generate codes


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event RecoveryCodesGenerated", "UAC001")]
    public async Task Should_EventRecoveryCodesGenerated()
    {
        // Event RecoveryCodesGenerated
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
