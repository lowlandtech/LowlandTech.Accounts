
namespace LowlandTech.Accounts.Abstractions.RecoveryCodes;

public sealed class RetrieveRecoveryCodeByIdRequest : IRequest<RetrieveRecoveryCodeByIdResponse>
{
    public Guid Id { get; set; }
}

