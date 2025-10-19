
namespace LowlandTech.Accounts.Backend.AccountPreferences;

public sealed class CreateAccountPreferenceHandler : IRequestHandler<CreateAccountPreferenceRequest, CreateAccountPreferenceResponse>
{
    private readonly AccountsContext _db;

    public CreateAccountPreferenceHandler(AccountsContext db) => _db = db;

    public async Task<CreateAccountPreferenceResponse> Handle(CreateAccountPreferenceRequest req, CancellationToken cancellationToken = default)
    {
        var e = new AccountPreference()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            AccountId = req.AccountId,
            Key = req.Key,
            Value = req.Value,
            ValueType = req.ValueType,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateAccountPreferenceResponse { Id = e.Id };
    }
}

