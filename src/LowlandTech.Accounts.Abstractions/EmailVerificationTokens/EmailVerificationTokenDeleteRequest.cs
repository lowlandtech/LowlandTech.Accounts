
namespace LowlandTech.Accounts.Abstractions.EmailVerificationTokens;

public sealed class DeleteEmailVerificationTokenRequest : IRequest<DeleteEmailVerificationTokenResponse>
{
    [Required]
    public Guid Id { get; set; }
}

