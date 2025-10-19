
namespace LowlandTech.Accounts.Abstractions.Sessions;

public sealed class DeleteSessionRequest : IRequest<DeleteSessionResponse>
{
    [Required]
    public Guid Id { get; set; }
}

