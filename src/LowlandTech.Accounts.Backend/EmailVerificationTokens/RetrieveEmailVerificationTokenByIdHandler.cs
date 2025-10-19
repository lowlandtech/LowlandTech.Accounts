
namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;

public sealed class RetrieveEmailVerificationTokenByIdHandler : IRequestHandler<RetrieveEmailVerificationTokenByIdRequest, RetrieveEmailVerificationTokenByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveEmailVerificationTokenByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveEmailVerificationTokenByIdResponse> Handle(RetrieveEmailVerificationTokenByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<EmailVerificationToken>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveEmailVerificationTokenByIdResponse() : new RetrieveEmailVerificationTokenByIdResponse { Item = Map(e) };
    }

    private static EmailVerificationTokenDto Map(EmailVerificationToken e)
    {
        var dto = new EmailVerificationTokenDto();
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

