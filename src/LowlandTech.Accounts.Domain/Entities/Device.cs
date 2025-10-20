
namespace LowlandTech.Accounts.Domain.Entities;

public partial class Device
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string? DeviceId { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? UserAgent { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Ip { get; set; } = string.Empty;
    
    public DateTime? FirstSeenUtc { get; set; }
    
    public DateTime? LastSeenUtc { get; set; }
    
    public bool IsTrusted { get; set; }
    
    // FK: AccountId → UserAccount.Id
    public virtual UserAccount UserAccount { get; set; }
}
