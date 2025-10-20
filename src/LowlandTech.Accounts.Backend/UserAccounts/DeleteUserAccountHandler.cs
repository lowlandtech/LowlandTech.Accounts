
namespace LowlandTech.Accounts.Backend.UserAccounts;

public sealed class DeleteUserAccountHandler : IRequestHandler<DeleteUserAccountRequest, DeleteUserAccountResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteUserAccountHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteUserAccountResponse> Handle(DeleteUserAccountRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<UserAccount>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteUserAccountResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteUserAccountResponse { Id = request.Id, Deleted = true };
    }
}

