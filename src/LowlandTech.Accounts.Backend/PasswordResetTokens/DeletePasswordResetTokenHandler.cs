
namespace LowlandTech.Accounts.Backend.PasswordResetTokens;

public sealed class DeletePasswordResetTokenHandler : IRequestHandler<DeletePasswordResetTokenRequest, DeletePasswordResetTokenResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeletePasswordResetTokenHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeletePasswordResetTokenResponse> Handle(DeletePasswordResetTokenRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<PasswordResetToken>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeletePasswordResetTokenResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeletePasswordResetTokenResponse { Id = request.Id, Deleted = true };
    }
}

