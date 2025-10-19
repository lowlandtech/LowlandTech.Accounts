
namespace LowlandTech.Accounts.Abstractions.Addresses;

public sealed class RetrieveAllAddressRequest : IRequest<RetrieveAllAddressResponse>
{
    /// <summary>1-based page index. Defaults to 1.</summary>
    public int Page { get; set; } = 1;

    /// <summary>Page size. Defaults to 50.</summary>
    public int PageSize { get; set; } = 50;

    /// <summary>Optional free-text search.</summary>
    public string? Search { get; set; }
}

