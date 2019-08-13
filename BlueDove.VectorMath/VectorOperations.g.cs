//Caution : This is a generated file so change in this file might disapeare.
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace BlueDove.VectorMath
{
    public static partial class VectorOperations
    {
        
        public static void Add(ReadOnlySpan<float> l, ReadOnlySpan<float> r, Span<float> target)
            => OperationBase<float, FloatAddAvx, FloatAddSse, FloatAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<float> l, ReadOnlySpan<float> r, Span<float> target)
            => OperationBase<float, FloatSubtractAvx, FloatSubtractSse, FloatSubtract>(l, r, target);

        public static void Multiply(ReadOnlySpan<float> l, ReadOnlySpan<float> r, Span<float> target)
            => OperationBase<float, FloatMultiplyAvx, FloatMultiplySse, FloatMultiply>(l, r, target);

        public static void Divide(ReadOnlySpan<float> l, ReadOnlySpan<float> r, Span<float> target)
            => OperationBase<float, FloatDivideAvx, FloatDivideSse, FloatDivide>(l, r, target);

        public static void Add(ReadOnlySpan<double> l, ReadOnlySpan<double> r, Span<double> target)
            => OperationBase<double, DoubleAddAvx, DoubleAddSse2, DoubleAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<double> l, ReadOnlySpan<double> r, Span<double> target)
            => OperationBase<double, DoubleSubtractAvx, DoubleSubtractSse2, DoubleSubtract>(l, r, target);

        public static void Multiply(ReadOnlySpan<double> l, ReadOnlySpan<double> r, Span<double> target)
            => OperationBase<double, DoubleMultiplyAvx, DoubleMultiplySse2, DoubleMultiply>(l, r, target);

        public static void Divide(ReadOnlySpan<double> l, ReadOnlySpan<double> r, Span<double> target)
            => OperationBase<double, DoubleDivideAvx, DoubleDivideSse2, DoubleDivide>(l, r, target);

        public static void Add(ReadOnlySpan<sbyte> l, ReadOnlySpan<sbyte> r, Span<sbyte> target)
            => OperationBase<sbyte, SByteAddAvx2, NilOperator<Vector128<sbyte>>, SByteAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<sbyte> l, ReadOnlySpan<sbyte> r, Span<sbyte> target)
            => OperationBase<sbyte, SByteSubtractAvx2, NilOperator<Vector128<sbyte>>, SByteSubtract>(l, r, target);

        public static void Add(ReadOnlySpan<byte> l, ReadOnlySpan<byte> r, Span<byte> target)
            => OperationBase<byte, ByteAddAvx2, NilOperator<Vector128<byte>>, ByteAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<byte> l, ReadOnlySpan<byte> r, Span<byte> target)
            => OperationBase<byte, ByteSubtractAvx2, NilOperator<Vector128<byte>>, ByteSubtract>(l, r, target);

        public static void Add(ReadOnlySpan<short> l, ReadOnlySpan<short> r, Span<short> target)
            => OperationBase<short, ShortAddAvx2, NilOperator<Vector128<short>>, ShortAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<short> l, ReadOnlySpan<short> r, Span<short> target)
            => OperationBase<short, ShortSubtractAvx2, NilOperator<Vector128<short>>, ShortSubtract>(l, r, target);

        public static void Add(ReadOnlySpan<ushort> l, ReadOnlySpan<ushort> r, Span<ushort> target)
            => OperationBase<ushort, UShortAddAvx2, NilOperator<Vector128<ushort>>, UShortAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<ushort> l, ReadOnlySpan<ushort> r, Span<ushort> target)
            => OperationBase<ushort, UShortSubtractAvx2, NilOperator<Vector128<ushort>>, UShortSubtract>(l, r, target);

        public static void Add(ReadOnlySpan<int> l, ReadOnlySpan<int> r, Span<int> target)
            => OperationBase<int, IntAddAvx2, NilOperator<Vector128<int>>, IntAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<int> l, ReadOnlySpan<int> r, Span<int> target)
            => OperationBase<int, IntSubtractAvx2, NilOperator<Vector128<int>>, IntSubtract>(l, r, target);

        public static void Add(ReadOnlySpan<uint> l, ReadOnlySpan<uint> r, Span<uint> target)
            => OperationBase<uint, UIntAddAvx2, NilOperator<Vector128<uint>>, UIntAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<uint> l, ReadOnlySpan<uint> r, Span<uint> target)
            => OperationBase<uint, UIntSubtractAvx2, NilOperator<Vector128<uint>>, UIntSubtract>(l, r, target);

        public static void Add(ReadOnlySpan<long> l, ReadOnlySpan<long> r, Span<long> target)
            => OperationBase<long, LongAddAvx2, NilOperator<Vector128<long>>, LongAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<long> l, ReadOnlySpan<long> r, Span<long> target)
            => OperationBase<long, LongSubtractAvx2, NilOperator<Vector128<long>>, LongSubtract>(l, r, target);

        public static void Add(ReadOnlySpan<ulong> l, ReadOnlySpan<ulong> r, Span<ulong> target)
            => OperationBase<ulong, ULongAddAvx2, NilOperator<Vector128<ulong>>, ULongAdd>(l, r, target);

        public static void Subtract(ReadOnlySpan<ulong> l, ReadOnlySpan<ulong> r, Span<ulong> target)
            => OperationBase<ulong, ULongSubtractAvx2, NilOperator<Vector128<ulong>>, ULongSubtract>(l, r, target);
    }
}

