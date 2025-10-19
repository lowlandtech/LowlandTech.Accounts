
namespace LowlandTech.Accounts.Backend.ApiKeys;

public sealed class CreateApiKeyHandler : IRequestHandler<CreateApiKeyRequest, CreateApiKeyResponse>
{
    private readonly AccountsContext _db;

    public CreateApiKeyHandler(AccountsContext db) => _db = db;

    public async Task<CreateApiKeyResponse> Handle(CreateApiKeyRequest req, CancellationToken cancellationToken = default)
    {
        var e = new ApiKey()
        {
            AccountId = req.AccountId,
            CreatedUtc = req.CreatedUtc,
            IsActive = req.IsActive,
            Key = req.Key,
            KeyHash = req.KeyHash,
            KeyPrefix = req.KeyPrefix,
            LastUsedUtc = req.LastUsedUtc,
            Name = req.Name,
            RevokedUtc = req.RevokedUtc,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateApiKeyResponse { Id = e.Id };
    }
}

