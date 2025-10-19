
namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;

public sealed class DeleteEmailVerificationTokenHandler : IRequestHandler<DeleteEmailVerificationTokenRequest, DeleteEmailVerificationTokenResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteEmailVerificationTokenHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteEmailVerificationTokenResponse> Handle(DeleteEmailVerificationTokenRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<EmailVerificationToken>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteEmailVerificationTokenResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteEmailVerificationTokenResponse { Id = request.Id, Deleted = true };
    }
}

