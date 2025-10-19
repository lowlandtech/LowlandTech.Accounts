
namespace LowlandTech.Accounts.Backend.Addresses;

public sealed class RetrieveAddressByIdHandler : IRequestHandler<RetrieveAddressByIdRequest, RetrieveAddressByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAddressByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAddressByIdResponse> Handle(RetrieveAddressByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<Address>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveAddressByIdResponse() : new RetrieveAddressByIdResponse { Item = Map(e) };
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

