using System;

namespace BlueDove.SlimCollections
{
    public class SortedDictionarySlim<TKey, TValue> where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        private int count;
        private DictionarySlim<TKey, TValue>.Entry[] entries;
        private int[] buckets;
    }
    
    
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
    {
        public bool Equals(Unit other) => true;
        public override bool Equals(object obj) { return obj is Unit; }
        public int CompareTo(Unit other) => 0;
        public override int GetHashCode() => 0;
        public override string ToString() => typeof(Unit).Name;
    }
}
