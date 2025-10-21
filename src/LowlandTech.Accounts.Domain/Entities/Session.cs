
namespace LowlandTech.Accounts.Domain.Entities;

public partial class Session
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
    
    public DateTime? CreatedUtc { get; set; }
    
    public DateTime? ExpiresUtc { get; set; }
    
    public DateTime? RevokedUtc { get; set; }
    
    // FK: AccountId → UserAccount.Id
    public virtual UserAccount? UserAccount { get; set; }
}
