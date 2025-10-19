
namespace LowlandTech.Accounts.Abstractions.PasswordResetTokens;

public sealed class DeletePasswordResetTokenRequest : IRequest<DeletePasswordResetTokenResponse>
{
    [Required]
    public Guid Id { get; set; }
}

