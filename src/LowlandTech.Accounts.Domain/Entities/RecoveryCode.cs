
namespace LowlandTech.Accounts.Domain.Entities;

public partial class RecoveryCode
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string? Code { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? CodeHash { get; set; } = string.Empty;
    
    public DateTime? UsedUtc { get; set; }
    
    // FK: AccountId → Account.Id
    public virtual Account Account { get; set; }
}
