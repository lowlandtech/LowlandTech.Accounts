namespace LowlandTech.Accounts.Abstractions.Sessions;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for Session operations
/// </summary>
public sealed class SessionApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SessionApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public SessionApiService(HttpClient httpClient, ILogger<SessionApiService> logger)
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
    /// Retrieves a paged list of Session items
    /// </summary>
    public async Task<PagedResult<SessionDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/sessions?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<SessionDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Session list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single Session by ID
    /// </summary>
    public async Task<SessionDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/sessions/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SessionDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Session {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new Session
    /// </summary>
    public async Task<SessionDto?> CreateAsync(SessionDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/sessions", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SessionDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Session");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing Session
    /// </summary>
    public async Task<SessionDto?> UpdateAsync(SessionDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/sessions", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SessionDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Session");
            throw;
        }
    }

    /// <summary>
    /// Deletes a Session by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/sessions/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting Session {Id}", id);
            throw;
        }
    }
}
