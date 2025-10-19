namespace LowlandTech.Accounts.Abstractions.ApiKeys;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for ApiKey operations
/// </summary>
public sealed class ApiKeyApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiKeyApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiKeyApiService(HttpClient httpClient, ILogger<ApiKeyApiService> logger)
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
    /// Retrieves a paged list of ApiKey items
    /// </summary>
    public async Task<PagedResult<ApiKeyDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/apikeys?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<ApiKeyDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving ApiKey list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single ApiKey by ID
    /// </summary>
    public async Task<ApiKeyDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/apikeys/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ApiKeyDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving ApiKey {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new ApiKey
    /// </summary>
    public async Task<ApiKeyDto?> CreateAsync(ApiKeyDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/apikeys", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ApiKeyDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating ApiKey");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing ApiKey
    /// </summary>
    public async Task<ApiKeyDto?> UpdateAsync(ApiKeyDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/apikeys", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ApiKeyDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating ApiKey");
            throw;
        }
    }

    /// <summary>
    /// Deletes a ApiKey by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/apikeys/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting ApiKey {Id}", id);
            throw;
        }
    }
}
