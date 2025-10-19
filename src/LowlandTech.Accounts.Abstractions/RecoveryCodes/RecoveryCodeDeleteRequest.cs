
namespace LowlandTech.Accounts.Abstractions.RecoveryCodes;

public sealed class DeleteRecoveryCodeRequest : IRequest<DeleteRecoveryCodeResponse>
{
    [Required]
    public Guid Id { get; set; }
}

