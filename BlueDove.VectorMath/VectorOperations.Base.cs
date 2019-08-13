using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace BlueDove.VectorMath
{
    public static partial class VectorOperations
    {
        private interface IBinomialOperator<T>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            T Calc(T l, T r);
            bool IsSupported { [MethodImpl(MethodImplOptions.AggressiveInlining)]get; }
        }

        private struct NilOperator<T> : IBinomialOperator<T>
        {
            public T Calc(T l, T r) => default;

            public bool IsSupported => false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase<T, T256, T128, TSerial>(ReadOnlySpan<T> l, ReadOnlySpan<T> r,
            Span<T> target)
            where T : unmanaged
            where T256 : unmanaged, IBinomialOperator<Vector256<T>>
            where T128 : unmanaged, IBinomialOperator<Vector128<T>>
            where TSerial : unmanaged, IBinomialOperator<T>
        {
            Debug.Assert(l.Length == r.Length);
            Debug.Assert(l.Length == target.Length);
            if (default(T256).IsSupported )
            {
                if (l.Length > Vector256<double>.Count)
                {
                    OperationBase256<T, T256>(l.Length, ref MemoryMarshal.GetReference(l), ref MemoryMarshal.GetReference(r),
                        ref MemoryMarshal.GetReference(target));
                    return;
                }
            }
            else if (default(T128).IsSupported )
            {
                if (l.Length > Vector128<double>.Count)
                {
                    OperationBase128<T, T128>(l.Length, ref MemoryMarshal.GetReference(l), ref MemoryMarshal.GetReference(r),
                        ref MemoryMarshal.GetReference(target));
                    return;
                }
            }

            {
                OperationBase<T, TSerial>(l.Length, ref MemoryMarshal.GetReference(l), ref MemoryMarshal.GetReference(r),
                    ref MemoryMarshal.GetReference(target));
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase<T, TOperator>(int length, ref T l, ref T r, ref T t)
            where T : unmanaged where TOperator : struct, IBinomialOperator<T>
        {
            Debug.Assert(default(TOperator).IsSupported);
            for (var i = 0; i < length; i++)
            {
                Unsafe.Add(ref t, i) = default(TOperator).Calc(Unsafe.Add(ref l, i), Unsafe.Add(ref r, i));
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase128<T, TSIMDOperator>(int length, ref T l, ref T r, ref T t)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector128<T>>
        {
            Debug.Assert(length >= Vector128<T>.Count);
            Debug.Assert(default(TSIMDOperator).IsSupported);
            int i = 0;
            Loop:
            do
            {
                Unsafe.As<T, Vector128<T>>(ref Unsafe.Add(ref t, i)) = default(TSIMDOperator).Calc(
                    Unsafe.As<T, Vector128<T>>(ref Unsafe.Add(ref l, i)),
                    Unsafe.As<T, Vector128<T>>(ref Unsafe.Add(ref r, i)));
                i += Vector128<T>.Count;
            } while (i < length);

            if (i != length)
            {
                i = length - Vector256<T>.Count;
                goto Loop;
            }
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase256<T, TSIMDOperator>(int length, ref T l, ref T r, ref T t)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector256<T>>
        {
            Debug.Assert(length >= Vector256<T>.Count);
            Debug.Assert(default(TSIMDOperator).IsSupported);
            int i = 0;
            Loop:
            do
            {
                Unsafe.As<T, Vector256<T>>(ref Unsafe.Add(ref t, i)) = default(TSIMDOperator).Calc(
                    Unsafe.As<T, Vector256<T>>(ref Unsafe.Add(ref l, i)),
                    Unsafe.As<T, Vector256<T>>(ref Unsafe.Add(ref r, i)));
                i += Vector256<T>.Count;
            } while (i < length);

            if (i != length)
            {
                i = length - Vector256<T>.Count;
                goto Loop;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void OperationBase128Aligned<T, TSIMDOperator>(int length, ref T l, ref T r, ref T t,
            TSIMDOperator simdOperator)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector128<T>>
        {
            
            Debug.Assert(length >= Vector128<T>.Count);
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void OperationBase256Aligned<T, TSIMDOperator>(int length, ref T l, ref T r, ref T t)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector256<T>>
        {
            Debug.Assert(length >= Vector256<T>.Count);
            Debug.Assert(UnalignedCountVector256(ref Unsafe.As<T, byte>(ref l)) != (IntPtr) 0 ||
                         UnalignedCountVector256(ref Unsafe.As<T, byte>(ref r)) != (IntPtr) 0 ||
                         UnalignedCountVector256(ref Unsafe.As<T, byte>(ref t)) != (IntPtr) 0);
            Debug.Assert(Avx.IsSupported);
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe IntPtr UnalignedCountVector128(ref byte searchSpace)
        {
            var unaligned = (int)Unsafe.AsPointer(ref searchSpace) & (Vector128<byte>.Count - 1);
            return (IntPtr)((Vector128<byte>.Count - unaligned) & (Vector128<byte>.Count - 1));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe IntPtr UnalignedCountVector256(ref byte searchSpace)
        {
            var unaligned = (int)Unsafe.AsPointer(ref searchSpace) & (Vector256<byte>.Count - 1);
            return (IntPtr)((Vector256<byte>.Count - unaligned) & (Vector256<byte>.Count - 1));
        }
        
        private static void ThrowNotSupported() => throw new NotSupportedException();
    }
}