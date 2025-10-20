
using Microsoft.Extensions.DependencyInjection;

namespace LowlandTech.Accounts.Abstractions;

/// <summary>
/// Extension methods for registering API services
/// </summary>
public static class ApiServicesRegistration
{
    /// <summary>
    /// Registers all API services with the specified base address
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="baseAddress">Base URL for the API (e.g., "https://api.example.com" or "/" for relative)</param>
    public static IServiceCollection AddPluginApiServices(this IServiceCollection services, string baseAddress)
    {
        var baseUri = new Uri(baseAddress, UriKind.RelativeOrAbsolute);

        // Register AccountPreferenceApiService
        services.AddHttpClient<AccountPreferences.AccountPreferenceApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register AddressApiService
        services.AddHttpClient<Addresses.AddressApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register ApiKeyApiService
        services.AddHttpClient<ApiKeys.ApiKeyApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register AuditEventApiService
        services.AddHttpClient<AuditEvents.AuditEventApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register AuthLoginApiService
        services.AddHttpClient<AuthLogins.AuthLoginApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register DeviceApiService
        services.AddHttpClient<Devices.DeviceApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register EmailVerificationTokenApiService
        services.AddHttpClient<EmailVerificationTokens.EmailVerificationTokenApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register PasswordResetTokenApiService
        services.AddHttpClient<PasswordResetTokens.PasswordResetTokenApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register RecoveryCodeApiService
        services.AddHttpClient<RecoveryCodes.RecoveryCodeApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register SessionApiService
        services.AddHttpClient<Sessions.SessionApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        // Register UserAccountApiService
        services.AddHttpClient<UserAccounts.UserAccountApiService>(client =>
        {
            if (baseUri.IsAbsoluteUri)
                client.BaseAddress = baseUri;
            else
                client.BaseAddress = new Uri(baseAddress, UriKind.Relative);
        });

        return services;
    }
}
