
namespace LowlandTech.Accounts.Backend.Sessions;

public sealed class UpdateSessionHandler : IRequestHandler<UpdateSessionRequest, UpdateSessionResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateSessionHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateSessionResponse> Handle(UpdateSessionRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Session>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateSessionResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.DeviceId = req.DeviceId;
        e.CreatedUtc = req.CreatedUtc;
        e.ExpiresUtc = req.ExpiresUtc;
        e.RevokedUtc = req.RevokedUtc;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateSessionResponse { Item = Map(e) };
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

