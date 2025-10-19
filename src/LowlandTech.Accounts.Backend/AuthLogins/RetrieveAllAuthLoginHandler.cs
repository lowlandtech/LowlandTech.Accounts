
namespace LowlandTech.Accounts.Backend.AuthLogins;

public sealed class RetrieveAllAuthLoginHandler : IRequestHandler<RetrieveAllAuthLoginRequest, RetrieveAllAuthLoginResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllAuthLoginHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllAuthLoginResponse> Handle(RetrieveAllAuthLoginRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<AuthLogin>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Name != null && x.Name!.Contains(s))
                || (x.Provider != null && x.Provider!.Contains(s))
                || (x.ProviderUserId != null && x.ProviderUserId!.Contains(s))
                || (x.AccessToken != null && x.AccessToken!.Contains(s))
                || (x.RefreshToken != null && x.RefreshToken!.Contains(s))
                || (x.Scopes != null && x.Scopes!.Contains(s))
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

        return new RetrieveAllAuthLoginResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static AuthLoginDto Map(AuthLogin e)
    {
        var dto = new AuthLoginDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Provider = e.Provider;
            dto.ProviderUserId = e.ProviderUserId;
            dto.AccessToken = e.AccessToken;
            dto.RefreshToken = e.RefreshToken;
            dto.ExpiresUtc = e.ExpiresUtc;
            dto.Scopes = e.Scopes;
        return dto;
    }
}

