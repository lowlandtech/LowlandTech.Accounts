
namespace LowlandTech.Accounts.Backend.Addresses;

public sealed class CreateAddressHandler : IRequestHandler<CreateAddressRequest, CreateAddressResponse>
{
    private readonly AccountsContext _db;

    public CreateAddressHandler(AccountsContext db) => _db = db;

    public async Task<CreateAddressResponse> Handle(CreateAddressRequest req, CancellationToken cancellationToken = default)
    {
        var e = new Address()
        {
            Name = req.Name,
            IsActive = req.IsActive,
            AccountId = req.AccountId,
            Kind = req.Kind,
            Line1 = req.Line1,
            Line2 = req.Line2,
            City = req.City,
            Region = req.Region,
            PostalCode = req.PostalCode,
            Country = req.Country,
            IsPrimary = req.IsPrimary,
        };
        _db.Add(e);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateAddressResponse { Id = e.Id };
    }
}

