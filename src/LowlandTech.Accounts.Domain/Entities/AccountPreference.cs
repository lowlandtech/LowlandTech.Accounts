
namespace LowlandTech.Accounts.Domain.Entities;

public partial class AccountPreference
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string? Key { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Value { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? ValueType { get; set; } = string.Empty;
    
    // FK: AccountId → Account.Id
    public virtual Account Account { get; set; }
}
