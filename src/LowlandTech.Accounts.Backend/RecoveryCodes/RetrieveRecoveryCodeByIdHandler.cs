
namespace LowlandTech.Accounts.Backend.RecoveryCodes;

public sealed class RetrieveRecoveryCodeByIdHandler : IRequestHandler<RetrieveRecoveryCodeByIdRequest, RetrieveRecoveryCodeByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveRecoveryCodeByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveRecoveryCodeByIdResponse> Handle(RetrieveRecoveryCodeByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<RecoveryCode>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveRecoveryCodeByIdResponse() : new RetrieveRecoveryCodeByIdResponse { Item = Map(e) };
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

