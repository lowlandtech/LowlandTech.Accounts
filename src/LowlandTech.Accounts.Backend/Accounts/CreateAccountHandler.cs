
namespace LowlandTech.Accounts.Backend.Accounts;

public sealed class CreateAccountHandler : IRequestHandler<CreateAccountRequest, CreateAccountResponse>
{
    private readonly AccountsContext _db;

    public CreateAccountHandler(AccountsContext db) => _db = db;

    public async Task<CreateAccountResponse> Handle(CreateAccountRequest req, CancellationToken cancellationToken = default)
    {
        var e = new Account()
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

        return new CreateAccountResponse { Id = e.Id };
    }
}

