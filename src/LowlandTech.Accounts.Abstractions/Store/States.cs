
namespace LowlandTech.Accounts.Abstractions.Store;

public interface IStateSlice
{
    event Action? Changed;
    void NotifyChanged();
}

public sealed class StateSlice<T> : IStateSlice
{
    private T _value = default!;
    public event Action? Changed;

    public T Value
    {
        get => _value;
        set { _value = value; NotifyChanged(); }
    }

    public void NotifyChanged() => Changed?.Invoke();
}

public sealed class StateBag
{
    private readonly Dictionary<string, IStateSlice> _slices = new(StringComparer.OrdinalIgnoreCase);

    public StateSlice<T> GetSlice<T>(string key)
    {
        if (_slices.TryGetValue(key, out var s)) return (StateSlice<T>)s;
        var created = new StateSlice<T>();
        _slices[key] = created;
        return created;
    }

    public T Get<T>(string key) => GetSlice<T>(key).Value;
    public void Set<T>(string key, T value) => GetSlice<T>(key).Value = value;
}
