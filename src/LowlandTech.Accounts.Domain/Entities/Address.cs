
namespace LowlandTech.Accounts.Domain.Entities;

public partial class Address
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
    public string? Line1 { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Line2 { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? City { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Region { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? PostalCode { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Country { get; set; } = string.Empty;
    
    public bool IsPrimary { get; set; }
    
    // FK: AccountId → UserAccount.Id
    public virtual UserAccount? UserAccount { get; set; }
}
