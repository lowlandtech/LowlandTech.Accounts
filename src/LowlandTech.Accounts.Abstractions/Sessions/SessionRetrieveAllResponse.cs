
namespace LowlandTech.Accounts.Abstractions.Sessions;

public sealed class RetrieveAllSessionResponse
{
    public IReadOnlyList<SessionDto> Items { get; init; } = Array.Empty<SessionDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

