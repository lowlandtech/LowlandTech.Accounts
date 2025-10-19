
namespace LowlandTech.Accounts.Backend.AuthLogins;

public sealed class CreateAuthLoginHandler : IRequestHandler<CreateAuthLoginRequest, CreateAuthLoginResponse>
{
    private readonly AccountsContext _db;

    public CreateAuthLoginHandler(AccountsContext db) => _db = db;

    public async Task<CreateAuthLoginResponse> Handle(CreateAuthLoginRequest req, CancellationToken cancellationToken = default)
    {
        var e = new AuthLogin()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            AccountId = req.AccountId,
            Provider = req.Provider,
            ProviderUserId = req.ProviderUserId,
            AccessToken = req.AccessToken,
            RefreshToken = req.RefreshToken,
            ExpiresUtc = req.ExpiresUtc,
            Scopes = req.Scopes,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateAuthLoginResponse { Id = e.Id };
    }
}

