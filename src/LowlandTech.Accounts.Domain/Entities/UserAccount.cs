
namespace LowlandTech.Accounts.Domain.Entities;

public partial class UserAccount
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(250)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    [StringLength(250)]
    public string? Email { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? DisplayName { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Phone { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? PhotoUrl { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Timezone { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? Locale { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string? PreferredCurrency { get; set; } = string.Empty;
    
    public bool TwoFactorEnabled { get; set; }
    
    public DateTime? CreatedUtc { get; set; }
    
    public DateTime? LastLoginUtc { get; set; }
    
    public virtual ICollection<AccountPreference> Preferences { get; set; } = new HashSet<AccountPreference>();
    
    public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    
    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new HashSet<ApiKey>();
    
    public virtual ICollection<AuditEvent> AuditEvents { get; set; } = new HashSet<AuditEvent>();
    
    public virtual ICollection<AuthLogin> AuthLogins { get; set; } = new HashSet<AuthLogin>();
    
    public virtual ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    
    public virtual ICollection<EmailVerificationToken> EmailTokens { get; set; } = new HashSet<EmailVerificationToken>();
    
    public virtual ICollection<PasswordResetToken> ResetTokens { get; set; } = new HashSet<PasswordResetToken>();
    
    public virtual ICollection<RecoveryCode> RecoveryCodes { get; set; } = new HashSet<RecoveryCode>();
    
    public virtual ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
}
