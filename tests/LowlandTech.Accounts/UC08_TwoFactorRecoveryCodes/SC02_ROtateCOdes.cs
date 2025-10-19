
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC08_Two_factor_And_recovery_codes;


[Scenario(
    specId: "VCHIP-8010-UC08-SC02",
    title: "ROTATE_CODES",
    given: "existing codes",
    when: "they rotate codes",
    then: "Event RecoveryCodesRotated")]
public sealed class VCHIP-8010_UC08_SC02Steps : WhenUsingDatabase<FoundryContext>
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
    {        // Event RecoveryCodesRotated
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
