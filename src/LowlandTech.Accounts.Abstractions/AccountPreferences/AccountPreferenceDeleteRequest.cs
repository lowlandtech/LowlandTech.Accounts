
namespace LowlandTech.Accounts.Abstractions.AccountPreferences;

public sealed class DeleteAccountPreferenceRequest : IRequest<DeleteAccountPreferenceResponse>
{
    [Required]
    public Guid Id { get; set; }
}

