
namespace LowlandTech.Accounts.Abstractions.Sessions;

public sealed class RetrieveSessionByIdRequest : IRequest<RetrieveSessionByIdResponse>
{
    public Guid Id { get; set; }
}

