
namespace LowlandTech.Tests.VCHIP_8010_VCHIP_8010.UC09_Devices_And_sessions;


[Scenario(
    specId: "VCHIP-8010-UC09-SC01",
    title: "UNTRUST_DEVICE",
    given: "a trusted device",
    when: "they untrust it",
    then: "Event DeviceUntrusted")]
public sealed class VCHIP-8010_UC09_SC01Steps : WhenUsingDatabase<FoundryContext>
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
    {        // Event DeviceUntrusted
        // TODO: Implement assertion
        await Task.CompletedTask;    }}
