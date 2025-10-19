
namespace LowlandTech.Accounts.Backend.Addresses;

public sealed class RetrieveAllAddressHandler : IRequestHandler<RetrieveAllAddressRequest, RetrieveAllAddressResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllAddressHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllAddressResponse> Handle(RetrieveAllAddressRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<Address>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.Kind != null && x.Kind!.Contains(s))
                || (x.Line1 != null && x.Line1!.Contains(s))
                || (x.Line2 != null && x.Line2!.Contains(s))
                || (x.City != null && x.City!.Contains(s))
                || (x.Region != null && x.Region!.Contains(s))
                || (x.PostalCode != null && x.PostalCode!.Contains(s))
                || (x.Country != null && x.Country!.Contains(s))
            );
        }

        // Order: prefer Name if present, else Id
        q = 
            q.OrderBy(x => x.Name);

        var page = req.Page <= 1 ? 1 : req.Page;
        var size = req.PageSize <= 0 ? 50 : req.PageSize;
        var skip = (page - 1) * size;

        var total = await q.CountAsync(cancellationToken);
        var items = await q.Skip(skip).Take(size).ToListAsync(cancellationToken);

        return new RetrieveAllAddressResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
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

