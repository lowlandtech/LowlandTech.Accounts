
namespace LowlandTech.Accounts.Abstractions.AuditEvents;

public sealed class RetrieveAuditEventByIdRequest : IRequest<RetrieveAuditEventByIdResponse>
{
    public Guid Id { get; set; }
}

