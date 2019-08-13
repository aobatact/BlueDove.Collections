using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace BlueDove.VectorMath
{
    public static partial class VectorOperations
    {
        /*
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ReadOnlySpan<double> l, ReadOnlySpan<double> r, Span<double> target)
            => OperationBase<double, DoubleAddAvx, DoubleAddSse2, DoubleAdd>(l, r, target);

        private struct DoubleAddAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> l, Vector256<double> r)
                => Avx.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleAddSse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> l, Vector128<double> r)
                => Sse2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleAdd : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double l, double r)
                => l + r;

            public bool IsSupported => true;
        }
        */
    }
}