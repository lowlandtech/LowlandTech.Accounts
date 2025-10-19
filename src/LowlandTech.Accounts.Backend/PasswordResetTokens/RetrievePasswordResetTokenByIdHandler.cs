
namespace LowlandTech.Accounts.Backend.PasswordResetTokens;

public sealed class RetrievePasswordResetTokenByIdHandler : IRequestHandler<RetrievePasswordResetTokenByIdRequest, RetrievePasswordResetTokenByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrievePasswordResetTokenByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrievePasswordResetTokenByIdResponse> Handle(RetrievePasswordResetTokenByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<PasswordResetToken>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrievePasswordResetTokenByIdResponse() : new RetrievePasswordResetTokenByIdResponse { Item = Map(e) };
    }

    private static PasswordResetTokenDto Map(PasswordResetToken e)
    {
        var dto = new PasswordResetTokenDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Token = e.Token;
            dto.ExpiresUtc = e.ExpiresUtc;
            dto.UsedUtc = e.UsedUtc;
        return dto;
    }
}

