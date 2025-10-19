
namespace LowlandTech.Accounts.Backend.ApiKeys;

public sealed class RetrieveAllApiKeyHandler : IRequestHandler<RetrieveAllApiKeyRequest, RetrieveAllApiKeyResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllApiKeyHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllApiKeyResponse> Handle(RetrieveAllApiKeyRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<ApiKey>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Key != null && x.Key!.Contains(s))
                || (x.KeyHash != null && x.KeyHash!.Contains(s))
                || (x.KeyPrefix != null && x.KeyPrefix!.Contains(s))
                || (x.Name != null && x.Name!.Contains(s))
            );
        }

        // Order: prefer Name if present, else Id
        q = 
            q.OrderBy(x => x.Name);

        var page = req.Page <= 1 ? 1 : req.Page;
        var size = req.PageSize <= 0 ? 50 : req.PageSize;
        var skip = (page - 1) * size;

        var total = await q.CountAsync(cancellationToken);
        var items = await q.Skip(skip).Take(size).ToListAsync(cancellationToken);

        return new RetrieveAllApiKeyResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
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

