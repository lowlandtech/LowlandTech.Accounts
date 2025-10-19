
namespace LowlandTech.Accounts.Backend.Accounts;

public sealed class UpdateAccountHandler : IRequestHandler<UpdateAccountRequest, UpdateAccountResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateAccountHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateAccountResponse> Handle(UpdateAccountRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Account>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateAccountResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.Email = req.Email;
        e.DisplayName = req.DisplayName;
        e.Phone = req.Phone;
        e.PhotoUrl = req.PhotoUrl;
        e.Timezone = req.Timezone;
        e.Locale = req.Locale;
        e.PreferredCurrency = req.PreferredCurrency;
        e.TwoFactorEnabled = req.TwoFactorEnabled;
        e.CreatedUtc = req.CreatedUtc;
        e.LastLoginUtc = req.LastLoginUtc;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateAccountResponse { Item = Map(e) };
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

