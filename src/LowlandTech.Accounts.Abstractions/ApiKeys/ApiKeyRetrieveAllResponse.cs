
namespace LowlandTech.Accounts.Abstractions.ApiKeys;

public sealed class RetrieveAllApiKeyResponse
{
    public IReadOnlyList<ApiKeyDto> Items { get; init; } = Array.Empty<ApiKeyDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

