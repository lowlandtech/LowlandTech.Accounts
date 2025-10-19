
namespace LowlandTech.Accounts.Backend.PasswordResetTokens;

public sealed class CreatePasswordResetTokenHandler : IRequestHandler<CreatePasswordResetTokenRequest, CreatePasswordResetTokenResponse>
{
    private readonly AccountsContext _db;

    public CreatePasswordResetTokenHandler(AccountsContext db) => _db = db;

    public async Task<CreatePasswordResetTokenResponse> Handle(CreatePasswordResetTokenRequest req, CancellationToken cancellationToken = default)
    {
        var e = new PasswordResetToken()
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

        return new CreatePasswordResetTokenResponse { Id = e.Id };
    }
}

