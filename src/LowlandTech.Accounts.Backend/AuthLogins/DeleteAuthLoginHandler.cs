
namespace LowlandTech.Accounts.Backend.AuthLogins;

public sealed class DeleteAuthLoginHandler : IRequestHandler<DeleteAuthLoginRequest, DeleteAuthLoginResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteAuthLoginHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteAuthLoginResponse> Handle(DeleteAuthLoginRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<AuthLogin>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteAuthLoginResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteAuthLoginResponse { Id = request.Id, Deleted = true };
    }
}

