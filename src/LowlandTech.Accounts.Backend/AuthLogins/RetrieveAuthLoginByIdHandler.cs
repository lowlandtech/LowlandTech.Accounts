
namespace LowlandTech.Accounts.Backend.AuthLogins;

public sealed class RetrieveAuthLoginByIdHandler : IRequestHandler<RetrieveAuthLoginByIdRequest, RetrieveAuthLoginByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAuthLoginByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAuthLoginByIdResponse> Handle(RetrieveAuthLoginByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<AuthLogin>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveAuthLoginByIdResponse() : new RetrieveAuthLoginByIdResponse { Item = Map(e) };
    }

    private static AuthLoginDto Map(AuthLogin e)
    {
        var dto = new AuthLoginDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Provider = e.Provider;
            dto.ProviderUserId = e.ProviderUserId;
            dto.AccessToken = e.AccessToken;
            dto.RefreshToken = e.RefreshToken;
            dto.ExpiresUtc = e.ExpiresUtc;
            dto.Scopes = e.Scopes;
        return dto;
    }
}

