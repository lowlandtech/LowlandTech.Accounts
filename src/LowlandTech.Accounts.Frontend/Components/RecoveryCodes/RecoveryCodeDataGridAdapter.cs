
namespace LowlandTech.Accounts.Frontend.Components.RecoveryCodes;

public static class RecoveryCodeDataGridAdapter
{
    public static Func<GridStateVirtualize<RecoveryCodeDto>, CancellationToken, Task<GridData<RecoveryCodeDto>>> 
        CreateRecoveryCodeServerData(IRecoveryCodeApi api, RecoveryCodePageState pageState)
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
                var response = await api.ListAsync(
                    page, 
                    pageSize, 
                    searchString, 
                    sortBy, 
                    sortDir, 
                    ct
                );

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    return new GridData<RecoveryCodeDto>
                    {
                        Items = response.Content.Items,
                        TotalItems = response.Content.TotalCount
                    };
                }
                else
                {
                    pageState.ToastError($"Failed to load recoverycodes: {response.Error?.Content}");
                    return new GridData<RecoveryCodeDto>
                    {
                        Items = [],
                        TotalItems = 0
                    };
                }
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
