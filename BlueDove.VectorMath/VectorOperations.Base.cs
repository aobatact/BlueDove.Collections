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
        private static void OperationBase<T, T256, T128, TSerial>(ReadOnlySpan<T> left, ReadOnlySpan<T> right,
            Span<T> target)
            where T : unmanaged
            where T256 : unmanaged, IBinomialOperator<Vector256<T>>
            where T128 : unmanaged, IBinomialOperator<Vector128<T>>
            where TSerial : unmanaged, IBinomialOperator<T>
        {
            Debug.Assert(left.Length == right.Length);
            Debug.Assert(left.Length == target.Length);
            if (default(T256).IsSupported )
            {
                if (left.Length > Vector256<double>.Count)
                {
                    OperationBase256<T, T256>(left.Length, ref MemoryMarshal.GetReference(left), ref MemoryMarshal.GetReference(right),
                        ref MemoryMarshal.GetReference(target));
                    return;
                }
            }
            else if (default(T128).IsSupported )
            {
                if (left.Length > Vector128<double>.Count)
                {
                    OperationBase128<T, T128>(left.Length, ref MemoryMarshal.GetReference(left), ref MemoryMarshal.GetReference(right),
                        ref MemoryMarshal.GetReference(target));
                    return;
                }
            }

            {
                OperationBase<T, TSerial>(left.Length, ref MemoryMarshal.GetReference(left), ref MemoryMarshal.GetReference(right),
                    ref MemoryMarshal.GetReference(target));
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase<T, TOperator>(int length, ref T left, ref T right, ref T t)
            where T : unmanaged where TOperator : struct, IBinomialOperator<T>
        {
            Debug.Assert(default(TOperator).IsSupported);
            for (var i = 0; i < length; i++)
            {
                Unsafe.Add(ref t, i) = default(TOperator).Calc(Unsafe.Add(ref left, i), Unsafe.Add(ref right, i));
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase128<T, TSIMDOperator>(int length, ref T left, ref T right, ref T target)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector128<T>>
        {
            Debug.Assert(length >= Vector128<T>.Count);
            Debug.Assert(default(TSIMDOperator).IsSupported);
            int i = 0;
            Loop:
            do
            {
                Unsafe.As<T, Vector128<T>>(ref Unsafe.Add(ref target, i)) = default(TSIMDOperator).Calc(
                    Unsafe.As<T, Vector128<T>>(ref Unsafe.Add(ref left, i)),
                    Unsafe.As<T, Vector128<T>>(ref Unsafe.Add(ref right, i)));
                i += Vector128<T>.Count;
            } while (i < length);

            if (i != length)
            {
                i = length - Vector256<T>.Count;
                goto Loop;
            }
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase256<T, TSIMDOperator>(int length, ref T left, ref T right, ref T target)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector256<T>>
        {
            Debug.Assert(length >= Vector256<T>.Count);
            Debug.Assert(default(TSIMDOperator).IsSupported);
            int i = 0;
            Loop:
            do
            {
                Unsafe.As<T, Vector256<T>>(ref Unsafe.Add(ref target, i)) = default(TSIMDOperator).Calc(
                    Unsafe.As<T, Vector256<T>>(ref Unsafe.Add(ref left, i)),
                    Unsafe.As<T, Vector256<T>>(ref Unsafe.Add(ref right, i)));
                i += Vector256<T>.Count;
            } while (i < length);

            if (i != length)
            {
                i = length - Vector256<T>.Count;
                goto Loop;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void OperationBase128Aligned<T, TSIMDOperator>(int length, ref T left, ref T right, ref T target,
            TSIMDOperator simdOperator)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector128<T>>
        {
            
            Debug.Assert(length >= Vector128<T>.Count);
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void OperationBase256Aligned<T, TSIMDOperator>(int length, ref T left, ref T right, ref T target)
            where T : unmanaged
            where TSIMDOperator : struct, IBinomialOperator<Vector256<T>>
        {
            Debug.Assert(length >= Vector256<T>.Count);
            Debug.Assert(UnalignedCountVector256(ref Unsafe.As<T, byte>(ref left)) != (IntPtr) 0 ||
                         UnalignedCountVector256(ref Unsafe.As<T, byte>(ref right)) != (IntPtr) 0 ||
                         UnalignedCountVector256(ref Unsafe.As<T, byte>(ref target)) != (IntPtr) 0);
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