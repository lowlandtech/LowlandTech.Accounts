namespace LowlandTech.Accounts.Frontend.Components.RecoveryCodes;

public partial class RecoveryCodePage : ComponentBase, IDisposable
{
    [Inject] private AppState App { get; set; } = default!;
    [Inject] private IPermService Perms { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private RecoveryCodePageState PageState { get; set; } = default!;
    [Inject] private RecoveryCodeApiService ApiService { get; set; } = default!;

    private Func<GridStateVirtualize<RecoveryCodeDto>, CancellationToken, Task<GridData<RecoveryCodeDto>>> _serverData = default!;
    private bool _canCreate;

    protected override async Task OnInitializedAsync()
    {
        _serverData = RecoveryCodeDataGridAdapter.CreateRecoveryCodeServerData(ApiService, PageState);
        PageState.ItemsChanged += OnRecoveryCodeItemsChanged;
        _canCreate = await Perms.CanAsync("accounts::create");
        PageState.Changed += OnPagePrefsChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        try
        {
            var dense   = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::RecoveryCodePage::Dense");
            var reorder = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::RecoveryCodePage::Reorder");
            var dropCls = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::RecoveryCodePage::DropCls");
            var dragIcn = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::RecoveryCodePage::DragIcon");
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
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::RecoveryCodePage::Dense", PageState.Dense.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::RecoveryCodePage::Reorder", PageState.DragDropColumnReordering.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::RecoveryCodePage::DropCls", PageState.ApplyDropClassesOnDragStarted.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::RecoveryCodePage::DragIcon", PageState.DragIndicatorIcon ?? string.Empty);
        }
        catch { }
        await InvokeAsync(StateHasChanged);
    }

    private void OnRecoveryCodeItemsChanged() => InvokeAsync(StateHasChanged);
    private Task Reload() => PageState.Load();

    public void Dispose()
    {
        PageState.ItemsChanged -= OnRecoveryCodeItemsChanged;
        PageState.Changed -= OnPagePrefsChanged;
    }
}
