
namespace LowlandTech.Accounts.Backend.PasswordResetTokens;

public sealed class UpdatePasswordResetTokenHandler : IRequestHandler<UpdatePasswordResetTokenRequest, UpdatePasswordResetTokenResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdatePasswordResetTokenHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdatePasswordResetTokenResponse> Handle(UpdatePasswordResetTokenRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<PasswordResetToken>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdatePasswordResetTokenResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.Token = req.Token;
        e.ExpiresUtc = req.ExpiresUtc;
        e.UsedUtc = req.UsedUtc;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdatePasswordResetTokenResponse { Item = Map(e) };
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

