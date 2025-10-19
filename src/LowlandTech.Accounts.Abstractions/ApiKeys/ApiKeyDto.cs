
namespace LowlandTech.Accounts.Abstractions.ApiKeys;

public partial class ApiKeyDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public DateTime? CreatedUtc { get; set; }
    
    public string? Key { get; set; } = string.Empty;
    
    public string? KeyHash { get; set; } = string.Empty;
    
    public string? KeyPrefix { get; set; } = string.Empty;
    
    public DateTime? LastUsedUtc { get; set; }
    
    public DateTime? RevokedUtc { get; set; }
}
