
namespace LowlandTech.Accounts.Backend.Devices;

public sealed class RetrieveAllDeviceHandler : IRequestHandler<RetrieveAllDeviceRequest, RetrieveAllDeviceResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllDeviceHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllDeviceResponse> Handle(RetrieveAllDeviceRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<Device>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.DeviceId != null && x.DeviceId!.Contains(s))
                || (x.UserAgent != null && x.UserAgent!.Contains(s))
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

        return new RetrieveAllDeviceResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static DeviceDto Map(Device e)
    {
        var dto = new DeviceDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.DeviceId = e.DeviceId;
            dto.UserAgent = e.UserAgent;
            dto.Ip = e.Ip;
            dto.FirstSeenUtc = e.FirstSeenUtc;
            dto.LastSeenUtc = e.LastSeenUtc;
            dto.IsTrusted = e.IsTrusted;
        return dto;
    }
}

