
namespace LowlandTech.Accounts.Backend.Addresses;

public sealed class DeleteAddressHandler : IRequestHandler<DeleteAddressRequest, DeleteAddressResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteAddressHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteAddressResponse> Handle(DeleteAddressRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<Address>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteAddressResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteAddressResponse { Id = request.Id, Deleted = true };
    }
}

