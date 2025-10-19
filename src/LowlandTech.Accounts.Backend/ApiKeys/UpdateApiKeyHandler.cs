
namespace LowlandTech.Accounts.Backend.ApiKeys;

public sealed class UpdateApiKeyHandler : IRequestHandler<UpdateApiKeyRequest, UpdateApiKeyResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateApiKeyHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateApiKeyResponse> Handle(UpdateApiKeyRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<ApiKey>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateApiKeyResponse(); // or throw a NotFound exception if you prefer

        e.AccountId = req.AccountId;
        e.CreatedUtc = req.CreatedUtc;
        e.IsActive = req.IsActive;
        e.Key = req.Key;
        e.KeyHash = req.KeyHash;
        e.KeyPrefix = req.KeyPrefix;
        e.LastUsedUtc = req.LastUsedUtc;
        e.Name = req.Name;
        e.RevokedUtc = req.RevokedUtc;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateApiKeyResponse { Item = Map(e) };
    }

    private static ApiKeyDto Map(ApiKey e)
    {
        var dto = new ApiKeyDto();
            dto.AccountId = e.AccountId;
            dto.CreatedUtc = e.CreatedUtc;
            dto.Id = e.Id;
            dto.IsActive = e.IsActive;
            dto.Key = e.Key;
            dto.KeyHash = e.KeyHash;
            dto.KeyPrefix = e.KeyPrefix;
            dto.LastUsedUtc = e.LastUsedUtc;
            dto.Name = e.Name;
            dto.RevokedUtc = e.RevokedUtc;
        return dto;
    }
}

