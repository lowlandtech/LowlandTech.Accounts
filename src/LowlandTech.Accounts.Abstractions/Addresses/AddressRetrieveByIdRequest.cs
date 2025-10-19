
namespace LowlandTech.Accounts.Abstractions.Addresses;

public sealed class RetrieveAddressByIdRequest : IRequest<RetrieveAddressByIdResponse>
{
    public Guid Id { get; set; }
}

