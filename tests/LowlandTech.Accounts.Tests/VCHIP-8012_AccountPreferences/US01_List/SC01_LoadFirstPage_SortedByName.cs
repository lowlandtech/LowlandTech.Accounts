namespace LowlandTech.Accounts.Tests.VCHIP_8012_AccountPreferences.US01_List;

[Scenario(
    specId: "VCHIP-8012-US01-SC01",
    title: "Load first page sorted by Name",
    given: "Given the API has AccountPreferences seeded",
    when:  "When I GET /api/accountpreferences?skip=0&take=100",
    then:  "Then results are ordered by Name and returned as the first page")]
public sealed class SC01_LoadFirstPage_SortedByName : WhenTestingForAsync<AppFactory<Program>>
{
    private HttpClient _client = null!;

    protected override AppFactory<Program> For() => new AppFactory<Program>();
    protected override async Task GivenAsync() => await Sut.Db.SeedAsync<DefaultAccountPreferenceSeedUseCase>();
    protected override Task WhenAsync()
    {
        _client = Sut.CreateClient();
        return Task.CompletedTask;
    }

    [Fact]
    [Then("It calls GET /api/accountpreferences?skip=0&take=100", "UAC001")]
    public async Task Calls_Endpoint_With_Default_Paging()
    {
        var res = await _client.GetAsync("/api/accountpreferences?skip=0&take=100");
        res.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    [Then("Results are ordered by Name", "UAC002")]
    public async Task Results_Are_Sorted_By_Name()
    {
        var items = await _client.GetFromJsonAsync<List<AccountPreference>>("/api/accountpreferences?skip=0&take=100");
        items.ShouldNotBeNull();
        items.Count.ShouldBeGreaterThanOrEqualTo(1);

        // Verify the sequence is sorted by Name
        var sorted = items.OrderBy(x => x.Name).ToList();
        items.ShouldBe(sorted);
    }

    [Fact]
    [Then("The table renders the returned AccountPreferences", "UAC003")]
    public async Task Returns_First_Page_Items()
    {
        var items = await _client.GetFromJsonAsync<List<AccountPreference>>("/api/accountpreferences?skip=0&take=100");
        items.ShouldNotBeNull();
        items.Count.ShouldBeGreaterThanOrEqualTo(1);
    }
}
