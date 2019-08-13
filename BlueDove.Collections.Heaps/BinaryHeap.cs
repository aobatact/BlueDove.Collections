using System;

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

        public ArrayBinaryHeap()
        {
            _values = new T[4];
        }

        public void Push(T value)
        {
            if (Count >= _values.Length)
            {
                BufferUtil.Expand(ref _values);
            }

            CascadeUp(value, Count++);
        }

        public T Peek()
        {
            if (Count != 0) return _values[0];
            BufferUtil.ThrowNoItem();
            return default;
        }

        public T Pop()
        {            
            if (Count == 0)
            {
                BufferUtil.ThrowNoItem();
            }
            var ret = _values[0];
            CascadeDown(_values[--Count], Count);
            return ret;
        }

        public bool TryPeek(out T value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }

            value = _values[0];
            return true;
        }
        
        public bool TryPop(out T value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }
            value = _values[0];
            CascadeDown(_values[--Count], Count);
            return true;
        }

        public int Count { get; private set; }
        
        public void Clear()
        {
            Count = 0;
        }

        private void CascadeUp(T value, int oldIndex)
        {
            ref var currentPos = ref _values[oldIndex];
            var uIndex = (oldIndex - 1) >> 1;
            ref var upperPos = ref _values[uIndex];
            
            while (value.CompareTo(upperPos) < 0)
            {
                currentPos = upperPos;
                currentPos = ref upperPos;
                if(oldIndex == 0) break;
                oldIndex = uIndex;
                uIndex = (oldIndex - 1) >> 1;
                upperPos = ref _values[uIndex];
            }

            currentPos = value;

        }

        private void CascadeDown(T value, int oldIndex)
        {
            ref var cur = ref _values[oldIndex];
            do
            {
                var dl = (oldIndex << 1) + 1;
                var dr = dl + 1;
                if (dr < Count)
                {
                    ref var vl = ref _values[dl];
                    ref var vr = ref _values[dr];
                    var comp = vl.CompareTo(vr) > 0;
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
                    ref var max = ref _values[dl];
                    if (max.CompareTo(value) < 0)
                    {
                        cur = max;
                        cur = ref max;
                    }
                }
                break;
            } while (true);

            cur = value;
        }
    }
}