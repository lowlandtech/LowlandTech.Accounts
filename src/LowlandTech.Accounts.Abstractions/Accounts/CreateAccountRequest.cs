namespace LowlandTech.Accounts.Abstractions.Accounts;

public class CreateAccountRequest : IRequest<CreateAccountResponse>
{
    public Guid Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
    
    [StringLength(250)]
    public string Email { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string DisplayName { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string Phone { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string PhotoUrl { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string Timezone { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string Locale { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string PreferredCurrency { get; set; } = string.Empty;
    
    public bool TwoFactorEnabled { get; set; }
    
    public DateTime CreatedUtc { get; set; }
    
    public DateTime LastLoginUtc { get; set; }
}

