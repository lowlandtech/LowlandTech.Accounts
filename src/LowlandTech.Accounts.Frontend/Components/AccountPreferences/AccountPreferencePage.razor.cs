namespace LowlandTech.Accounts.Frontend.Components.AccountPreferences;

public partial class AccountPreferencePage : ComponentBase, IDisposable
{
    [Inject] private AppState App { get; set; } = default!;
    [Inject] private IPermService Perms { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private AccountPreferencePageState PageState { get; set; } = default!;
    [Inject] private AccountPreferenceApiService ApiService { get; set; } = default!;

    private Func<GridStateVirtualize<AccountPreferenceDto>, CancellationToken, Task<GridData<AccountPreferenceDto>>> _serverData = default!;
    private bool _canCreate;

    protected override async Task OnInitializedAsync()
    {
        _serverData = AccountPreferenceDataGridAdapter.CreateAccountPreferenceServerData(ApiService, PageState);
        PageState.ItemsChanged += OnAccountPreferenceItemsChanged;
        _canCreate = await Perms.CanAsync("accounts::create");
        PageState.Changed += OnPagePrefsChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        try
        {
            var dense   = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::AccountPreferencePage::Dense");
            var reorder = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::AccountPreferencePage::Reorder");
            var dropCls = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::AccountPreferencePage::DropCls");
            var dragIcn = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::AccountPreferencePage::DragIcon");
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
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::AccountPreferencePage::Dense", PageState.Dense.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::AccountPreferencePage::Reorder", PageState.DragDropColumnReordering.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::AccountPreferencePage::DropCls", PageState.ApplyDropClassesOnDragStarted.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::AccountPreferencePage::DragIcon", PageState.DragIndicatorIcon ?? string.Empty);
        }
        catch { }
        await InvokeAsync(StateHasChanged);
    }

    private void OnAccountPreferenceItemsChanged() => InvokeAsync(StateHasChanged);
    private Task Reload() => PageState.Load();

    public void Dispose()
    {
        PageState.ItemsChanged -= OnAccountPreferenceItemsChanged;
        PageState.Changed -= OnPagePrefsChanged;
    }
}
