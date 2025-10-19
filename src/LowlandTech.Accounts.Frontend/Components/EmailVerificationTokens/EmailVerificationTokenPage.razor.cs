namespace LowlandTech.Accounts.Frontend.Components.EmailVerificationTokens;

public partial class EmailVerificationTokenPage : ComponentBase, IDisposable
{
    [Inject] private AppState App { get; set; } = default!;
    [Inject] private IPermService Perms { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private EmailVerificationTokenPageState PageState { get; set; } = default!;
    [Inject] private EmailVerificationTokenApiService ApiService { get; set; } = default!;

    private Func<GridStateVirtualize<EmailVerificationTokenDto>, CancellationToken, Task<GridData<EmailVerificationTokenDto>>> _serverData = default!;
    private bool _canCreate;

    protected override async Task OnInitializedAsync()
    {
        _serverData = EmailVerificationTokenDataGridAdapter.CreateEmailVerificationTokenServerData(ApiService, PageState);
        PageState.ItemsChanged += OnEmailVerificationTokenItemsChanged;
        _canCreate = await Perms.CanAsync("accounts::create");
        PageState.Changed += OnPagePrefsChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        try
        {
            var dense   = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::EmailVerificationTokenPage::Dense");
            var reorder = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::EmailVerificationTokenPage::Reorder");
            var dropCls = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::EmailVerificationTokenPage::DropCls");
            var dragIcn = await JS.InvokeAsync<string>("localStorage.getItem", "Accounts::EmailVerificationTokenPage::DragIcon");
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
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::EmailVerificationTokenPage::Dense", PageState.Dense.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::EmailVerificationTokenPage::Reorder", PageState.DragDropColumnReordering.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::EmailVerificationTokenPage::DropCls", PageState.ApplyDropClassesOnDragStarted.ToString());
            await JS.InvokeVoidAsync("localStorage.setItem", "Accounts::EmailVerificationTokenPage::DragIcon", PageState.DragIndicatorIcon ?? string.Empty);
        }
        catch { }
        await InvokeAsync(StateHasChanged);
    }

    private void OnEmailVerificationTokenItemsChanged() => InvokeAsync(StateHasChanged);
    private Task Reload() => PageState.Load();

    public void Dispose()
    {
        PageState.ItemsChanged -= OnEmailVerificationTokenItemsChanged;
        PageState.Changed -= OnPagePrefsChanged;
    }
}
