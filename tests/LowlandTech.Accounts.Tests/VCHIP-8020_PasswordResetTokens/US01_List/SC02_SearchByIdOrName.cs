namespace LowlandTech.Accounts.Tests.VCHIP_8020_PasswordResetTokens.US01_List;

[Scenario(
    specId: "VCHIP-8020-US01-SC02",
    title: "Search by Id or Name",
    given: "Given the API has PasswordResetTokens with searchable Ids and Names",
    when:  "When I GET /api/passwordresettokens?skip=0&take=100&search={term}",
    then:  "Then only PasswordResetTokens whose Id or Name contains the search term are returned")]
public sealed class SC02_SearchByIdOrName : WhenTestingForAsync<AppFactory<Program>>
{
    private HttpClient _client = null!;

    protected override AppFactory<Program> For() => new AppFactory<Program>();
    protected override async Task GivenAsync() => await Sut.Db.SeedAsync<DefaultPasswordResetTokenSeedUseCase>();
    protected override Task WhenAsync()
    {
        _client = Sut.CreateClient();
        return Task.CompletedTask;
    }

    [Fact]
    [Then("It calls GET /api/passwordresettokens?skip=0&take=100&search={term}", "UAC004")]
    public async Task Calls_Endpoint_With_Search_Query()
    {
        var res = await _client.GetAsync("/api/passwordresettokens?skip=0&take=100&search=test");
        res.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    [Then("Only PasswordResetTokens whose Id or Name contains the search term are shown", "UAC005")]
    public async Task Filters_By_Id_Or_Name_Contains()
    {
        var items = await _client.GetFromJsonAsync<List<PasswordResetToken>>("/api/passwordresettokens?skip=0&take=100&search=test");
        items.ShouldNotBeNull();
        items.Count.ShouldBeGreaterThanOrEqualTo(0);
        items.ShouldAllBe(i => (i.Id + i.Name).Contains("test", StringComparison.OrdinalIgnoreCase));
    }
}
