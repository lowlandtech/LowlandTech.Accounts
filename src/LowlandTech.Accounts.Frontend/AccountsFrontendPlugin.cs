namespace LowlandTech.Accounts.Frontend;

public sealed class AccountsFrontendPlugin
{
    // The host Shell will call this to add services for the plugin Frontend.
    public static void Install(ServiceRegistry services, IConfiguration config)
    {
        // MudBlazor
        services.AddMudServices();

        // MediatR (scan this assembly)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AccountsFrontendPlugin).Assembly));

        // App-wide state (scoped to circuit) and page state
        services.AddScoped<AppState>();

        // Table stores as singletons
        services.AddSingleton<AccountPageState>();
        services.AddSingleton<AccountPreferencePageState>();
        services.AddSingleton<AddressPageState>();
        services.AddSingleton<ApiKeyPageState>();
        services.AddSingleton<AuditEventPageState>();
        services.AddSingleton<AuthLoginPageState>();
        services.AddSingleton<DevicePageState>();
        services.AddSingleton<EmailVerificationTokenPageState>();
        services.AddSingleton<PasswordResetTokenPageState>();
        services.AddSingleton<RecoveryCodePageState>();
        services.AddSingleton<SessionPageState>();

        // API services from Abstractions
        var baseAddress = config["WebApi:BaseAddress"] ?? "/";
        services.AddPluginApiServices(baseAddress);

        // Authorization: default policy mapping is in the Shell; we only read claims via IPermService.
        services.AddScoped<IPermService, PermService>();
    }
}
