
namespace LowlandTech.Accounts.Abstractions.AccountPreferences;

public sealed class DeleteAccountPreferenceResponse
{
    public Guid Id { get; init; }
    public bool Deleted { get; init; }
}

