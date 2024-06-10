
namespace DragonRunes.Shared;

public class DictionaryWrapper<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> internalDictionary = new();

    public void AddItem(TKey key, TValue value)
    {
        internalDictionary[key] = value;
    }

    public TValue GetItem(TKey key)
    {
        return internalDictionary.TryGetValue(key, out TValue value) ? value : default;
    }

    public bool ContainsKey(TKey key)
    {
        return internalDictionary.ContainsKey(key);
    }

    public int Count()
    {
        return internalDictionary.Count;
    }

    public Dictionary<TKey, TValue> GetItems()
    {
        return internalDictionary;
    }

    public void RemoveItem(TKey key)
    {
        internalDictionary.Remove(key);
    }

    public void Clear()
    {
        internalDictionary.Clear();
    }
}
