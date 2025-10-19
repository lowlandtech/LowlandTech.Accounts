
namespace LowlandTech.Accounts.Abstractions.Devices;

public partial class DeviceDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? DeviceId { get; set; } = string.Empty;
    
    public string? UserAgent { get; set; } = string.Empty;
    
    public string? Ip { get; set; } = string.Empty;
    
    public DateTime? FirstSeenUtc { get; set; }
    
    public DateTime? LastSeenUtc { get; set; }
    
    public bool IsTrusted { get; set; }
}
