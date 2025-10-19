
namespace LowlandTech.Accounts.Abstractions.Accounts;

public sealed class RetrieveAllAccountResponse
{
    public IReadOnlyList<AccountDto> Items { get; init; } = Array.Empty<AccountDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

