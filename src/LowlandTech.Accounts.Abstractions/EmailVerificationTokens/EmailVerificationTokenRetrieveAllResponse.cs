
namespace LowlandTech.Accounts.Abstractions.EmailVerificationTokens;

public sealed class RetrieveAllEmailVerificationTokenResponse
{
    public IReadOnlyList<EmailVerificationTokenDto> Items { get; init; } = Array.Empty<EmailVerificationTokenDto>();
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

