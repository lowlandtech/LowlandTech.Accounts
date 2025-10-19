
namespace LowlandTech.Accounts.Backend.Accounts;

public sealed class RetrieveAccountByIdHandler : IRequestHandler<RetrieveAccountByIdRequest, RetrieveAccountByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAccountByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAccountByIdResponse> Handle(RetrieveAccountByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Account>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveAccountByIdResponse() : new RetrieveAccountByIdResponse { Item = Map(e) };
    }

    private static AccountDto Map(Account e)
    {
        var dto = new AccountDto();
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

