
namespace LowlandTech.Accounts.Frontend.Components.Addresses;

public partial class AddressEdit : ComponentBase, IDisposable
{
    [Inject] private AppState App { get; set; } = default!;
    [Inject] private IPermService Perms { get; set; } = default!;
    [Inject] private AddressPageState State { get; set; } = default!;

    private bool _canCreate, _canUpdate, _canDelete;
    private bool IsReadOnly => State.ViewState == ViewStates.Viewing;
    private bool CanEdit   => _canUpdate && State.Selected is not null;
    private bool CanDelete => _canDelete && State.Selected is not null;
    private bool CanSave   => (State.ViewState == ViewStates.Editing && _canUpdate) || (State.ViewState == ViewStates.Adding && _canCreate);

    protected override async Task OnInitializedAsync()
    {
        State.ItemsChanged += OnAddressChanged;
        _canCreate = await Perms.CanAsync("address::create");
        _canUpdate = await Perms.CanAsync("address::update");
        _canDelete = await Perms.CanAsync("address::delete");
    }

    private void OnAddressChanged() => InvokeAsync(StateHasChanged);

    public void Dispose() => State.ItemsChanged -= OnAddressChanged;

    private void BeginEdit()   => State.BeginEdit();
    private void BeginAdd()    => State.BeginAdd();
    private void CancelEdit()  => State.CancelEdit();

    private async Task OnSaveAsync()
    {
        // TODO: dispatch Create/Update via MediatR/typed API and surface validation via App toast
        App.ToastSuccess("Saved");
        State.CancelEdit();
        await State.Load();
    }

    private async Task OnDeleteAsync()
    {
        if (!_canDelete || State.Selected is null) return;
        // TODO: confirm + dispatch Delete via MediatR/typed API
        App.ToastSuccess("Deleted");
        await State.Load();
    }
}
