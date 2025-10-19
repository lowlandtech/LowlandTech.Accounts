namespace LowlandTech.Accounts.Abstractions.AuditEvents;

public class CreateAuditEventRequest : IRequest<CreateAuditEventResponse>
{
    public Guid Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string Kind { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string DataJson { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string DeviceId { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string Ip { get; set; } = string.Empty;
    
    public DateTime CreatedUtc { get; set; }
}

