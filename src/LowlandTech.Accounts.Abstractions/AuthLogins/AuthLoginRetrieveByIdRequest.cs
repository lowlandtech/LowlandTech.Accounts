
namespace LowlandTech.Accounts.Abstractions.AuthLogins;

public sealed class RetrieveAuthLoginByIdRequest : IRequest<RetrieveAuthLoginByIdResponse>
{
    public Guid Id { get; set; }
}

