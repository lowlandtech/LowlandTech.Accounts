
namespace LowlandTech.Accounts.Abstractions.RecoveryCodes;

public sealed class RetrieveAllRecoveryCodeResponse
{
    public IReadOnlyList<RecoveryCodeDto> Items { get; init; } = Array.Empty<RecoveryCodeDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

