
namespace LowlandTech.Accounts.Backend.AuditEvents;

public sealed class RetrieveAllAuditEventHandler : IRequestHandler<RetrieveAllAuditEventRequest, RetrieveAllAuditEventResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllAuditEventHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllAuditEventResponse> Handle(RetrieveAllAuditEventRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<AuditEvent>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.Kind != null && x.Kind!.Contains(s))
                || (x.DataJson != null && x.DataJson!.Contains(s))
                || (x.DeviceId != null && x.DeviceId!.Contains(s))
                || (x.Ip != null && x.Ip!.Contains(s))
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

        return new RetrieveAllAuditEventResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static AuditEventDto Map(AuditEvent e)
    {
        var dto = new AuditEventDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Kind = e.Kind;
            dto.DataJson = e.DataJson;
            dto.DeviceId = e.DeviceId;
            dto.Ip = e.Ip;
            dto.CreatedUtc = e.CreatedUtc;
        return dto;
    }
}

