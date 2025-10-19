namespace LowlandTech.Accounts.Abstractions.RecoveryCodes;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for RecoveryCode operations
/// </summary>
public sealed class RecoveryCodeApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RecoveryCodeApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public RecoveryCodeApiService(HttpClient httpClient, ILogger<RecoveryCodeApiService> logger)
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
    /// Retrieves a paged list of RecoveryCode items
    /// </summary>
    public async Task<PagedResult<RecoveryCodeDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/recoverycodes?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<RecoveryCodeDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving RecoveryCode list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single RecoveryCode by ID
    /// </summary>
    public async Task<RecoveryCodeDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/recoverycodes/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RecoveryCodeDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving RecoveryCode {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new RecoveryCode
    /// </summary>
    public async Task<RecoveryCodeDto?> CreateAsync(RecoveryCodeDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/recoverycodes", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RecoveryCodeDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating RecoveryCode");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing RecoveryCode
    /// </summary>
    public async Task<RecoveryCodeDto?> UpdateAsync(RecoveryCodeDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/recoverycodes", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RecoveryCodeDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating RecoveryCode");
            throw;
        }
    }

    /// <summary>
    /// Deletes a RecoveryCode by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/recoverycodes/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting RecoveryCode {Id}", id);
            throw;
        }
    }
}
