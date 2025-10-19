
namespace LowlandTech.Accounts.Domain.Entities;

public partial class AuditEvent
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string? Kind { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? DataJson { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? DeviceId { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Ip { get; set; } = string.Empty;
    
    public DateTime? CreatedUtc { get; set; }
    
    // FK: AccountId → Account.Id
    public virtual Account? Account { get; set; }
}
