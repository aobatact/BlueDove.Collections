using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Heaps
{
    internal static class Util
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Expand<T>(ref T[] buffer)
        {
            var newSize = buffer.Length << 1;
            if ((uint) newSize > int.MaxValue)
                newSize = int.MaxValue;
            Array.Resize(ref buffer, newSize);
        }

#if !NETSTANDARD2_0
        [DoesNotReturn]
#endif
        public static void ThrowNoItem()
        {
            throw new InvalidOperationException("No Items in Heap");
        }

        public static bool ReturnNoItem<T>(out T item)
        {
            item = default;
            return false;
        }
    }

    public static class HeapEx
    {
        public static IEnumerable<T> AsEnumerable<T>(this IHeap<T> heap)
        {
            while (heap.TryPop(out var value))
            {
                yield return value;
            }
        }
    }
    
#if NETSTANDARD2_0
    internal class MaybeNullWhenAttribute : Attribute
    {
        public MaybeNullWhenAttribute(bool b) { }
    }
#endif
    
    public readonly struct ComparableKeyValue<TKey,TValue> : IComparable<ComparableKeyValue<TKey,TValue>>
        where TKey : IComparable<TKey>
    {
        public readonly TKey key;
        public readonly TValue value;

        public ComparableKeyValue(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public int CompareTo(ComparableKeyValue<TKey, TValue> other) 
            => key.CompareTo(other.key);

        public void Deconstruct(out TKey key, out TValue value)
        {
            key = this.key;
            value = this.value;
        }
    }
}