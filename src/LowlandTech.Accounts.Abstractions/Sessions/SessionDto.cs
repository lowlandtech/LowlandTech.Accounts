
namespace LowlandTech.Accounts.Abstractions.Sessions;

public partial class SessionDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? DeviceId { get; set; } = string.Empty;
    
    public DateTime? CreatedUtc { get; set; }
    
    public DateTime? ExpiresUtc { get; set; }
    
    public DateTime? RevokedUtc { get; set; }
}
