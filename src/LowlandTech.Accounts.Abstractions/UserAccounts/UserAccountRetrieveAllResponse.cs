
namespace LowlandTech.Accounts.Abstractions.UserAccounts;

public sealed class RetrieveAllUserAccountResponse
{
    public IReadOnlyList<UserAccountDto> Items { get; init; } = Array.Empty<UserAccountDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

