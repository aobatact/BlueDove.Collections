using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Heaps
{
    internal static class BufferUtil
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Expand<T>(ref T[] buffer)
        {
            var nl = (uint)buffer.Length << 1;
            var nBuffer = new T[nl <= (uint) int.MaxValue ? nl :
                nl + (uint)(buffer.Length >> 1) <= (uint) int.MaxValue ? nl + (buffer.Length >> 1) : int.MaxValue];
            buffer.AsSpan().CopyTo(nBuffer);
            buffer = nBuffer;
        }
        
        [DoesNotReturn]
        public static void ThrowNoItem()
        {
            throw new InvalidOperationException("No Items in Heap");
        }
    }
}