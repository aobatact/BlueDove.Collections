using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BlueDove.SlimCollections;

namespace BlueDove.Collections
{
    public struct KeyedTable<TKey1, TKey2, TValue> where TKey1 : IEquatable<TKey1> where TKey2 : IEquatable<TKey2>
    {
        private readonly TValue[] _values;
        private readonly DictionarySlim<TKey1, int> _keys1;
        private readonly DictionarySlim<TKey2, int> _keys2;

        internal KeyedTable(DictionarySlim<TKey1, int> keys1, DictionarySlim<TKey2, int> keys2, TValue[] values)
        {
            _values = values;
            _keys1 = keys1;
            _keys2 = keys2;
        }

        public KeyedTable(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, bool throwOnDuplicateKeys = false)
        {
            _keys1 = CreateKeys(keys1, throwOnDuplicateKeys);
            _keys2 = CreateKeys(keys2, throwOnDuplicateKeys);
            _values = new TValue[_keys1.Count * _keys2.Count];
        }
        
        public int Key1Count => _keys1.Count;
        public int Key2Count => _keys2.Count;

        public DictionarySlim<TKey1, int>.Enumerator GetKeys1 => _keys1.GetEnumerator();
        public DictionarySlim<TKey2, int>.Enumerator GetKeys2 => _keys2.GetEnumerator();

        public TValue this[TKey1 key1, TKey2 key2]
        {
            get
            {
                var index = GetIndex(key1, key2);
                if (index < 0) ThrowKeyNotFound();
                return _values[index];
            }
            set
            {
                var index = GetIndex(key1, key2);
                if (index < 0) ThrowKeyNotFound();
                _values[index] = value;
            }
        }

        public bool ContainsKey1(TKey1 key1) => _keys1.ContainsKey(key1);
        public bool ContainsKey2(TKey2 key2) => _keys2.ContainsKey(key2);

