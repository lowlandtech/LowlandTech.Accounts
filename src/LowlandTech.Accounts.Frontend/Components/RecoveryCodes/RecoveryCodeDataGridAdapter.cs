
namespace LowlandTech.Accounts.Frontend.Components.RecoveryCodes;

public static class RecoveryCodeDataGridAdapter
{
    public static Func<GridStateVirtualize<RecoveryCodeDto>, CancellationToken, Task<GridData<RecoveryCodeDto>>> 
        CreateRecoveryCodeServerData(RecoveryCodeApiService apiService, RecoveryCodePageState pageState)
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
                    return new GridData<RecoveryCodeDto>
                    {
                        Items = result.Items,
                        TotalItems = result.TotalCount
                    };
                }
                else
                {
                    pageState.ToastError($"Failed to load recoverycodes: No data returned");
                    return new GridData<RecoveryCodeDto>
                    {
                        Items = [],
                        TotalItems = 0
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                pageState.ToastError($"HTTP error loading recoverycodes: {ex.Message}");
                return new GridData<RecoveryCodeDto>
                {
                    Items = [],
                    TotalItems = 0
                };
            }
            catch (Exception ex)
            {
                pageState.ToastError($"Error loading recoverycodes: {ex.Message}");
                return new GridData<RecoveryCodeDto>
                {
                    Items = [],
                    TotalItems = 0
                };
            }
        };
    }
}
