
namespace LowlandTech.Accounts.Backend.AccountPreferences;

public sealed class UpdateAccountPreferenceHandler : IRequestHandler<UpdateAccountPreferenceRequest, UpdateAccountPreferenceResponse>
{
    private readonly IDbContextFactory<AccountsContext> _factory;
    public UpdateAccountPreferenceHandler(IDbContextFactory<AccountsContext> factory) => _factory = factory;

    public async Task<UpdateAccountPreferenceResponse> Handle(UpdateAccountPreferenceRequest req, CancellationToken cancellationToken = default)
    {
        await using var db = await _factory.CreateDbContextAsync(cancellationToken);
        var e = await db.Set<AccountPreference>().FirstOrDefaultAsync(x => x.Id == req.Id, cancellationToken);
        if (e is null) return new UpdateAccountPreferenceResponse(); // or throw a NotFound exception if you prefer

        e.Name = req.Name;
        e.IsActive = req.IsActive;
        e.AccountId = req.AccountId;
        e.Key = req.Key;
        e.Value = req.Value;
        e.ValueType = req.ValueType;

        await db.SaveChangesAsync(cancellationToken);
        return new UpdateAccountPreferenceResponse { Item = Map(e) };
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

