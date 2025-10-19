
namespace LowlandTech.Accounts.Abstractions.AuditEvents;

public partial class AuditEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? Kind { get; set; } = string.Empty;
    
    public string? DataJson { get; set; } = string.Empty;
    
    public string? DeviceId { get; set; } = string.Empty;
    
    public string? Ip { get; set; } = string.Empty;
    
    public DateTime? CreatedUtc { get; set; }
}
