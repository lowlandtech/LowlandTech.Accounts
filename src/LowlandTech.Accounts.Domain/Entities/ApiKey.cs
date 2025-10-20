
namespace LowlandTech.Accounts.Domain.Entities;

public partial class ApiKey
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    public DateTime? CreatedUtc { get; set; }
    
    [StringLength(250)]
    public string? Key { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? KeyHash { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? KeyPrefix { get; set; } = string.Empty;
    
    public DateTime? LastUsedUtc { get; set; }
    
    public DateTime? RevokedUtc { get; set; }
    
    // FK: AccountId → UserAccount.Id
    public virtual UserAccount UserAccount { get; set; }
}
