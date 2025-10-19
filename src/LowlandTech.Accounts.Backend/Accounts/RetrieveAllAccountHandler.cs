
namespace LowlandTech.Accounts.Backend.Accounts;

public sealed class RetrieveAllAccountHandler : IRequestHandler<RetrieveAllAccountRequest, RetrieveAllAccountResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllAccountHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllAccountResponse> Handle(RetrieveAllAccountRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<Account>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.Email != null && x.Email!.Contains(s))
                || (x.DisplayName != null && x.DisplayName!.Contains(s))
                || (x.Phone != null && x.Phone!.Contains(s))
                || (x.PhotoUrl != null && x.PhotoUrl!.Contains(s))
                || (x.Timezone != null && x.Timezone!.Contains(s))
                || (x.Locale != null && x.Locale!.Contains(s))
                || (x.PreferredCurrency != null && x.PreferredCurrency!.Contains(s))
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

        return new RetrieveAllAccountResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static AccountDto Map(Account e)
    {
        var dto = new AccountDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.Email = e.Email;
            dto.DisplayName = e.DisplayName;
            dto.Phone = e.Phone;
            dto.PhotoUrl = e.PhotoUrl;
            dto.Timezone = e.Timezone;
            dto.Locale = e.Locale;
            dto.PreferredCurrency = e.PreferredCurrency;
            dto.TwoFactorEnabled = e.TwoFactorEnabled;
            dto.CreatedUtc = e.CreatedUtc;
            dto.LastLoginUtc = e.LastLoginUtc;
        return dto;
    }
}

