
namespace LowlandTech.Accounts.Domain.Entities;

public partial class AuthLogin
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string? Provider { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? ProviderUserId { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? AccessToken { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? RefreshToken { get; set; } = string.Empty;
    
    public DateTime? ExpiresUtc { get; set; }
    
    [StringLength(250)]
    public string? Scopes { get; set; } = string.Empty;
    
    // FK: AccountId → Account.Id
    public virtual Account Account { get; set; }
}
