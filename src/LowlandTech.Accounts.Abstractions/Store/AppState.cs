namespace LowlandTech.Accounts.Abstractions.Store;

public sealed class AppState
{
    private readonly ISnackbar _snackbar;
    public AppState(ISnackbar snackbar) => _snackbar = snackbar;

    public void ToastInfo(string message)    => _snackbar.Add(message, Severity.Info);
    public void ToastSuccess(string message) => _snackbar.Add(message, Severity.Success);
    public void ToastWarn(string message)    => _snackbar.Add(message, Severity.Warning);
    public void ToastError(string message)   => _snackbar.Add(message, Severity.Error);
}
