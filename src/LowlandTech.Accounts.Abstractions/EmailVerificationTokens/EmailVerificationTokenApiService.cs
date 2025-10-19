namespace LowlandTech.Accounts.Abstractions.EmailVerificationTokens;

/// <summary>
/// Paged result wrapper for list responses
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
}

/// <summary>
/// API service for EmailVerificationToken operations
/// </summary>
public sealed class EmailVerificationTokenApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EmailVerificationTokenApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public EmailVerificationTokenApiService(HttpClient httpClient, ILogger<EmailVerificationTokenApiService> logger)
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
    /// Retrieves a paged list of EmailVerificationToken items
    /// </summary>
    public async Task<PagedResult<EmailVerificationTokenDto>?> ListAsync(
        int page = 1,
        int pageSize = 20,
        string? q = null,
        string? sort = null,
        string? dir = null,
        CancellationToken ct = default)
    {
        try
        {
            var query = $"/emailverificationtokens?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(q)) query += $"&q={Uri.EscapeDataString(q)}";
            if (!string.IsNullOrWhiteSpace(sort)) query += $"&sort={Uri.EscapeDataString(sort)}";
            if (!string.IsNullOrWhiteSpace(dir)) query += $"&dir={Uri.EscapeDataString(dir)}";

            var response = await _httpClient.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<EmailVerificationTokenDto>>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving EmailVerificationToken list");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single EmailVerificationToken by ID
    /// </summary>
    public async Task<EmailVerificationTokenDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/emailverificationtokens/{id}", ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<EmailVerificationTokenDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving EmailVerificationToken {Id}", id);
            throw;
        }
    }

    /// <summary>
    /// Creates a new EmailVerificationToken
    /// </summary>
    public async Task<EmailVerificationTokenDto?> CreateAsync(EmailVerificationTokenDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/emailverificationtokens", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<EmailVerificationTokenDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating EmailVerificationToken");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing EmailVerificationToken
    /// </summary>
    public async Task<EmailVerificationTokenDto?> UpdateAsync(EmailVerificationTokenDto model, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync("/emailverificationtokens", model, _jsonOptions, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<EmailVerificationTokenDto>(_jsonOptions, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating EmailVerificationToken");
            throw;
        }
    }

    /// <summary>
    /// Deletes a EmailVerificationToken by ID
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/emailverificationtokens/{id}", ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting EmailVerificationToken {Id}", id);
            throw;
        }
    }
}
