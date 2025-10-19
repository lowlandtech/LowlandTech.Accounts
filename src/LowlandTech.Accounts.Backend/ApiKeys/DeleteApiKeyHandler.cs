
namespace LowlandTech.Accounts.Backend.ApiKeys;

public sealed class DeleteApiKeyHandler : IRequestHandler<DeleteApiKeyRequest, DeleteApiKeyResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteApiKeyHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteApiKeyResponse> Handle(DeleteApiKeyRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<ApiKey>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteApiKeyResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteApiKeyResponse { Id = request.Id, Deleted = true };
    }
}

