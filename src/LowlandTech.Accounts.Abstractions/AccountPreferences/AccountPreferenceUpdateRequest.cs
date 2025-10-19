
namespace LowlandTech.Accounts.Abstractions.AccountPreferences;

public class UpdateAccountPreferenceRequest : IRequest<UpdateAccountPreferenceResponse>
{
    [Required]
    public Guid Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Guid AccountId { get; set; }
    [StringLength(250)]
    public string Key { get; set; } = string.Empty;
    [StringLength(250)]
    public string Value { get; set; } = string.Empty;
    [StringLength(250)]
    public string ValueType { get; set; } = string.Empty;
}

