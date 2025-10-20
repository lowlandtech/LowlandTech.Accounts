
namespace LowlandTech.Accounts.Frontend.Components.UserAccounts;

public sealed class UserAccountPageState : INotificationHandler<UserAccountInitEvent>
{
    public event Action? Changed;
    private readonly StateBag _bag = new();
    private readonly IMediator _mediator;

    private readonly StateSlice<IReadOnlyList<UserAccountDto>> _itemsSlice;
    private readonly StateSlice<int> _pageSlice;
    private readonly StateSlice<int> _pageSizeSlice;
    private readonly StateSlice<string?> _querySlice;
    private readonly StateSlice<string?> _sortBySlice;
    private readonly StateSlice<string?> _sortDirSlice;

    public UserAccountPageState(IMediator mediator)
    {
        _mediator = mediator;
        _itemsSlice    = _bag.GetSlice<IReadOnlyList<UserAccountDto>>("Items");
        _pageSlice     = _bag.GetSlice<int>("Page");
        _pageSizeSlice = _bag.GetSlice<int>("PageSize");
        _querySlice    = _bag.GetSlice<string?>("Query");
        _sortBySlice   = _bag.GetSlice<string?>("SortBy");
        _sortDirSlice  = _bag.GetSlice<string?>("SortDir");

        _pageSlice.Value     = 1;
        _pageSizeSlice.Value = 50; // match backend default
        _sortDirSlice.Value  = "asc";

        _itemsSlice.Changed += OnItemsSliceChanged;
        _pageSlice.Changed += () => Changed?.Invoke();
        _pageSizeSlice.Changed += () => Changed?.Invoke();
        _querySlice.Changed += () => Changed?.Invoke();
        _sortBySlice.Changed += () => Changed?.Invoke();
        _sortDirSlice.Changed += () => Changed?.Invoke();
    }

    public bool ApplyDropClassesOnDragStarted { get; set; } = false;
    public bool DragDropColumnReordering { get; set; } = true;
    public bool Dense { get; set; } = true;
    public string DragIndicatorIcon { get; set; } = Icons.Material.Filled.DragIndicator;
    public ViewStates ViewState { get; private set; } = ViewStates.Viewing;
    public UserAccountDto? Selected { get; private set; }
    public UserAccountDto  Editing  { get; private set; } = new();

    public IReadOnlyList<UserAccountDto> Items
    {
        get => _itemsSlice.Value ?? [];
        private set => _itemsSlice.Value = value ?? [];
    }

    // Bindable props with public setters that dispatch actions
    public int Page
    {
        get => _pageSlice.Value;
        set { _ = _mediator.Send(new UserAccountSetPageAction(value)); }
    }

    public int PageSize
    {
        get => _pageSizeSlice.Value;
        set { _ = _mediator.Send(new UserAccountSetPageSizeAction(value)); }
    }

    public string? Query
    {
        get => _querySlice.Value;
        set { _ = _mediator.Send(new UserAccountSetQueryAction(value)); }
    }

    public string? SortBy
    {
        get => _sortBySlice.Value;
        set { _ = _mediator.Send(new UserAccountSetSortAction(value, SortDir)); }
    }

    public string? SortDir
    {
        get => _sortDirSlice.Value;
        set { _ = _mediator.Send(new UserAccountSetSortAction(SortBy, value)); }
    }

    // Notify components when Items change
    public event Action? ItemsChanged;
    private void OnItemsSliceChanged() => ItemsChanged?.Invoke();

    public Task Load(int? page = null, int? pageSize = null, string? q = null, string? sort = null, string? dir = null)
        => _mediator.Send(new UserAccountInitAction(page ?? Page, pageSize ?? PageSize, q ?? Query, sort ?? SortBy, dir ?? SortDir));

    public void Select(UserAccountDto? item)
    {
        Selected = item;
        Editing  = item is null
            ? new()
            : JsonSerializer.Deserialize<UserAccountDto>(
                JsonSerializer.Serialize(item))!; // simple deep clone
        ViewState = ViewStates.Viewing;
    }

    public void BeginAdd()  { Editing = new(); ViewState = ViewStates.Adding; }
    public void BeginEdit() { if (Selected is not null) ViewState = ViewStates.Editing; }
    public void CancelEdit(){ ViewState = ViewStates.Viewing; }

    // Event → reduce slices and items
    public Task Handle(UserAccountInitEvent e, CancellationToken ct)
    {
        ReducePage(e.Page);
        ReducePageSize(e.PageSize);
        ReduceQuery(e.Query);
        ReduceSort(e.Sort, e.Dir);
        Items = e.Items ?? [];
        return Task.CompletedTask;
    }

    // Internal reducers (called by handlers)
    internal void ReducePage(int v)          => _pageSlice.Value = v < 1 ? 1 : v;
    internal void ReducePageSize(int v)      => _pageSizeSlice.Value = v <= 0 ? 50 : v;
    internal void ReduceQuery(string? v)     => _querySlice.Value = v;
    internal void ReduceSort(string? by, string? dir)
    {
        _sortBySlice.Value = by;
        _sortDirSlice.Value = string.IsNullOrWhiteSpace(dir) ? "asc" : dir;
    }
    
    internal void ToastError(string message)
    {
        //_toastService.ShowError(message);
    }
}
