
namespace LowlandTech.Accounts.Backend.AuditEvents;

public sealed class UpdateAuditEventHandler : IRequestHandler<UpdateAuditEventRequest, UpdateAuditEventResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateAuditEventHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateAuditEventResponse> Handle(UpdateAuditEventRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<AuditEvent>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateAuditEventResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.Kind = req.Kind;
        e.DataJson = req.DataJson;
        e.DeviceId = req.DeviceId;
        e.Ip = req.Ip;
        e.CreatedUtc = req.CreatedUtc;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateAuditEventResponse { Item = Map(e) };
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

