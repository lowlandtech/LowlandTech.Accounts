
namespace LowlandTech.Accounts.Backend.Devices;

public sealed class RetrieveDeviceByIdHandler : IRequestHandler<RetrieveDeviceByIdRequest, RetrieveDeviceByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveDeviceByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveDeviceByIdResponse> Handle(RetrieveDeviceByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Device>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveDeviceByIdResponse() : new RetrieveDeviceByIdResponse { Item = Map(e) };
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

