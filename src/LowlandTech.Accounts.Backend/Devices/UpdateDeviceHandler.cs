
namespace LowlandTech.Accounts.Backend.Devices;

public sealed class UpdateDeviceHandler : IRequestHandler<UpdateDeviceRequest, UpdateDeviceResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateDeviceHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateDeviceResponse> Handle(UpdateDeviceRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Device>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateDeviceResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.DeviceId = req.DeviceId;
        e.UserAgent = req.UserAgent;
        e.Ip = req.Ip;
        e.FirstSeenUtc = req.FirstSeenUtc;
        e.LastSeenUtc = req.LastSeenUtc;
        e.IsTrusted = req.IsTrusted;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateDeviceResponse { Item = Map(e) };
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

