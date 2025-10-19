
namespace LowlandTech.Accounts.Abstractions.ApiKeys;

public sealed class DeleteApiKeyRequest : IRequest<DeleteApiKeyResponse>
{
    [Required]
    public Guid Id { get; set; }
}

