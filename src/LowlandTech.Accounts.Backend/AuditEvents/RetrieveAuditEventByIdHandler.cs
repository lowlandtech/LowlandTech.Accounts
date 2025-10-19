
namespace LowlandTech.Accounts.Backend.AuditEvents;

public sealed class RetrieveAuditEventByIdHandler : IRequestHandler<RetrieveAuditEventByIdRequest, RetrieveAuditEventByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAuditEventByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAuditEventByIdResponse> Handle(RetrieveAuditEventByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<AuditEvent>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveAuditEventByIdResponse() : new RetrieveAuditEventByIdResponse { Item = Map(e) };
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

