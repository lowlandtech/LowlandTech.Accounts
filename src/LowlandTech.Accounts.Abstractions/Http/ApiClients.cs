
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace LowlandTech.Accounts.Abstractions.Http;

/// <summary>
/// Paged result wrapper for API responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// Refit API client interface for Account
/// </summary>
[Headers("Accept: application/json")]
public interface IAccountApi
{
    [Get("/api/account")]
    Task<ApiResponse<PagedResult<AccountDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/account/{id}")]
    Task<ApiResponse<AccountDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/account")]
    Task<ApiResponse<AccountDto>> CreateAsync([Body] AccountDto model, CancellationToken ct = default);

    [Put("/api/account")]
    Task<ApiResponse<AccountDto>> UpdateAsync([Body] AccountDto model, CancellationToken ct = default);

    [Delete("/api/account/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for AccountPreference
/// </summary>
[Headers("Accept: application/json")]
public interface IAccountPreferenceApi
{
    [Get("/api/accountpreference")]
    Task<ApiResponse<PagedResult<AccountPreferenceDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/accountpreference/{id}")]
    Task<ApiResponse<AccountPreferenceDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/accountpreference")]
    Task<ApiResponse<AccountPreferenceDto>> CreateAsync([Body] AccountPreferenceDto model, CancellationToken ct = default);

    [Put("/api/accountpreference")]
    Task<ApiResponse<AccountPreferenceDto>> UpdateAsync([Body] AccountPreferenceDto model, CancellationToken ct = default);

    [Delete("/api/accountpreference/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for Address
/// </summary>
[Headers("Accept: application/json")]
public interface IAddressApi
{
    [Get("/api/address")]
    Task<ApiResponse<PagedResult<AddressDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/address/{id}")]
    Task<ApiResponse<AddressDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/address")]
    Task<ApiResponse<AddressDto>> CreateAsync([Body] AddressDto model, CancellationToken ct = default);

    [Put("/api/address")]
    Task<ApiResponse<AddressDto>> UpdateAsync([Body] AddressDto model, CancellationToken ct = default);

    [Delete("/api/address/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for ApiKey
/// </summary>
[Headers("Accept: application/json")]
public interface IApiKeyApi
{
    [Get("/api/apikey")]
    Task<ApiResponse<PagedResult<ApiKeyDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/apikey/{id}")]
    Task<ApiResponse<ApiKeyDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/apikey")]
    Task<ApiResponse<ApiKeyDto>> CreateAsync([Body] ApiKeyDto model, CancellationToken ct = default);

    [Put("/api/apikey")]
    Task<ApiResponse<ApiKeyDto>> UpdateAsync([Body] ApiKeyDto model, CancellationToken ct = default);

    [Delete("/api/apikey/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for AuditEvent
/// </summary>
[Headers("Accept: application/json")]
public interface IAuditEventApi
{
    [Get("/api/auditevent")]
    Task<ApiResponse<PagedResult<AuditEventDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/auditevent/{id}")]
    Task<ApiResponse<AuditEventDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/auditevent")]
    Task<ApiResponse<AuditEventDto>> CreateAsync([Body] AuditEventDto model, CancellationToken ct = default);

    [Put("/api/auditevent")]
    Task<ApiResponse<AuditEventDto>> UpdateAsync([Body] AuditEventDto model, CancellationToken ct = default);

    [Delete("/api/auditevent/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for AuthLogin
/// </summary>
[Headers("Accept: application/json")]
public interface IAuthLoginApi
{
    [Get("/api/authlogin")]
    Task<ApiResponse<PagedResult<AuthLoginDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/authlogin/{id}")]
    Task<ApiResponse<AuthLoginDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/authlogin")]
    Task<ApiResponse<AuthLoginDto>> CreateAsync([Body] AuthLoginDto model, CancellationToken ct = default);

    [Put("/api/authlogin")]
    Task<ApiResponse<AuthLoginDto>> UpdateAsync([Body] AuthLoginDto model, CancellationToken ct = default);

    [Delete("/api/authlogin/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for Device
/// </summary>
[Headers("Accept: application/json")]
public interface IDeviceApi
{
    [Get("/api/device")]
    Task<ApiResponse<PagedResult<DeviceDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/device/{id}")]
    Task<ApiResponse<DeviceDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/device")]
    Task<ApiResponse<DeviceDto>> CreateAsync([Body] DeviceDto model, CancellationToken ct = default);

    [Put("/api/device")]
    Task<ApiResponse<DeviceDto>> UpdateAsync([Body] DeviceDto model, CancellationToken ct = default);

    [Delete("/api/device/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for EmailVerificationToken
/// </summary>
[Headers("Accept: application/json")]
public interface IEmailVerificationTokenApi
{
    [Get("/api/emailverificationtoken")]
    Task<ApiResponse<PagedResult<EmailVerificationTokenDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/emailverificationtoken/{id}")]
    Task<ApiResponse<EmailVerificationTokenDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/emailverificationtoken")]
    Task<ApiResponse<EmailVerificationTokenDto>> CreateAsync([Body] EmailVerificationTokenDto model, CancellationToken ct = default);

    [Put("/api/emailverificationtoken")]
    Task<ApiResponse<EmailVerificationTokenDto>> UpdateAsync([Body] EmailVerificationTokenDto model, CancellationToken ct = default);

    [Delete("/api/emailverificationtoken/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for PasswordResetToken
/// </summary>
[Headers("Accept: application/json")]
public interface IPasswordResetTokenApi
{
    [Get("/api/passwordresettoken")]
    Task<ApiResponse<PagedResult<PasswordResetTokenDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/passwordresettoken/{id}")]
    Task<ApiResponse<PasswordResetTokenDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/passwordresettoken")]
    Task<ApiResponse<PasswordResetTokenDto>> CreateAsync([Body] PasswordResetTokenDto model, CancellationToken ct = default);

    [Put("/api/passwordresettoken")]
    Task<ApiResponse<PasswordResetTokenDto>> UpdateAsync([Body] PasswordResetTokenDto model, CancellationToken ct = default);

    [Delete("/api/passwordresettoken/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for RecoveryCode
/// </summary>
[Headers("Accept: application/json")]
public interface IRecoveryCodeApi
{
    [Get("/api/recoverycode")]
    Task<ApiResponse<PagedResult<RecoveryCodeDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/recoverycode/{id}")]
    Task<ApiResponse<RecoveryCodeDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/recoverycode")]
    Task<ApiResponse<RecoveryCodeDto>> CreateAsync([Body] RecoveryCodeDto model, CancellationToken ct = default);

    [Put("/api/recoverycode")]
    Task<ApiResponse<RecoveryCodeDto>> UpdateAsync([Body] RecoveryCodeDto model, CancellationToken ct = default);

    [Delete("/api/recoverycode/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Refit API client interface for Session
/// </summary>
[Headers("Accept: application/json")]
public interface ISessionApi
{
    [Get("/api/session")]
    Task<ApiResponse<PagedResult<SessionDto>>> ListAsync([Query] int page, [Query] int pageSize, [Query] string? q = null, [Query] string? sort = null, [Query] string? dir = null, CancellationToken ct = default);

    [Get("/api/session/{id}")]
    Task<ApiResponse<SessionDto?>> GetAsync(Guid id, CancellationToken ct = default);

    [Post("/api/session")]
    Task<ApiResponse<SessionDto>> CreateAsync([Body] SessionDto model, CancellationToken ct = default);

    [Put("/api/session")]
    Task<ApiResponse<SessionDto>> UpdateAsync([Body] SessionDto model, CancellationToken ct = default);

    [Delete("/api/session/{id}")]
    Task<ApiResponse<object>> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Extension methods for registering API clients
/// </summary>
public static class ApiClientsRegistration
{
    /// <summary>
    /// Registers all Refit API clients with the specified base address
    /// </summary>
    public static IServiceCollection AddPluginApiClients(this IServiceCollection services, string baseAddress)
    {
        var settings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            })
        };

        services.AddRefitClient<IAccountApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IAccountPreferenceApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IAddressApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IApiKeyApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IAuditEventApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IAuthLoginApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IDeviceApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IEmailVerificationTokenApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IPasswordResetTokenApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<IRecoveryCodeApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));
        services.AddRefitClient<ISessionApi>(settings).ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));

        return services;
    }
}
