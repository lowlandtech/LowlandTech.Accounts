
namespace LowlandTech.Accounts.Backend.UserAccounts;

public sealed class CreateUserAccountHandler : IRequestHandler<CreateUserAccountRequest, CreateUserAccountResponse>
{
    private readonly AccountsContext _db;

    public CreateUserAccountHandler(AccountsContext db) => _db = db;

    public async Task<CreateUserAccountResponse> Handle(CreateUserAccountRequest req, CancellationToken cancellationToken = default)
    {
        var e = new UserAccount()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            Email = req.Email,
            DisplayName = req.DisplayName,
            Phone = req.Phone,
            PhotoUrl = req.PhotoUrl,
            Timezone = req.Timezone,
            Locale = req.Locale,
            PreferredCurrency = req.PreferredCurrency,
            TwoFactorEnabled = req.TwoFactorEnabled,
            CreatedUtc = req.CreatedUtc,
            LastLoginUtc = req.LastLoginUtc,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateUserAccountResponse { Id = e.Id };
    }
}

