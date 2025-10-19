
namespace LowlandTech.Accounts.Frontend.Infrastructure.Auth;

public interface IPermService
{
    ValueTask<bool> CanAsync(string perm);
    ValueTask<bool> CanAnyAsync(params string[] perms);
}

public sealed class PermService(AuthenticationStateProvider auth) : IPermService
{
    private readonly AuthenticationStateProvider _auth = auth;

    public async ValueTask<bool> CanAsync(string perm)
    {
        var state = await _auth.GetAuthenticationStateAsync();
        var user = state.User;
        if (user?.Identity?.IsAuthenticated != true) return false;
        return user.HasClaim("perm", perm);
    }

    public async ValueTask<bool> CanAnyAsync(params string[] perms)
    {
        var state = await _auth.GetAuthenticationStateAsync();
        var user = state.User;
        if (user?.Identity?.IsAuthenticated != true) return false;
        if (perms is null || perms.Length == 0) return false;
        foreach (var p in perms) if (user.HasClaim("perm", p)) return true;
        return false;
    }
}

public static class PermExtensions
{
    public static string Read(string table) => $"{table}::read";
    public static string Create(string table) => $"{table}::create";
    public static string Update(string table) => $"{table}::update";
    public static string Delete(string table) => $"{table}::delete";
}
