namespace LowlandTech.Accounts.Tests.VCHIP_8019_EmailVerificationTokens.US01_List;

[Scenario(
    specId: "VCHIP-8019-US01-SC03",
    title: "Filter by property",
    given: "Given the API has EmailVerificationTokens with filterable properties",
    when:  "When I GET /api/emailverificationtokens?skip=0&take=100&filterProperty=filterValue",
    then:  "Then only EmailVerificationTokens matching the filter are returned")]
public sealed class SC03_FilterByProperty : WhenTestingForAsync<AppFactory<Program>>
{
    private HttpClient _client = null!;

    protected override AppFactory<Program> For() => new AppFactory<Program>();
    protected override async Task GivenAsync() => await Sut.Db.SeedAsync<DefaultEmailVerificationTokenSeedUseCase>();
    protected override Task WhenAsync()
    {
        _client = Sut.CreateClient();
        return Task.CompletedTask;
    }

    [Fact]
    [Then("It calls GET /api/emailverificationtokens with the filter parameter", "UAC006")]
    public async Task Calls_Endpoint_With_Filter()
    {
        // TODO: Customize filter property and value for your table
        var res = await _client.GetAsync("/api/emailverificationtokens?skip=0&take=100&filterProperty=filterValue");
        res.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    [Then("Only matching EmailVerificationTokens are shown", "UAC007")]
    public async Task Returns_Only_Matching_Items()
    {
        // TODO: Customize filter property and value for your table
        var items = await _client.GetFromJsonAsync<List<EmailVerificationToken>>("/api/emailverificationtokens?skip=0&take=100&filterProperty=filterValue");
        items.ShouldNotBeNull();
        items.Count.ShouldBeGreaterThanOrEqualTo(0);
        // TODO: Add assertion to verify all items match the filter criteria
        // Example: items.ShouldAllBe(i => i.PropertyName == "filterValue");
    }
}
