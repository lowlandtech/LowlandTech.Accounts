
namespace LowlandTech.Accounts.Abstractions.Accounts;

public sealed class RetrieveAccountByIdRequest : IRequest<RetrieveAccountByIdResponse>
{
    public Guid Id { get; set; }
}

