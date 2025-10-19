
namespace LowlandTech.Accounts.Backend.RecoveryCodes;

public sealed class DeleteRecoveryCodeHandler : IRequestHandler<DeleteRecoveryCodeRequest, DeleteRecoveryCodeResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteRecoveryCodeHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteRecoveryCodeResponse> Handle(DeleteRecoveryCodeRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<RecoveryCode>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteRecoveryCodeResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteRecoveryCodeResponse { Id = request.Id, Deleted = true };
    }
}

