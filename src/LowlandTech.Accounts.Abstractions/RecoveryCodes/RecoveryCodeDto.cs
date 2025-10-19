
namespace LowlandTech.Accounts.Abstractions.RecoveryCodes;

public partial class RecoveryCodeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? Code { get; set; } = string.Empty;
    
    public string? CodeHash { get; set; } = string.Empty;
    
    public DateTime? UsedUtc { get; set; }
}
