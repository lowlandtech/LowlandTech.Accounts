
namespace LowlandTech.Accounts.Abstractions.AuditEvents;

public sealed class DeleteAuditEventResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

