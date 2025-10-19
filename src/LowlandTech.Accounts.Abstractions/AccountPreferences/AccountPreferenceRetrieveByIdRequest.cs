
namespace LowlandTech.Accounts.Abstractions.AccountPreferences;

public sealed class RetrieveAccountPreferenceByIdRequest : IRequest<RetrieveAccountPreferenceByIdResponse>
{
    public Guid Id { get; set; }
}

