
namespace LowlandTech.Accounts.Abstractions.ApiKeys;

public sealed class RetrieveApiKeyByIdRequest : IRequest<RetrieveApiKeyByIdResponse>
{
    public Guid Id { get; set; }
}

