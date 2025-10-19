
namespace LowlandTech.Accounts.Backend.Devices;

public sealed class DeleteDeviceHandler : IRequestHandler<DeleteDeviceRequest, DeleteDeviceResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public DeleteDeviceHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<DeleteDeviceResponse> Handle(DeleteDeviceRequest request, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var set = db.Set<Device>();
        var e = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (e is null) return new DeleteDeviceResponse { Id = request.Id, Deleted = false }; // or throw NotFound

        set.Remove(e);
        await db.SaveChangesAsync(cancellationToken);
        return new DeleteDeviceResponse { Id = request.Id, Deleted = true };
    }
}

