
namespace LowlandTech.Accounts.Backend.Devices;

public sealed class CreateDeviceHandler : IRequestHandler<CreateDeviceRequest, CreateDeviceResponse>
{
    private readonly AccountsContext _db;

    public CreateDeviceHandler(AccountsContext db) => _db = db;

    public async Task<CreateDeviceResponse> Handle(CreateDeviceRequest req, CancellationToken cancellationToken = default)
    {
        var e = new Device()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            AccountId = req.AccountId,
            DeviceId = req.DeviceId,
            UserAgent = req.UserAgent,
            Ip = req.Ip,
            FirstSeenUtc = req.FirstSeenUtc,
            LastSeenUtc = req.LastSeenUtc,
            IsTrusted = req.IsTrusted,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateDeviceResponse { Id = e.Id };
    }
}

