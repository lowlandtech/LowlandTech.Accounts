namespace LowlandTech.Accounts.Abstractions.Devices;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for Device operations
/// </summary>
public sealed class DeviceApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DeviceApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public DeviceApiService(HttpClient httpClient, ILogger<DeviceApiService> logger)
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
    /// Retrieves a paged list of Device items
    /// </summary>
    public async Task<PagedResult<DeviceDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/devices?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<DeviceDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Device list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single Device by ID
    /// </summary>
    public async Task<DeviceDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/devices/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DeviceDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Device {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new Device
    /// </summary>
    public async Task<DeviceDto?> CreateAsync(DeviceDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/devices", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DeviceDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Device");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing Device
    /// </summary>
    public async Task<DeviceDto?> UpdateAsync(DeviceDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/devices", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DeviceDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Device");
            throw;
        }
    }

    /// <summary>
    /// Deletes a Device by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/devices/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting Device {Id}", id);
            throw;
        }
    }
}
