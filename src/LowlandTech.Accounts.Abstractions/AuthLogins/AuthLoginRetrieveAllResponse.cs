
namespace LowlandTech.Accounts.Abstractions.AuthLogins;

public sealed class RetrieveAllAuthLoginResponse
{
    public IReadOnlyList<AuthLoginDto> Items { get; init; } = Array.Empty<AuthLoginDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

