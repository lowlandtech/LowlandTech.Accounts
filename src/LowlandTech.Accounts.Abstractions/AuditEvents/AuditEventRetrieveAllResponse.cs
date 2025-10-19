
namespace LowlandTech.Accounts.Abstractions.AuditEvents;

public sealed class RetrieveAllAuditEventResponse
{
    public IReadOnlyList<AuditEventDto> Items { get; init; } = Array.Empty<AuditEventDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

