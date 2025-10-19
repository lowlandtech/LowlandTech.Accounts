
namespace LowlandTech.Accounts.Frontend.Components.PasswordResetTokens;

public static class PasswordResetTokenDataGridAdapter
{
    public static Func<GridStateVirtualize<PasswordResetTokenDto>, CancellationToken, Task<GridData<PasswordResetTokenDto>>> 
        CreatePasswordResetTokenServerData(PasswordResetTokenApiService apiService, PasswordResetTokenPageState pageState)
    {
        return async (gridState, ct) =>
        {
            try
            {
                // Extract pagination, sorting, and filtering from grid state
                var page = pageState.Page + 1; // MudBlazor uses 0-based, API might use 1-based
                var pageSize = pageState.PageSize;
                var searchString = gridState.FilterDefinitions?.FirstOrDefault()?.Value?.ToString();
                
                // Extract sorting
                string? sortBy = null;
                string? sortDir = null;
                if (gridState.SortDefinitions?.Any() == true)
                {
                    var sortDef = gridState.SortDefinitions.First();
                    sortBy = sortDef.SortBy;
                    sortDir = sortDef.Descending ? "desc" : "asc";
                }

                // Call the API
                var result = await apiService.ListAsync(
                    page, 
                    pageSize, 
                    searchString, 
                    sortBy, 
                    sortDir, 
                    ct
                );

                if (result is not null)
                {
                    return new GridData<PasswordResetTokenDto>
                    {
                        Items = result.Items,
                        TotalItems = result.TotalCount
                    };
                }
                else
                {
                    pageState.ToastError($"Failed to load passwordresettokens: No data returned");
                    return new GridData<PasswordResetTokenDto>
                    {
                        Items = [],
                        TotalItems = 0
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                pageState.ToastError($"HTTP error loading passwordresettokens: {ex.Message}");
                return new GridData<PasswordResetTokenDto>
                {
                    Items = [],
                    TotalItems = 0
                };
            }
            catch (Exception ex)
            {
                pageState.ToastError($"Error loading passwordresettokens: {ex.Message}");
                return new GridData<PasswordResetTokenDto>
                {
                    Items = [],
                    TotalItems = 0
                };
            }
        };
    }
}
