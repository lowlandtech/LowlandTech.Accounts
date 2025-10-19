
namespace LowlandTech.Accounts.Backend.Sessions;

public sealed class DeleteSessionHandler : IRequestHandler<DeleteSessionRequest, DeleteSessionResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteSessionHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteSessionResponse> Handle(DeleteSessionRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<Session>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteSessionResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteSessionResponse { Id = request.Id, Deleted = true };
    }
}

