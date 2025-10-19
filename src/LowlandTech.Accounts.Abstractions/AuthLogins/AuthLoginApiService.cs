namespace LowlandTech.Accounts.Abstractions.AuthLogins;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for AuthLogin operations
/// </summary>
public sealed class AuthLoginApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthLoginApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public AuthLoginApiService(HttpClient httpClient, ILogger<AuthLoginApiService> logger)
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
    /// Retrieves a paged list of AuthLogin items
    /// </summary>
    public async Task<PagedResult<AuthLoginDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/authlogins?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<AuthLoginDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AuthLogin list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single AuthLogin by ID
    /// </summary>
    public async Task<AuthLoginDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/authlogins/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthLoginDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AuthLogin {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new AuthLogin
    /// </summary>
    public async Task<AuthLoginDto?> CreateAsync(AuthLoginDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/authlogins", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthLoginDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating AuthLogin");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing AuthLogin
    /// </summary>
    public async Task<AuthLoginDto?> UpdateAsync(AuthLoginDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/authlogins", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthLoginDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating AuthLogin");
            throw;
        }
    }

    /// <summary>
    /// Deletes a AuthLogin by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/authlogins/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting AuthLogin {Id}", id);
            throw;
        }
    }
}
