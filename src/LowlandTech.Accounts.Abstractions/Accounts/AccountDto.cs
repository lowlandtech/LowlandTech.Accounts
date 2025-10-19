
namespace LowlandTech.Accounts.Abstractions.Accounts;

public partial class AccountDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public string? Email { get; set; } = string.Empty;
    
    public string? DisplayName { get; set; } = string.Empty;
    
    public string? Phone { get; set; } = string.Empty;
    
    public string? PhotoUrl { get; set; } = string.Empty;
    
    public string? Timezone { get; set; } = string.Empty;
    
    public string? Locale { get; set; } = string.Empty;
    
    public string? PreferredCurrency { get; set; } = string.Empty;
    
    public bool TwoFactorEnabled { get; set; }
    
    public DateTime? CreatedUtc { get; set; }
    
    public DateTime? LastLoginUtc { get; set; }
}
