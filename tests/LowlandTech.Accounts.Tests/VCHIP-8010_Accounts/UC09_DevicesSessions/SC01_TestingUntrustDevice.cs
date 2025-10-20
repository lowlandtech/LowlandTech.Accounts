
namespace LowlandTech.Accounts.Tests.VCHIP_8010_Accounts.UC09_DevicesSessions;

[Scenario(
    specId: "VCHIP-8010-UC09-SC01",
    title: "UNTRUST_DEVICE",
    given: "a trusted device",
    when: "they untrust it",
    then: "Event DeviceUntrusted")]
public sealed class SC01_TestingUntrustDevice : WhenUsingDatabase<AccountsContext>
{
  // Test data fields

    protected override async Task GivenAsync()
    {
        // a trusted device


        await Task.CompletedTask;
    }

    protected override async Task WhenAsync()
    {
        // they untrust it


        await Task.CompletedTask;
    }

    [Fact]
    [Then("Event DeviceUntrusted", "UAC001")]
    public async Task Should_EventDeviceUntrusted()
    {
        // Event DeviceUntrusted
        // TODO: Implement assertion
        await Task.CompletedTask;
    }
}
