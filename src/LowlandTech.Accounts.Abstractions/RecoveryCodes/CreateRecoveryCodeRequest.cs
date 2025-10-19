namespace LowlandTech.Accounts.Abstractions.RecoveryCodes;

public class CreateRecoveryCodeRequest : IRequest<CreateRecoveryCodeResponse>
{
    public Guid AccountId { get; set; }
    
    [StringLength(250)]
    public string Code { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string CodeHash { get; set; } = string.Empty;
    
    public Guid Id { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    
    public DateTime UsedUtc { get; set; }
}

