
namespace LowlandTech.Accounts.Backend.Sessions;

public sealed class RetrieveSessionByIdHandler : IRequestHandler<RetrieveSessionByIdRequest, RetrieveSessionByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveSessionByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveSessionByIdResponse> Handle(RetrieveSessionByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Session>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveSessionByIdResponse() : new RetrieveSessionByIdResponse { Item = Map(e) };
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

