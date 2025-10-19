
namespace LowlandTech.Accounts.Backend.AccountPreferences;

public sealed class DeleteAccountPreferenceHandler : IRequestHandler<DeleteAccountPreferenceRequest, DeleteAccountPreferenceResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteAccountPreferenceHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteAccountPreferenceResponse> Handle(DeleteAccountPreferenceRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<AccountPreference>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteAccountPreferenceResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteAccountPreferenceResponse { Id = request.Id, Deleted = true };
    }
}

