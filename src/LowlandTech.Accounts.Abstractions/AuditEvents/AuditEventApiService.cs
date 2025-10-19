namespace LowlandTech.Accounts.Abstractions.AuditEvents;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for AuditEvent operations
/// </summary>
public sealed class AuditEventApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuditEventApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public AuditEventApiService(HttpClient httpClient, ILogger<AuditEventApiService> logger)
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
    /// Retrieves a paged list of AuditEvent items
    /// </summary>
    public async Task<PagedResult<AuditEventDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/auditevents?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<AuditEventDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AuditEvent list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single AuditEvent by ID
    /// </summary>
    public async Task<AuditEventDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/auditevents/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuditEventDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AuditEvent {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new AuditEvent
    /// </summary>
    public async Task<AuditEventDto?> CreateAsync(AuditEventDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/auditevents", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuditEventDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating AuditEvent");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing AuditEvent
    /// </summary>
    public async Task<AuditEventDto?> UpdateAsync(AuditEventDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/auditevents", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuditEventDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating AuditEvent");
            throw;
        }
    }

    /// <summary>
    /// Deletes a AuditEvent by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/auditevents/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting AuditEvent {Id}", id);
            throw;
        }
    }
}
