namespace LowlandTech.Accounts.Abstractions.AccountPreferences;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for AccountPreference operations
/// </summary>
public sealed class AccountPreferenceApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AccountPreferenceApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public AccountPreferenceApiService(HttpClient httpClient, ILogger<AccountPreferenceApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    /// <summary>
    /// Retrieves a paged list of AccountPreference items
    /// </summary>
    public async Task<PagedResult<AccountPreferenceDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/accountpreferences?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<AccountPreferenceDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AccountPreference list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single AccountPreference by ID
    /// </summary>
    public async Task<AccountPreferenceDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/accountpreferences/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AccountPreferenceDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AccountPreference {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new AccountPreference
    /// </summary>
    public async Task<AccountPreferenceDto?> CreateAsync(AccountPreferenceDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/accountpreferences", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AccountPreferenceDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating AccountPreference");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing AccountPreference
    /// </summary>
    public async Task<AccountPreferenceDto?> UpdateAsync(AccountPreferenceDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/accountpreferences", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AccountPreferenceDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating AccountPreference");
            throw;
        }
    }

    /// <summary>
    /// Deletes a AccountPreference by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/accountpreferences/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting AccountPreference {Id}", id);
            throw;
        }
    }
}
