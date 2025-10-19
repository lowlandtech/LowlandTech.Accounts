
namespace LowlandTech.Accounts.Abstractions.AuthLogins;

public partial class AuthLoginDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? Provider { get; set; } = string.Empty;
    
    public string? ProviderUserId { get; set; } = string.Empty;
    
    public string? AccessToken { get; set; } = string.Empty;
    
    public string? RefreshToken { get; set; } = string.Empty;
    
    public DateTime? ExpiresUtc { get; set; }
    
    public string? Scopes { get; set; } = string.Empty;
}
