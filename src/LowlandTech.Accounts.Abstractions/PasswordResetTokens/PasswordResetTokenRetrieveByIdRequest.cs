
namespace LowlandTech.Accounts.Abstractions.PasswordResetTokens;

public sealed class RetrievePasswordResetTokenByIdRequest : IRequest<RetrievePasswordResetTokenByIdResponse>
{
    public Guid Id { get; set; }
}