        public Key1FixedCollection GetKey1Values(TKey1 key1)
        {
            _keys1.TryGetValue(key1, out var i);
            return new Key1FixedCollection(this, i * Key2Count);
        }
        public Key2FixedCollection GetKey2Values(TKey2 key2)
        {
            _keys2.TryGetValue(key2, out var i);
            return new Key2FixedCollection(this, i);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TValue TryGetValueRef(TKey1 key1, TKey2 key2, out bool found)
        {
            int i;
            if ((i = GetIndex(key1, key2)) >= 0)
            {
                found = true;
                return ref _values[i];
            }

            found = false;
            return ref DummyCache<TValue>.Dummy;
        }
        
        private int GetIndex(TKey1 key1, TKey2 key2)
        {
            if (_keys1.TryGetValue(key1, out var i1) && _keys2.TryGetValue(key2, out var i2))
                return i1 * Key2Count + i2;
            return -1;
        }
        private static DictionarySlim<TKey, int> CreateKeys<TKey>(IEnumerable<TKey> keys,bool throwOnDuplicate)
            where TKey : IEquatable<TKey>
        {
            var firstSize = keys switch
            {
                ICollection<TKey> col => col.Count,
                IReadOnlyCollection<TKey> rCol => rCol.Count,
                _ => 0
            };

            var dict = new DictionarySlim<TKey, int>(firstSize);
            var counter = -1;
            foreach (var key in keys)
            {
                ref var i = ref dict.GetOrAddValueRef(key);
                if (i == 0) i = ++counter;
                else if(throwOnDuplicate)
                {
                    ThrowDuplicate();
                }
            }

            return dict;
        }

        private static void ThrowDuplicate()
        {
            throw new InvalidOperationException("Duplicate Key");
        }
        private static void ThrowKeyNotFound()
        {
            throw new KeyNotFoundException();
        }
        
        public struct Key1FixedCollection : IReadOnlyCollection<KeyValuePair<TKey2,TValue>>
        {
            private KeyedTable<TKey1, TKey2, TValue> _keyedTable;
            private int _index1;

            public Key1FixedCollection(KeyedTable<TKey1, TKey2, TValue> keyedTable, int index1)
            {
                _keyedTable = keyedTable;
                _index1 = index1;
            }

            public ref TValue this[TKey2 key]
            {
                get
                {
                    if (_keyedTable._keys2.TryGetValue(key, out var index2))
                        return ref _keyedTable._values[_index1 + index2];
                    ThrowKeyNotFound();
                    return ref DummyCache<TValue>.Dummy;
                }
            }

            public Enumerator GetEnumerator() 
                => new Enumerator(_keyedTable._values,_keyedTable._keys2.GetEnumerator(),_index1);

            IEnumerator<KeyValuePair<TKey2, TValue>> IEnumerable<KeyValuePair<TKey2, TValue>>.GetEnumerator() 
                => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() 
                => GetEnumerator();

            public struct Enumerator : IEnumerator<KeyValuePair<TKey2,TValue>>
            {
                private TValue[] _values;
                private DictionarySlim<TKey2, int>.Enumerator _enumerator;
                private int index1;

                public Enumerator(TValue[] values, DictionarySlim<TKey2, int>.Enumerator enumerator, int index1)
                {
                    _values = values;
                    _enumerator = enumerator;
                    this.index1 = index1;
                    Current = default;
                }

                public bool MoveNext()
                {
                    if (_enumerator.MoveNext())
                    {
                        var (k, v) = _enumerator.Current;
                        Current = new KeyValuePair<TKey2, TValue>(k, _values[index1 + v]);
                        return true;
                    }
                    return false;
                }

                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                public KeyValuePair<TKey2, TValue> Current { get; private set; }

                object IEnumerator.Current => Current;
                
                public void Dispose()
                {
                }
            }
            
            public int Count => _keyedTable.Key2Count;
        }
        public struct Key2FixedCollection : IReadOnlyCollection<KeyValuePair<TKey1,TValue>>
        {
            private KeyedTable<TKey1, TKey2, TValue> _keyedTable;
            private int _index2;

            public Key2FixedCollection(KeyedTable<TKey1, TKey2, TValue> keyedTable, int index2)
            {
                _keyedTable = keyedTable;
                _index2 = index2;
            }

            public ref TValue this[TKey1 key]
            {
                get
                {
                    if (_keyedTable._keys1.TryGetValue(key, out var index1))
                        return ref _keyedTable._values[_index2 + index1 * _keyedTable.Key2Count];
                    ThrowKeyNotFound();
                    return ref DummyCache<TValue>.Dummy;
                }
            }

            public Enumerator GetEnumerator()
                => new Enumerator(_keyedTable._values, _keyedTable._keys1.GetEnumerator(), _index2,
                    _keyedTable.Key2Count);

            IEnumerator<KeyValuePair<TKey1, TValue>> IEnumerable<KeyValuePair<TKey1, TValue>>.GetEnumerator() 
                => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() 
                => GetEnumerator();

            public struct Enumerator : IEnumerator<KeyValuePair<TKey1,TValue>>
            {
                private TValue[] _values;
                private DictionarySlim<TKey1, int>.Enumerator _enumerator;
                private int index2;
                private int l2;

                public Enumerator(TValue[] values, DictionarySlim<TKey1, int>.Enumerator enumerator, int index2,int l2)
                {
                    _values = values;
                    _enumerator = enumerator;
                    this.index2 = index2;
                    Current = default;
                    this.l2 = l2;
                }

                public bool MoveNext()
                {
                    if (_enumerator.MoveNext())
                    {
                        var (k, v) = _enumerator.Current;
                        Current = new KeyValuePair<TKey1, TValue>(k, _values[index2 * l2 + v]);
                        return true;
                    }
                    return false;
                }

                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                public KeyValuePair<TKey1, TValue> Current { get; private set; }

                object IEnumerator.Current => Current;
                
                public void Dispose()
                {
                }
            }
            
            public int Count => _keyedTable.Key1Count;
        }
    }
}