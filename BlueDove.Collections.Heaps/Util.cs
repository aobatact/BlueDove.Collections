using System;
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
    
    
#if NETSTANDARD2_0
    internal class MaybeNullWhenAttribute : Attribute
    {
        public MaybeNullWhenAttribute(bool b) { }
    }
#endif
}