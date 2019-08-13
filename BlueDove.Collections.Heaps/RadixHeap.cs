﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Heaps
{
    public class RadixHeap<T, TConverter> : IHeap<T> 
        where T : IComparable<T> where TConverter : struct, IUnsignedValueConverter<T>
    {
        private readonly T[][] _buffers;
        private readonly int[] _bufferSizes;
        private T Last { get; set; }
        public int Count { get; private set; }
        
        public void Clear()
        {
            Count = 0;
            for (var i = 0; i < _bufferSizes.Length; i++)
            {
                if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                {
                    _buffers.AsSpan().Fill(null);
                }
                _bufferSizes[i] = 0;
            }
        }

        public RadixHeap()
        {
            var bufferSize = default(TConverter).BufferSize();
            _buffers = new T[bufferSize][];
            for (var index = 0; index < _buffers.Length; index++)
            {
                _buffers[index] = Array.Empty<T>();
            }

            _bufferSizes = new int[bufferSize];
        }

        public void Push(T value)
        {
            Debug.Assert(Last.CompareTo(value) <= 0);
            Count++;
            var target = default(TConverter).GetIndex(Last, value);
            Add2Buffer(value, target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Pop()
        {
            if (TryPop(out var val))
            {
                return val;
            }
            BufferUtil.ThrowNoItem();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPop(out T value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }
            Pull();
            value = Last;
            Count--;
            _bufferSizes[0]--;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPeek(out T value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }

            Pull();
            value = Last;
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Peek()
        {
            if (Count == 0)
            {
                BufferUtil.ThrowNoItem();
            }
            Pull();
            return Last;
        }
        
        private void Pull()
        {
            Debug.Assert(Count > 0);
            if (_bufferSizes[0] != 0) return;
            var i = 0;
            while (_bufferSizes[++i] != 0)
            {
                Debug.Assert(i + 1 < _buffers.Length);
            }

            var buffer = _buffers[i].AsSpan(0, _bufferSizes[i]);
            var nl = Min(buffer);
            foreach (var t in buffer)
            {
                Add2Buffer(t, default(TConverter).GetIndex(nl, t));
            }

            _bufferSizes[i] = 0;
            Last = nl;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add2Buffer(T value, int target)
        {
            Debug.Assert(target < _buffers.Length);
            ref var buffer = ref _buffers[target];
            ref var bfs = ref _bufferSizes[target];
            if (buffer.Length == bfs)
            { 
                BufferUtil.Expand(ref buffer);
            }
            
            buffer[bfs++] = value;
        }
        
        private static T Min(Span<T> buffer)
        {
            var min = buffer[0];
            foreach (var value in buffer)
            {
                if (min.CompareTo(value) > 0)
                {
                    min = value;
                }
            }

            return min;
        }

    }

    public class InHeapRadixHeap<T, THeap, TConverter> : IHeap<T>
        where T : IComparable<T> where THeap : IHeap<T> where TConverter : struct, IUnsignedValueConverter<T>
    {
        private readonly THeap[] _innerHeaps;

        public InHeapRadixHeap(Func<THeap> factory)
        {
            _innerHeaps = new THeap[default(TConverter).BufferSize()];
            for (var i = 0; i < _innerHeaps.Length; i++)
            {
                _innerHeaps[i] = factory();
            }
        }

        private T Last { get; set; }
        
        public void Push(T value)
        {
            Debug.Assert(Last.CompareTo(value) <= 0);
            Count++;
            var target = default(TConverter).GetIndex(Last, value);
            Add2Buffer(value, target);
        }

        private void Add2Buffer(T value, int target)
        {
            _innerHeaps[target].Push(value);
        }
        
        public T Pop()
        {            
            if (!TryPop(out var val))
            {
                return val;
            }
            BufferUtil.ThrowNoItem();
            return default;
        }

        public bool TryPop(out T value)
        {            
            if (Count == 0)
            {
                value = default;
                return false;
            }
            Pull();
            value = Last;
            Count--;
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Peek()
        {
            if (Count == 0)
            {
                BufferUtil.ThrowNoItem();
            }
            Pull();
            return Last;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPeek(out T value)
        {
            if (Count == 0)
            {
                value = default;
                return false;
            }

            Pull();
            value = Last;
            return true;
        }
        
        private void Pull()
        {
            Debug.Assert(Count > 0);
            if(_innerHeaps[0].Count > 0) return;
            var i = 0;
            while (_innerHeaps[++i].Count != 0)
            {
                Debug.Assert(i + 1 < _innerHeaps.Length);
            }

            var nl = _innerHeaps[i].Pop();
            while (_innerHeaps[i].TryPop(out var value))
            {
                Add2Buffer(value, default(TConverter).GetIndex(Last, value));
            }

            Last = nl;
        }

        public int Count { get; private set; }
        public void Clear()
        {
            Count = 0;
            foreach (var heap in _innerHeaps)
            {
                heap.Clear();
            }
        }
    }
}