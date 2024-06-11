
namespace DragonRunes.Shared;

// Classe para gerenciar um dicionário genérico com funcionalidades adicionais
public class DictionaryWrapper<TKey, TValue>
{
    private readonly Dictionary<TKey, TValue> internalDictionary = new();

    // Método para adicionar um item ao dicionário
    public void AddItem(TKey key, TValue value)
    {
        internalDictionary[key] = value;
    }

    // Método para obter um item do dicionário
    public TValue GetItem(TKey key)
    {
        return internalDictionary.TryGetValue(key, out TValue value) ? value : default;
    }

    // Método para obter um item do dicionário de um tipo específico
    public T GetItem<T>(TKey key) where T : TValue
    {
        return internalDictionary.TryGetValue(key, out TValue value) && value is T ? (T)value : default;
    }

    // Método para verificar se o dicionário contém uma chave
    public bool ContainsKey(TKey key)
    {
        return internalDictionary.ContainsKey(key);
    }

    // Método para verificar se o dicionário contém uma chave de um tipo específico
    public bool ContainsKey<T>(TKey key) where T : TValue
    {
        return internalDictionary.TryGetValue(key, out TValue value) && value is T;
    }

    // Método para contar o número de itens no dicionário
    public int Count()
    {
        return internalDictionary.Count;
    }

    // Método para obter todos os itens do dicionário
    public Dictionary<TKey, TValue> GetItems()
    {
        return internalDictionary;
    }

    // Método para remover um item do dicionário
    public void RemoveItem(TKey key)
    {
        internalDictionary.Remove(key);
    }

    // Método para remover um item do dicionário com um nome e tipo específico
    public void RemoveItem<T>(TKey key) where T : TValue
    {
        if (internalDictionary.TryGetValue(key, out TValue value) && value is T)
        {
            internalDictionary.Remove(key);
        }
    }

    // Método para limpar o dicionário
    public void Clear()
    {
        internalDictionary.Clear();
    }
}
