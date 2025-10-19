
namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;

public sealed class UpdateEmailVerificationTokenHandler : IRequestHandler<UpdateEmailVerificationTokenRequest, UpdateEmailVerificationTokenResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateEmailVerificationTokenHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateEmailVerificationTokenResponse> Handle(UpdateEmailVerificationTokenRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<EmailVerificationToken>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateEmailVerificationTokenResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.Token = req.Token;
        e.ExpiresUtc = req.ExpiresUtc;
        e.UsedUtc = req.UsedUtc;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateEmailVerificationTokenResponse { Item = Map(e) };
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

