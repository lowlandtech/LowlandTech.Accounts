
namespace LowlandTech.Accounts.Backend.AuthLogins;

public sealed class UpdateAuthLoginHandler : IRequestHandler<UpdateAuthLoginRequest, UpdateAuthLoginResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateAuthLoginHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateAuthLoginResponse> Handle(UpdateAuthLoginRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<AuthLogin>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateAuthLoginResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.Provider = req.Provider;
        e.ProviderUserId = req.ProviderUserId;
        e.AccessToken = req.AccessToken;
        e.RefreshToken = req.RefreshToken;
        e.ExpiresUtc = req.ExpiresUtc;
        e.Scopes = req.Scopes;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateAuthLoginResponse { Item = Map(e) };
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

