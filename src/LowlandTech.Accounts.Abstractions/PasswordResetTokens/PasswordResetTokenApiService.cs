namespace LowlandTech.Accounts.Abstractions.PasswordResetTokens;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for PasswordResetToken operations
/// </summary>
public sealed class PasswordResetTokenApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PasswordResetTokenApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public PasswordResetTokenApiService(HttpClient httpClient, ILogger<PasswordResetTokenApiService> logger)
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
    /// Retrieves a paged list of PasswordResetToken items
    /// </summary>
    public async Task<PagedResult<PasswordResetTokenDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/passwordresettokens?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<PasswordResetTokenDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving PasswordResetToken list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single PasswordResetToken by ID
    /// </summary>
    public async Task<PasswordResetTokenDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/passwordresettokens/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PasswordResetTokenDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving PasswordResetToken {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new PasswordResetToken
    /// </summary>
    public async Task<PasswordResetTokenDto?> CreateAsync(PasswordResetTokenDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/passwordresettokens", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PasswordResetTokenDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating PasswordResetToken");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing PasswordResetToken
    /// </summary>
    public async Task<PasswordResetTokenDto?> UpdateAsync(PasswordResetTokenDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/passwordresettokens", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PasswordResetTokenDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating PasswordResetToken");
            throw;
        }
    }

    /// <summary>
    /// Deletes a PasswordResetToken by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/passwordresettokens/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting PasswordResetToken {Id}", id);
            throw;
        }
    }
}
