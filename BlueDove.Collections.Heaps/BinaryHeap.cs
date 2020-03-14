using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Heaps
{
    /// <summary>
    /// Binary Heap implemented by array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayBinaryHeap<T> : IHeap<T>
        where T : IComparable<T>
    {
        private T[] _values;
        private const int MinIndex = 0;
        private const int Move = MinIndex - 1;

        public ArrayBinaryHeap()
        {
            _values = new T[4];
        }

        public void Push(T value)
        {
            if (Count >= _values.Length) Util.Expand(ref _values);

            CascadeUp(value, Count);
        }

        public T Peek()
        {
            if (Count != 0) return _values[MinIndex];
            Util.ThrowNoItem();
            return default;
        }

        public T Pop()
        {            
            if (Count == 0) Util.ThrowNoItem();
            var ret = _values[MinIndex];
            CascadeDown(_values[--Count], MinIndex);
            /*
            CascadeDown(_values[Count], Count);
            Count--;
            */
            return ret;
        }

        public bool TryPeek(out T value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }

            value = _values[MinIndex];
            return true;
        }

        public bool TryPop(out T value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }
            value = _values[MinIndex];
            CascadeDown(_values[--Count], MinIndex);
            /*
            CascadeDown(_values[Count], Count);
            Count--;
            */
            return true;
        }

        public int Count { get; private set; }

        public void Clear()
        {
#if NETSTANDARD2_0
            for (var i = 0; i < _values.Length; i++)
            {
                _values[i] = default(T);
            }
#else 
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                Array.Fill(_values, default(T));
#endif 
            Count = 0;
        }

        private void CascadeUp(T value, int oldIndex)
        {
            ref var currentPos = ref _values[oldIndex];
            var uIndex = (oldIndex + Move) >> 1;
            if (uIndex > MinIndex)
            {
                ref var upperPos = ref _values[uIndex];

                while (value.CompareTo(upperPos) < 0)
                {
                    currentPos = upperPos;
                    currentPos = ref upperPos;
                    if (oldIndex == MinIndex + 1) break;
                    oldIndex = uIndex;
                    uIndex = (oldIndex + Move) >> 1;
                    upperPos = ref _values[uIndex];
                }
            }
            currentPos = value;
        }

        private void CascadeDown(T value, int oldIndex)
        {
            ref var cur = ref _values[oldIndex];
            while (true)
            {
                var dl = (oldIndex << 1) - Move;
                var dr = dl + 1;
                if (dr < Count)
                {
                    ref var vl = ref _values[dl];
                    ref var vr = ref Unsafe.Add(ref vl, 1);
                    var comp = vl.CompareTo(vr) < 0;
                    ref var max = ref comp ? ref vl : ref vr;
                    if (max.CompareTo(value) < 0)
                    {
                        cur = max;
                        cur = ref max;
                        oldIndex = comp ? dl : dr;
                        continue;
                    }
                    break;
                }
                if (dr == Count)
                {
                    Debug.Assert(dl >= MinIndex && dl < _values.Length);
                    ref var max = ref _values[dl];
                    if (max.CompareTo(value) < 0)
                    {
                        cur = max;
                        cur = ref max;
                    }
                }
                break;
            }

            cur = value;
        }
    }
}
