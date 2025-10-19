
namespace LowlandTech.Accounts.Domain.Entities;

public partial class EmailVerificationToken
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string? Token { get; set; } = string.Empty;
    
    public DateTime? ExpiresUtc { get; set; }
    
    public DateTime? UsedUtc { get; set; }
    
    // FK: AccountId → Account.Id
    public virtual Account Account { get; set; }
}
