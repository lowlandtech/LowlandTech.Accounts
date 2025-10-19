
namespace LowlandTech.Accounts.Backend.RecoveryCodes;

public sealed class CreateRecoveryCodeHandler : IRequestHandler<CreateRecoveryCodeRequest, CreateRecoveryCodeResponse>
{
    private readonly AccountsContext _db;

    public CreateRecoveryCodeHandler(AccountsContext db) => _db = db;

    public async Task<CreateRecoveryCodeResponse> Handle(CreateRecoveryCodeRequest req, CancellationToken cancellationToken = default)
    {
        var e = new RecoveryCode()
        {
            AccountId = req.AccountId,
            Code = req.Code,
            CodeHash = req.CodeHash,
            IsActive = req.IsActive,
            Name = req.Name,
            UsedUtc = req.UsedUtc,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateRecoveryCodeResponse { Id = e.Id };
    }
}

