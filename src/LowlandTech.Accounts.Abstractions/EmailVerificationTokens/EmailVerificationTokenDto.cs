
namespace LowlandTech.Accounts.Abstractions.EmailVerificationTokens;

public partial class EmailVerificationTokenDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? Token { get; set; } = string.Empty;
    
    public DateTime? ExpiresUtc { get; set; }
    
    public DateTime? UsedUtc { get; set; }
}
