
namespace LowlandTech.Accounts.Backend.AuditEvents;

public sealed class DeleteAuditEventHandler : IRequestHandler<DeleteAuditEventRequest, DeleteAuditEventResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteAuditEventHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteAuditEventResponse> Handle(DeleteAuditEventRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<AuditEvent>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteAuditEventResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteAuditEventResponse { Id = request.Id, Deleted = true };
    }
}

