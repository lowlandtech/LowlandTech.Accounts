
namespace LowlandTech.Accounts.Abstractions.Addresses;

public partial class AddressDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? Kind { get; set; } = string.Empty;
    
    public string? Line1 { get; set; } = string.Empty;
    
    public string? Line2 { get; set; } = string.Empty;
    
    public string? City { get; set; } = string.Empty;
    
    public string? Region { get; set; } = string.Empty;
    
    public string? PostalCode { get; set; } = string.Empty;
    
    public string? Country { get; set; } = string.Empty;
    
    public bool IsPrimary { get; set; }
}
