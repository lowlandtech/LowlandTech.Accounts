
namespace LowlandTech.Accounts.Backend.Accounts;

public sealed class DeleteAccountHandler : IRequestHandler<DeleteAccountRequest, DeleteAccountResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteAccountHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteAccountResponse> Handle(DeleteAccountRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<Account>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteAccountResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteAccountResponse { Id = request.Id, Deleted = true };
    }
}

