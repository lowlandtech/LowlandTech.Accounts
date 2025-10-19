
namespace LowlandTech.Accounts.Backend.ApiKeys;

public sealed class RetrieveApiKeyByIdHandler : IRequestHandler<RetrieveApiKeyByIdRequest, RetrieveApiKeyByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveApiKeyByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveApiKeyByIdResponse> Handle(RetrieveApiKeyByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<ApiKey>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveApiKeyByIdResponse() : new RetrieveApiKeyByIdResponse { Item = Map(e) };
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

