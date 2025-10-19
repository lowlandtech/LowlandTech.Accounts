namespace LowlandTech.Accounts.Frontend.Components.ApiKeys;

public partial class ApiKeyPage : ComponentBase, IDisposable
{
    [Inject] private AppState App { get; set; } = default!;
    [Inject] private IPermService Perms { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private ApiKeyPageState PageState { get; set; } = default!;
    [Inject] private ApiKeyApiService ApiService { get; set; } = default!;

    private Func<GridStateVirtualize<ApiKeyDto>, CancellationToken, Task<GridData<ApiKeyDto>>> _serverData = default!;
    private bool _canCreate;

    protected override async Task OnInitializedAsync()
    {
        _serverData = ApiKeyDataGridAdapter.CreateApiKeyServerData(ApiService, PageState);
        PageState.ItemsChanged += OnApiKeyItemsChanged;
        _canCreate = await Perms.CanAsync("accounts::create");
        PageState.Changed += OnPagePrefsChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        try
        {
            var dense   = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::ApiKeyPage::Dense");
            var reorder = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::ApiKeyPage::Reorder");
            var dropCls = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::ApiKeyPage::DropCls");
            var dragIcn = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::ApiKeyPage::DragIcon");
            if (bool.TryParse(dense, out var d)) PageState.Dense = d;
            if (bool.TryParse(reorder, out var r)) PageState.DragDropColumnReordering = r;
            if (bool.TryParse(dropCls, out var a)) PageState.ApplyDropClassesOnDragStarted = a;
            if (!string.IsNullOrWhiteSpace(dragIcn)) PageState.DragIndicatorIcon = dragIcn;
        }
        catch { }
        await PageState.Load();
        StateHasChanged();
    }

    private async void OnPagePrefsChanged()
    {
        try
        {
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::ApiKeyPage::Dense", PageState.Dense.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::ApiKeyPage::Reorder", PageState.DragDropColumnReordering.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::ApiKeyPage::DropCls", PageState.ApplyDropClassesOnDragStarted.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::ApiKeyPage::DragIcon", PageState.DragIndicatorIcon ?? string.Empty);
        }
        catch { }
        await InvokeAsync(StateHasChanged);
    }

    private void OnApiKeyItemsChanged() => InvokeAsync(StateHasChanged);
    private Task Reload() => PageState.Load();

    public void Dispose()
    {
        PageState.ItemsChanged -= OnApiKeyItemsChanged;
        PageState.Changed -= OnPagePrefsChanged;
    }
}
