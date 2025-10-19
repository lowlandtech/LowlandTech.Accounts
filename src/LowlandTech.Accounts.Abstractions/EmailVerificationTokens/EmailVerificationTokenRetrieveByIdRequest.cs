
namespace LowlandTech.Accounts.Abstractions.EmailVerificationTokens;

public sealed class RetrieveEmailVerificationTokenByIdRequest : IRequest<RetrieveEmailVerificationTokenByIdResponse>
{
    public Guid Id { get; set; }
}

