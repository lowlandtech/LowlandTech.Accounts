
namespace LowlandTech.Accounts.Abstractions.AccountPreferences;

public partial class AccountPreferenceDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? Key { get; set; } = string.Empty;
    
    public string? Value { get; set; } = string.Empty;
    
    public string? ValueType { get; set; } = string.Empty;
}
