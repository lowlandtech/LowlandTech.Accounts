
namespace LowlandTech.Accounts.Backend.Sessions;

public sealed class RetrieveAllSessionHandler : IRequestHandler<RetrieveAllSessionRequest, RetrieveAllSessionResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllSessionHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllSessionResponse> Handle(RetrieveAllSessionRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<Session>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.DeviceId != null && x.DeviceId!.Contains(s))
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

        return new RetrieveAllSessionResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static SessionDto Map(Session e)
    {
        var dto = new SessionDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.DeviceId = e.DeviceId;
            dto.CreatedUtc = e.CreatedUtc;
            dto.ExpiresUtc = e.ExpiresUtc;
            dto.RevokedUtc = e.RevokedUtc;
        return dto;
    }
}

