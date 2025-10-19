namespace LowlandTech.Accounts.Abstractions.ApiKeys;

public class CreateApiKeyRequest : IRequest<CreateApiKeyResponse>
{
    public Guid AccountId { get; set; }
    
    public DateTime CreatedUtc { get; set; }
    
    public Guid Id { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [StringLength(250)]
    public string Key { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string KeyHash { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string KeyPrefix { get; set; } = string.Empty;
    
    public DateTime LastUsedUtc { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    public DateTime RevokedUtc { get; set; }
}

