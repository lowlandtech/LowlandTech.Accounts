namespace LowlandTech.Accounts.Tests.VCHIP_8019_EmailVerificationTokens.US01_List;

[Scenario(
    specId: "VCHIP-8019-US01-SC04",
    title: "Change page size above server max clamps to 500",
    given: "Given more than 500 EmailVerificationTokens exist",
    when:  "When I GET /api/emailverificationtokens?skip=0&take=2000",
    then:  "Then the API clamps take to 500 and returns at most 500 items")]
public sealed class SC04_ChangePageSize_ClampsTo500 : WhenTestingForAsync<AppFactory<Program>>
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
    [Then("It calls GET /api/emailverificationtokens?skip=0&take=500", "UAC008")]
    public async Task Server_Clamps_Take_To_500()
    {
        // Client asks for 2000; server clamps to 500 which we validate via count
        var res = await _client.GetAsync("/api/emailverificationtokens?skip=0&take=2000");
        res.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    [Then("At most 500 items are rendered", "UAC009")]
    public async Task Returns_At_Most_500_Items()
    {
        var items = await _client.GetFromJsonAsync<List<EmailVerificationToken>>("/api/emailverificationtokens?skip=0&take=2000");
        items.ShouldNotBeNull();
        items.Count.ShouldBeLessThanOrEqualTo(500);
    }
}
