
namespace LowlandTech.Accounts.Abstractions.AuthLogins;

public sealed class DeleteAuthLoginRequest : IRequest<DeleteAuthLoginResponse>
{
    [Required]
    public Guid Id { get; set; }
}

