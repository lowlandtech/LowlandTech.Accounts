
namespace LowlandTech.Accounts.Backend.AuditEvents;

public sealed class CreateAuditEventHandler : IRequestHandler<CreateAuditEventRequest, CreateAuditEventResponse>
{
    private readonly AccountsContext _db;

    public CreateAuditEventHandler(AccountsContext db) => _db = db;

    public async Task<CreateAuditEventResponse> Handle(CreateAuditEventRequest req, CancellationToken cancellationToken = default)
    {
        var e = new AuditEvent()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            AccountId = req.AccountId,
            Kind = req.Kind,
            DataJson = req.DataJson,
            DeviceId = req.DeviceId,
            Ip = req.Ip,
            CreatedUtc = req.CreatedUtc,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateAuditEventResponse { Id = e.Id };
    }
}

