
namespace LowlandTech.Accounts.Backend.RecoveryCodes;

public sealed class UpdateRecoveryCodeHandler : IRequestHandler<UpdateRecoveryCodeRequest, UpdateRecoveryCodeResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateRecoveryCodeHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateRecoveryCodeResponse> Handle(UpdateRecoveryCodeRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<RecoveryCode>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateRecoveryCodeResponse(); // or throw a NotFound exception if you prefer

        e.AccountId = req.AccountId;
        e.Code = req.Code;
        e.CodeHash = req.CodeHash;
        e.IsActive = req.IsActive;
        e.Name = req.Name;
        e.UsedUtc = req.UsedUtc;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateRecoveryCodeResponse { Item = Map(e) };
    }

    private static RecoveryCodeDto Map(RecoveryCode e)
    {
        var dto = new RecoveryCodeDto();
            dto.AccountId = e.AccountId;
            dto.Code = e.Code;
            dto.CodeHash = e.CodeHash;
            dto.Id = e.Id;
            dto.IsActive = e.IsActive;
            dto.Name = e.Name;
            dto.UsedUtc = e.UsedUtc;
        return dto;
    }
}

