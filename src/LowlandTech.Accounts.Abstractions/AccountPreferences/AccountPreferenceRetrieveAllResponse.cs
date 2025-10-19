
namespace LowlandTech.Accounts.Abstractions.AccountPreferences;

public sealed class RetrieveAllAccountPreferenceResponse
{
    public IReadOnlyList<AccountPreferenceDto> Items { get; init; } = Array.Empty<AccountPreferenceDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

