namespace LowlandTech.Accounts.Frontend.Components.PasswordResetTokens;

public partial class PasswordResetTokenPage : ComponentBase, IDisposable
{
    [Inject] private AppState App { get; set; } = default!;
    [Inject] private IPermService Perms { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private PasswordResetTokenPageState PageState { get; set; } = default!;
    [Inject] private PasswordResetTokenApiService ApiService { get; set; } = default!;

    private Func<GridStateVirtualize<PasswordResetTokenDto>, CancellationToken, Task<GridData<PasswordResetTokenDto>>> _serverData = default!;
    private bool _canCreate;

    protected override async Task OnInitializedAsync()
    {
        _serverData = PasswordResetTokenDataGridAdapter.CreatePasswordResetTokenServerData(ApiService, PageState);
        PageState.ItemsChanged += OnPasswordResetTokenItemsChanged;
        _canCreate = await Perms.CanAsync("accounts::create");
        PageState.Changed += OnPagePrefsChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        try
        {
            var dense   = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::PasswordResetTokenPage::Dense");
            var reorder = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::PasswordResetTokenPage::Reorder");
            var dropCls = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::PasswordResetTokenPage::DropCls");
            var dragIcn = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::PasswordResetTokenPage::DragIcon");
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
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::PasswordResetTokenPage::Dense", PageState.Dense.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::PasswordResetTokenPage::Reorder", PageState.DragDropColumnReordering.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::PasswordResetTokenPage::DropCls", PageState.ApplyDropClassesOnDragStarted.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::PasswordResetTokenPage::DragIcon", PageState.DragIndicatorIcon ?? string.Empty);
        }
        catch { }
        await InvokeAsync(StateHasChanged);
    }

    private void OnPasswordResetTokenItemsChanged() => InvokeAsync(StateHasChanged);
    private Task Reload() => PageState.Load();

    public void Dispose()
    {
        PageState.ItemsChanged -= OnPasswordResetTokenItemsChanged;
        PageState.Changed -= OnPagePrefsChanged;
    }
}
