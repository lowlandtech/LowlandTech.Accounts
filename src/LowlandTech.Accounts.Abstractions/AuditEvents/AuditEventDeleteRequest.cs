
namespace LowlandTech.Accounts.Abstractions.AuditEvents;

public sealed class DeleteAuditEventRequest : IRequest<DeleteAuditEventResponse>
{
    [Required]
    public Guid Id { get; set; }
}

