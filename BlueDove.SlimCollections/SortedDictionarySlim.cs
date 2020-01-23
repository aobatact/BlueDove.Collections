using System;

namespace BlueDove.SlimCollections
{
    public class SortedDictionarySlim<TKey, TValue> where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        private int count;
        private DictionarySlim<TKey, TValue>.Entry[] entries;
        private int[] buckets;
    }
}
