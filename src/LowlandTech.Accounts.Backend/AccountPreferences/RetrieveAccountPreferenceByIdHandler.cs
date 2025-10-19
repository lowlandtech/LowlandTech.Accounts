
namespace LowlandTech.Accounts.Backend.AccountPreferences;

public sealed class RetrieveAccountPreferenceByIdHandler : IRequestHandler<RetrieveAccountPreferenceByIdRequest, RetrieveAccountPreferenceByIdResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public RetrieveAccountPreferenceByIdHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<RetrieveAccountPreferenceByIdResponse> Handle(RetrieveAccountPreferenceByIdRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<AccountPreference>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        return e is null ? new RetrieveAccountPreferenceByIdResponse() : new RetrieveAccountPreferenceByIdResponse { Item = Map(e) };
    }

    private static AccountPreferenceDto Map(AccountPreference e)
    {
        var dto = new AccountPreferenceDto();
            dto.Id = e.Id;
            dto.Name = e.Name;
            dto.IsActive = e.IsActive;
            dto.AccountId = e.AccountId;
            dto.Key = e.Key;
            dto.Value = e.Value;
            dto.ValueType = e.ValueType;
        return dto;
    }
}

