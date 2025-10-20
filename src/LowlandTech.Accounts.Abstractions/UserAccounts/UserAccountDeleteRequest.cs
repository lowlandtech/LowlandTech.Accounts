
namespace LowlandTech.Accounts.Abstractions.UserAccounts;

public sealed class DeleteUserAccountRequest : IRequest<DeleteUserAccountResponse>
{
    [Required]
    public Guid Id { get; set; }
}

