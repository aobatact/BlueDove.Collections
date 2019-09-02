using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace BlueDove.VectorMath
{
    public static class AlignedMemory
    {
        public static Memory<T> AllocAligned256<T>(int length) where T : unmanaged 
            => (new AlignedMemory256<T>(length)).CreateMemory();

        public static Memory<T> AllocAligned128<T>(int length) where T : unmanaged
            => (new AlignedMemory128<T>(length)).CreateMemory();
    }
    
    public class AlignedMemory256<T> : MemoryManager<T> where T : unmanaged
    {
        private Vector256<T>[] _values;

        public AlignedMemory256(int capacity)
        {
            _values = new Vector256<T>[(capacity + (Vector256<T>.Count - 1)) >>
                                       BitOperations.Log2((uint) Vector256<T>.Count)];
        }

        protected override void Dispose(bool disposing) => _values = null;

        public override Span<T> GetSpan() =>
            MemoryMarshal.CreateSpan(ref Unsafe.As<Vector256<T>, T>(ref _values[0]), 
                _values.Length * Vector256<T>.Count);

        public override unsafe MemoryHandle Pin(int elementIndex = 0) => new MemoryHandle(
                Unsafe.AsPointer(ref Unsafe.Add(ref Unsafe.As<Vector256<T>, T>(ref _values[0]), elementIndex)),
                GCHandle.Alloc(_values, GCHandleType.Pinned));

        public override void Unpin()
        {
        }

        public Memory<T> CreateMemory() => CreateMemory(_values.Length * Vector256<T>.Count);
    }
    
    public class AlignedMemory128<T> : MemoryManager<T> where T : unmanaged
    {
        private Vector128<T>[] _values;

        public AlignedMemory128(int capacity)
        {
            _values = new Vector128<T>[(capacity + (Vector128<T>.Count - 1)) >>
                                       BitOperations.Log2((uint) Vector128<T>.Count)];
        }

        protected override void Dispose(bool disposing) => _values = null;

        public override Span<T> GetSpan() =>
            MemoryMarshal.CreateSpan(ref Unsafe.As<Vector128<T>, T>(ref _values[0]), 
                _values.Length * Vector128<T>.Count);

        public override unsafe MemoryHandle Pin(int elementIndex = 0) => new MemoryHandle(
            Unsafe.AsPointer(ref Unsafe.Add(ref Unsafe.As<Vector128<T>, T>(ref _values[0]), elementIndex)),
            GCHandle.Alloc(_values, GCHandleType.Pinned));

        public override void Unpin()
        {
        }
        
        public Memory<T> CreateMemory() => CreateMemory(_values.Length * Vector256<T>.Count);
    }
}