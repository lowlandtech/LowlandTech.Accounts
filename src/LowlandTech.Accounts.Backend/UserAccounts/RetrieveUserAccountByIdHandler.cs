
namespace LowlandTech.Accounts.Backend.UserAccounts;

public sealed class RetrieveUserAccountByIdHandler : IRequestHandler<RetrieveUserAccountByIdRequest, RetrieveUserAccountByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveUserAccountByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveUserAccountByIdResponse> Handle(RetrieveUserAccountByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<UserAccount>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveUserAccountByIdResponse() : new RetrieveUserAccountByIdResponse { Item = Map(e) };
    }

    private static UserAccountDto Map(UserAccount e)
    {
        var dto = new UserAccountDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.Email = e.Email;
            dto.DisplayName = e.DisplayName;
            dto.Phone = e.Phone;
            dto.PhotoUrl = e.PhotoUrl;
            dto.Timezone = e.Timezone;
            dto.Locale = e.Locale;
            dto.PreferredCurrency = e.PreferredCurrency;
            dto.TwoFactorEnabled = e.TwoFactorEnabled;
            dto.CreatedUtc = e.CreatedUtc;
            dto.LastLoginUtc = e.LastLoginUtc;
        return dto;
    }
}

