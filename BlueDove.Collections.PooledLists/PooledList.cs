//#define VERSION_CHECK
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;namespace BlueDove.Collections.PooledLists
{
    public class PooledList<T> : IList<T>, IReadOnlyList<T>
    {
        private readonly ArrayPool<T> _pool;
        private T[] _array;
        private int _length;
#if VERSION_CHECK
        public int Version { get; private set; }
#endif
        
        public PooledList() : this(ArrayPool<T>.Shared)
        {
        }

        public PooledList(int capacity) : this(ArrayPool<T>.Shared, capacity)
        {
        }

        public PooledList(ArrayPool<T> pool, int capacity = 4)
        {
            _pool = pool;
            _array = _pool.Rent(capacity);
            _length = 0;
#if VERSION_CHECK
            Version = 0;
#endif            
        }

        public int Count => _array.Length;

        bool ICollection<T>.IsReadOnly => false;

        public Memory<T> Memory => _array.AsMemory(0, _length);
        public Span<T> Span => _array.AsSpan(0, _length);

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return _array[index];
            }
            set
            {
                CheckIndex(index);
                _array[index] = value;
                ChangeVersion();
            }
        }

        public void Add(T item)
        {
            if (_array.Length == _length)
            {
                Resize();
            }

            _array[_length++] = item;
            ChangeVersion();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Resize()
        {
            var nArray = _pool.Rent(_length << 1);
            Array.Copy(_array, nArray, _length);
            _array = nArray;
        }

        public void Clear()
        {
            _length = 0;
            _pool.Return(_array);
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, 0, array, arrayIndex, _length);
        }

        public int IndexOf(T item)
            => Array.IndexOf(_array, item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(int index, T item)
        {
            CheckIndex(index);
            if (_array.Length == _length)
                InsertWithResize(index);
            else
            {
                Array.Copy(_array, index + 1, _array, index, _length - index - 1);
                ChangeVersion();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void InsertWithResize(int index)
        {
            var nArray = _pool.Rent(_length << 1);
            Array.Copy(_array, nArray, index);
            Array.Copy(_array, index + 1, nArray, index, _length - index - 1);
            _array = nArray;
            ChangeVersion();
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ref T UnsafeGetRef(int index) => ref _array[index];

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index < 0) return false;
            RemoveAtInner(index);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(int index)
        {
            CheckIndex(index);
            RemoveAtInner(index);
        }

        private void RemoveAtInner(int index)
        {
            Array.Copy(_array, index + 1,
                _array, index, _length - index - 1);
            ChangeVersion();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckIndex(int index)
        {
            if ((uint) index >= (uint) _length)
            {
                Thrower(index);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Thrower(int index)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range");
        }

        [Conditional("VERSION_CHECK")]
        private void ChangeVersion()
        {
#if VERSION_CHECK
            Version++;
#endif
        }
        
        public struct Enumerator : IEnumerator<T>
        {
            private readonly PooledList<T> _list;
            private int _index;
#if VERSION_CHECK
            private int version;
#endif

            public Enumerator(PooledList<T> pooledList)
            {
                _list = pooledList;
                Current = default;
                _index = -1;
#if VERSION_CHECK
                version = pooledList.Version;
#endif
            }

            public bool MoveNext()
            {
#if VERSION_CHECK
                static void Throw()
                {
                    throw new InvalidOperationException("Modified list while iterating");
                }

                if (version != _list.Version)
                {
                    Throw();
                }
#endif
                if (++_index < _list.Count)
                {
                    Current = _list._array[_index];
                    return true;
                }

                return false;
            }

            void IEnumerator.Reset()
            {
                _index = -1;
            }

            public T Current { get; private set; }

            readonly object IEnumerator.Current => Current;

            readonly void IDisposable.Dispose()
            {
            }
        }
    }
}