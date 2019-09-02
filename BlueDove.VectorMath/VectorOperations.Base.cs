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
        private interface IBinomialOperator<T> : IBinomialOperator<T, T, T>
        {
        }

        private interface IBinomialOperator<in TLeft, in TRight, out TResult>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            TResult Calc(TLeft l, TRight r);

            bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get;
            }
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
            if (default(T256).IsSupported)
            {
                if (left.Length > Vector256<double>.Count)
                {
                    OperationBase256<T, T, T, T256>(left.Length, ref MemoryMarshal.GetReference(left),
                        ref MemoryMarshal.GetReference(right),
                        ref MemoryMarshal.GetReference(target));
                    return;
                }
            }
            else if (default(T128).IsSupported)
            {
                if (left.Length > Vector128<double>.Count)
                {
                    OperationBase128<T, T, T, T128>(left.Length, ref MemoryMarshal.GetReference(left),
                        ref MemoryMarshal.GetReference(right),
                        ref MemoryMarshal.GetReference(target));
                    return;
                }
            }

            {
                OperationBase<T, T, T, TSerial>(left.Length, ref MemoryMarshal.GetReference(left),
                    ref MemoryMarshal.GetReference(right),
                    ref MemoryMarshal.GetReference(target));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase<TLeft, TRight, TResult, TOperator>(int length, ref TLeft left,
            ref TRight right, ref TResult target)
            where TLeft : unmanaged
            where TRight : unmanaged
            where TResult : unmanaged
            where TOperator : struct, IBinomialOperator<TLeft,TRight,TResult>
        {
            Debug.Assert(default(TOperator).IsSupported);
            Debug.Assert(Unsafe.SizeOf<TLeft>() == Unsafe.SizeOf<TRight>());
            for (var i = 0; i < length; i++)
            {
                Unsafe.Add(ref target, i) = default(TOperator).Calc(Unsafe.Add(ref left, i), Unsafe.Add(ref right, i));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase128<TLeft, TRight, TResult, TOperator>(int length, ref TLeft left,
            ref TRight right, ref TResult target)
            where TLeft : unmanaged
            where TRight : unmanaged
            where TResult : unmanaged
            where TOperator : struct, IBinomialOperator<Vector128<TLeft>,Vector128<TRight>,Vector128<TResult>>
        {
            Debug.Assert(length >= Vector128<TLeft>.Count);
            Debug.Assert(default(TOperator).IsSupported);
            Debug.Assert(Unsafe.SizeOf<TLeft>() == Unsafe.SizeOf<TRight>());
            int i = 0;
            Loop:
            do
            {
                Unsafe.As<TResult, Vector128<TResult>>(ref Unsafe.Add(ref target, i)) = default(TOperator).Calc(
                    Unsafe.As<TLeft, Vector128<TLeft>>(ref Unsafe.Add(ref left, i)),
                    Unsafe.As<TRight, Vector128<TRight>>(ref Unsafe.Add(ref right, i)));
                i += Vector128<TLeft>.Count;
            } while (i < length);

            if (i != length)
            {
                i = length - Vector128<TLeft>.Count;
                goto Loop;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void OperationBase256<TLeft, TRight, TResult, TOperator>(int length, ref TLeft left,
            ref TRight right, ref TResult target)
            where TLeft : unmanaged
            where TRight : unmanaged
            where TResult : unmanaged
            where TOperator : struct, IBinomialOperator<Vector256<TLeft>,Vector256<TRight>,Vector256<TResult>>
        {
            Debug.Assert(length >= Vector256<TLeft>.Count);
            Debug.Assert(default(TOperator).IsSupported);
            Debug.Assert(Unsafe.SizeOf<TLeft>() == Unsafe.SizeOf<TRight>());
            int i = 0;
            Loop:
            do
            {
                Unsafe.As<TResult, Vector256<TResult>>(ref Unsafe.Add(ref target, i)) = default(TOperator).Calc(
                    Unsafe.As<TLeft, Vector256<TLeft>>(ref Unsafe.Add(ref left, i)),
                    Unsafe.As<TRight, Vector256<TRight>>(ref Unsafe.Add(ref right, i)));
                i += Vector256<TLeft>.Count;
            } while (i < length);

            if (i != length)
            {
                i = length - Vector256<TLeft>.Count;
                goto Loop;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe IntPtr UnalignedCountVector128(ref byte searchSpace)
        {
            var unaligned = (int) Unsafe.AsPointer(ref searchSpace) & (Vector128<byte>.Count - 1);
            return (IntPtr) ((Vector128<byte>.Count - unaligned) & (Vector128<byte>.Count - 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe IntPtr UnalignedCountVector256(ref byte searchSpace)
        {
            var unaligned = (int) Unsafe.AsPointer(ref searchSpace) & (Vector256<byte>.Count - 1);
            return (IntPtr) ((Vector256<byte>.Count - unaligned) & (Vector256<byte>.Count - 1));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowNotSupported() => throw new NotSupportedException();
    }
}