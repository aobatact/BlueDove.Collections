using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlueDove.SlimCollections
{
    public class DictionarySlimWrapper<TKey , TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private readonly DictionarySlim<TKey, TValue> baseDictionary;
        public DictionarySlim<TKey, TValue>.Enumerator GetEnumerator() => baseDictionary.GetEnumerator();
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear() => baseDictionary.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) 
            => baseDictionary.TryGetValue(item.Key, out var v) &&
               EqualityComparer<TValue>.Default.Equals(item.Value, v);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var i = arrayIndex;
            foreach (var pair in baseDictionary)
            {
                array[i++] = pair;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) 
            => baseDictionary.TryGetValue(item.Key, out var v) &&
               EqualityComparer<TValue>.Default.Equals(item.Value, v) && 
               baseDictionary.Remove(item.Key);

        public int Count => baseDictionary.Count;
        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value) { baseDictionary.GetOrAddValueRef(key) = value; }

        public bool Remove(TKey key) => baseDictionary.Remove(key);

        public bool ContainsKey(TKey key) => baseDictionary.ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value) => baseDictionary.TryGetValue(key, out value);

        public TValue this[TKey key]
        {
            get => baseDictionary.TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();
            set => baseDictionary.GetOrAddValueRef(key) = value;
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => baseDictionary.Select(x=>x.Key);

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => baseDictionary.Select(x=>x.Value);

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => baseDictionary.Select(x=>x.Key).ToArray();

        ICollection<TValue> IDictionary<TKey, TValue>.Values => baseDictionary.Select(x=>x.Value).ToArray();
    }
}
