
namespace LowlandTech.Accounts.Abstractions.UserAccounts;

public sealed class RetrieveUserAccountByIdRequest : IRequest<RetrieveUserAccountByIdResponse>
{
    public Guid Id { get; set; }
}

