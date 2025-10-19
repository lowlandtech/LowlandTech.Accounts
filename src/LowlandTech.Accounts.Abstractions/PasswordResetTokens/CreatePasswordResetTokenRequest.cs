namespace LowlandTech.Accounts.Abstractions.PasswordResetTokens;

public class CreatePasswordResetTokenRequest : IRequest<CreatePasswordResetTokenResponse>
{
    public Guid Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string Token { get; set; } = string.Empty;
    
    public DateTime ExpiresUtc { get; set; }
    
    public DateTime UsedUtc { get; set; }
}

