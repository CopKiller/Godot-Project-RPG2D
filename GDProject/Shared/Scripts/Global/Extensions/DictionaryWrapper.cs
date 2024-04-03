using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdProject.Shared.Scripts.Global.Extensions
{
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

        //internal void UpdateValuesDifferentFrom(TKey keyToCheck, TValue newValue, Type valueType)
        //{
        //    internalDictionary
        //        .Where(x => !EqualityComparer<TKey>.Default.Equals(x.Key, keyToCheck) && x.Value.GetType() == valueType)
        //        .ToList()
        //        .ForEach(x => internalDictionary[x.Key] = newValue);
        //}
    }
}
