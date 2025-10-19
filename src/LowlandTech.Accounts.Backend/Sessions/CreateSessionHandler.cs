
namespace LowlandTech.Accounts.Backend.Sessions;

public sealed class CreateSessionHandler : IRequestHandler<CreateSessionRequest, CreateSessionResponse>
{
    private readonly AccountsContext _db;

    public CreateSessionHandler(AccountsContext db) => _db = db;

    public async Task<CreateSessionResponse> Handle(CreateSessionRequest req, CancellationToken cancellationToken = default)
    {
        var e = new Session()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            AccountId = req.AccountId,
            DeviceId = req.DeviceId,
            CreatedUtc = req.CreatedUtc,
            ExpiresUtc = req.ExpiresUtc,
            RevokedUtc = req.RevokedUtc,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateSessionResponse { Id = e.Id };
    }
}

