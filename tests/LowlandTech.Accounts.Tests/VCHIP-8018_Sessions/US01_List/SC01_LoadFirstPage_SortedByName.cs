namespace LowlandTech.Accounts.Tests.VCHIP_8018_Sessions.US01_List;

[Scenario(
    specId: "VCHIP-8018-US01-SC01",
    title: "Load first page sorted by Name",
    given: "Given the API has Sessions seeded",
    when:  "When I GET /api/sessions?skip=0&take=100",
    then:  "Then results are ordered by Name and returned as the first page")]
public sealed class SC01_LoadFirstPage_SortedByName : WhenTestingForAsync<AppFactory<Program>>
{
    private HttpClient _client = null!;

    protected override AppFactory<Program> For() => new AppFactory<Program>();
    protected override async Task GivenAsync() => await Sut.Db.SeedAsync<DefaultSessionSeedUseCase>();
    protected override Task WhenAsync()
    {
        _client = Sut.CreateClient();
        return Task.CompletedTask;
    }

    [Fact]
    [Then("It calls GET /api/sessions?skip=0&take=100", "UAC001")]
    public async Task Calls_Endpoint_With_Default_Paging()
    {
        var res = await _client.GetAsync("/api/sessions?skip=0&take=100");
        res.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    [Then("Results are ordered by Name", "UAC002")]
    public async Task Results_Are_Sorted_By_Name()
    {
        var items = await _client.GetFromJsonAsync<List<Session>>("/api/sessions?skip=0&take=100");
        items.ShouldNotBeNull();
        items.Count.ShouldBeGreaterThanOrEqualTo(1);

        // Verify the sequence is sorted by Name
        var sorted = items.OrderBy(x => x.Name).ToList();
        items.ShouldBe(sorted);
    }

    [Fact]
    [Then("The table renders the returned Sessions", "UAC003")]
    public async Task Returns_First_Page_Items()
    {
        var items = await _client.GetFromJsonAsync<List<Session>>("/api/sessions?skip=0&take=100");
        items.ShouldNotBeNull();
        items.Count.ShouldBeGreaterThanOrEqualTo(1);
    }
}
