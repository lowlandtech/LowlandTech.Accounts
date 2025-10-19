
namespace LowlandTech.Accounts.Abstractions.Accounts;

public sealed class DeleteAccountRequest : IRequest<DeleteAccountResponse>
{
    [Required]
    public Guid Id { get; set; }
}

