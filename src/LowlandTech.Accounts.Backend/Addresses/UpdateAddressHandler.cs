
namespace LowlandTech.Accounts.Backend.Addresses;

public sealed class UpdateAddressHandler : IRequestHandler<UpdateAddressRequest, UpdateAddressResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateAddressHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateAddressResponse> Handle(UpdateAddressRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Address>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateAddressResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.Kind = req.Kind;
        e.Line1 = req.Line1;
        e.Line2 = req.Line2;
        e.City = req.City;
        e.Region = req.Region;
        e.PostalCode = req.PostalCode;
        e.Country = req.Country;
        e.IsPrimary = req.IsPrimary;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateAddressResponse { Item = Map(e) };
    }

    private static AddressDto Map(Address e)
    {
        var dto = new AddressDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Kind = e.Kind;
            dto.Line1 = e.Line1;
            dto.Line2 = e.Line2;
            dto.City = e.City;
            dto.Region = e.Region;
            dto.PostalCode = e.PostalCode;
            dto.Country = e.Country;
            dto.IsPrimary = e.IsPrimary;
        return dto;
    }
}

