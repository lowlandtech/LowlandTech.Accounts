namespace LowlandTech.Accounts.Abstractions.Sessions;

public class CreateSessionRequest : IRequest<CreateSessionResponse>
{
    public Guid Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
    
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string DeviceId { get; set; } = string.Empty;
    
    public DateTime CreatedUtc { get; set; }
    
    public DateTime ExpiresUtc { get; set; }
    
    public DateTime RevokedUtc { get; set; }
}

