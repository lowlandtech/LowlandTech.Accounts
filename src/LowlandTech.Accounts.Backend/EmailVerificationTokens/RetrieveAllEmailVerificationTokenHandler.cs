
namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;

public sealed class RetrieveAllEmailVerificationTokenHandler : IRequestHandler<RetrieveAllEmailVerificationTokenRequest, RetrieveAllEmailVerificationTokenResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllEmailVerificationTokenHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllEmailVerificationTokenResponse> Handle(RetrieveAllEmailVerificationTokenRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<EmailVerificationToken>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.Token != null && x.Token!.Contains(s))
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

        return new RetrieveAllEmailVerificationTokenResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static EmailVerificationTokenDto Map(EmailVerificationToken e)
    {
        var dto = new EmailVerificationTokenDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Token = e.Token;
            dto.ExpiresUtc = e.ExpiresUtc;
            dto.UsedUtc = e.UsedUtc;
        return dto;
    }
}

