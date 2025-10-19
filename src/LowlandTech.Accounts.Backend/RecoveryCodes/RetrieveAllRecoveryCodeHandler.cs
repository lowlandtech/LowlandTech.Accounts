
namespace LowlandTech.Accounts.Backend.RecoveryCodes;

public sealed class RetrieveAllRecoveryCodeHandler : IRequestHandler<RetrieveAllRecoveryCodeRequest, RetrieveAllRecoveryCodeResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAllRecoveryCodeHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAllRecoveryCodeResponse> Handle(RetrieveAllRecoveryCodeRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var q = db.Set<RecoveryCode>().AsNoTracking();

        // Optional free-text search across string columns
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            var s = req.Search!;

            q = q.Where(x =>
                (x.Code != null && x.Code!.Contains(s))
                || (x.CodeHash != null && x.CodeHash!.Contains(s))
                || (x.Name != null && x.Name!.Contains(s))
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

        return new RetrieveAllRecoveryCodeResponse
        {
            Items = items.Select(Map).ToList(),
            Total = total
        };
    }

    private static RecoveryCodeDto Map(RecoveryCode e)
    {
        var dto = new RecoveryCodeDto();
            dto.AccountId = e.AccountId;
            dto.Code = e.Code;
            dto.CodeHash = e.CodeHash;
            dto.Id = e.Id;
            dto.IsActive = e.IsActive;
            dto.Name = e.Name;
            dto.UsedUtc = e.UsedUtc;
        return dto;
    }
}

