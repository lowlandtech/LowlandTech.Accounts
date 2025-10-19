
namespace LowlandTech.Accounts.Backend.AccountPreferences;

public sealed class RetrieveAllAccountPreferenceHandler : IRequestHandler<RetrieveAllAccountPreferenceRequest, RetrieveAllAccountPreferenceResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllAccountPreferenceHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllAccountPreferenceResponse> Handle(RetrieveAllAccountPreferenceRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<AccountPreference>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.Key != null && x.Key!.Contains(s))
                || (x.Value != null && x.Value!.Contains(s))
                || (x.ValueType != null && x.ValueType!.Contains(s))
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

        return new RetrieveAllAccountPreferenceResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static AccountPreferenceDto Map(AccountPreference e)
    {
        var dto = new AccountPreferenceDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Key = e.Key;
            dto.Value = e.Value;
            dto.ValueType = e.ValueType;
        return dto;
    }
}

