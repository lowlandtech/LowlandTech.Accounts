
namespace LowlandTech.Accounts.Backend.UserAccounts;

public sealed class UpdateUserAccountHandler : IRequestHandler<UpdateUserAccountRequest, UpdateUserAccountResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateUserAccountHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateUserAccountResponse> Handle(UpdateUserAccountRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<UserAccount>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateUserAccountResponse(); // or throw a NotFound exception if you prefer

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
        return new UpdateUserAccountResponse { Item = Map(e) };
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

