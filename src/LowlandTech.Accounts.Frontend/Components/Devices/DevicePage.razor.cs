namespace LowlandTech.Accounts.Frontend.Components.Devices;

public partial class DevicePage : ComponentBase, IDisposable
{
    [Inject] private AppState App { get; set; } = default!;
    [Inject] private IPermService Perms { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private DevicePageState PageState { get; set; } = default!;
    [Inject] private DeviceApiService ApiService { get; set; } = default!;

    private Func<GridStateVirtualize<DeviceDto>, CancellationToken, Task<GridData<DeviceDto>>> _serverData = default!;
    private bool _canCreate;

    protected override async Task OnInitializedAsync()
    {
        _serverData = DeviceDataGridAdapter.CreateDeviceServerData(ApiService, PageState);
        PageState.ItemsChanged += OnDeviceItemsChanged;
        _canCreate = await Perms.CanAsync("accounts::create");
        PageState.Changed += OnPagePrefsChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        try
        {
            var dense   = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::DevicePage::Dense");
            var reorder = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::DevicePage::Reorder");
            var dropCls = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::DevicePage::DropCls");
            var dragIcn = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::DevicePage::DragIcon");
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
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::DevicePage::Dense", PageState.Dense.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::DevicePage::Reorder", PageState.DragDropColumnReordering.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::DevicePage::DropCls", PageState.ApplyDropClassesOnDragStarted.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::DevicePage::DragIcon", PageState.DragIndicatorIcon ?? string.Empty);
        }
        catch { }
        await InvokeAsync(StateHasChanged);
    }

    private void OnDeviceItemsChanged() => InvokeAsync(StateHasChanged);
    private Task Reload() => PageState.Load();

    public void Dispose()
    {
        PageState.ItemsChanged -= OnDeviceItemsChanged;
        PageState.Changed -= OnPagePrefsChanged;
    }
}
