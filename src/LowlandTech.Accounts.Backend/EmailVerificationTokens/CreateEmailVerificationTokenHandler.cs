
namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;

public sealed class CreateEmailVerificationTokenHandler : IRequestHandler<CreateEmailVerificationTokenRequest, CreateEmailVerificationTokenResponse>
{
    private readonly AccountsContext _db;

    public CreateEmailVerificationTokenHandler(AccountsContext db) => _db = db;

    public async Task<CreateEmailVerificationTokenResponse> Handle(CreateEmailVerificationTokenRequest req, CancellationToken cancellationToken = default)
    {
        var e = new EmailVerificationToken()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            AccountId = req.AccountId,
            Token = req.Token,
            ExpiresUtc = req.ExpiresUtc,
            UsedUtc = req.UsedUtc,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateEmailVerificationTokenResponse { Id = e.Id };
    }
}

