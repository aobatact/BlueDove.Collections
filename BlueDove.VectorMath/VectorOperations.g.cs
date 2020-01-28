//Caution : This is a generated file so change in this file might disapeare.
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace BlueDove.VectorMath
{
    public static partial class VectorOperations
    {

        public static void Add(ReadOnlySpan<float> left, ReadOnlySpan<float> right, Span<float> target)
            => OperationBase<float, FloatAddAvx, FloatAddSse, FloatAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<float> left, ReadOnlySpan<float> right, Span<float> target)
            => OperationBase<float, FloatSubtractAvx, FloatSubtractSse, FloatSubtract>(left, right, target);

        public static void Multiply(ReadOnlySpan<float> left, ReadOnlySpan<float> right, Span<float> target)
            => OperationBase<float, FloatMultiplyAvx, FloatMultiplySse, FloatMultiply>(left, right, target);

        public static void Divide(ReadOnlySpan<float> left, ReadOnlySpan<float> right, Span<float> target)
            => OperationBase<float, FloatDivideAvx, FloatDivideSse, FloatDivide>(left, right, target);

        public static void Add(ReadOnlySpan<double> left, ReadOnlySpan<double> right, Span<double> target)
            => OperationBase<double, DoubleAddAvx, DoubleAddSse2, DoubleAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<double> left, ReadOnlySpan<double> right, Span<double> target)
            => OperationBase<double, DoubleSubtractAvx, DoubleSubtractSse2, DoubleSubtract>(left, right, target);

        public static void Multiply(ReadOnlySpan<double> left, ReadOnlySpan<double> right, Span<double> target)
            => OperationBase<double, DoubleMultiplyAvx, DoubleMultiplySse2, DoubleMultiply>(left, right, target);

        public static void Divide(ReadOnlySpan<double> left, ReadOnlySpan<double> right, Span<double> target)
            => OperationBase<double, DoubleDivideAvx, DoubleDivideSse2, DoubleDivide>(left, right, target);

        public static void Add(ReadOnlySpan<sbyte> left, ReadOnlySpan<sbyte> right, Span<sbyte> target)
            => OperationBase<sbyte, SByteAddAvx2, SByteAddSse2, SByteAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<sbyte> left, ReadOnlySpan<sbyte> right, Span<sbyte> target)
            => OperationBase<sbyte, SByteSubtractAvx2, SByteSubtractSse2, SByteSubtract>(left, right, target);

        public static void And(ReadOnlySpan<sbyte> left, ReadOnlySpan<sbyte> right, Span<sbyte> target)
            => OperationBase<sbyte, SByteAndAvx2, SByteAndSse2, SByteAnd>(left, right, target);

        public static void Or(ReadOnlySpan<sbyte> left, ReadOnlySpan<sbyte> right, Span<sbyte> target)
            => OperationBase<sbyte, SByteOrAvx2, SByteOrSse2, SByteOr>(left, right, target);

        public static void Xor(ReadOnlySpan<sbyte> left, ReadOnlySpan<sbyte> right, Span<sbyte> target)
            => OperationBase<sbyte, SByteXorAvx2, SByteXorSse2, SByteXor>(left, right, target);

        public static void Add(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right, Span<byte> target)
            => OperationBase<byte, ByteAddAvx2, ByteAddSse2, ByteAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right, Span<byte> target)
            => OperationBase<byte, ByteSubtractAvx2, ByteSubtractSse2, ByteSubtract>(left, right, target);

        public static void And(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right, Span<byte> target)
            => OperationBase<byte, ByteAndAvx2, ByteAndSse2, ByteAnd>(left, right, target);

        public static void Or(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right, Span<byte> target)
            => OperationBase<byte, ByteOrAvx2, ByteOrSse2, ByteOr>(left, right, target);

        public static void Xor(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right, Span<byte> target)
            => OperationBase<byte, ByteXorAvx2, ByteXorSse2, ByteXor>(left, right, target);

        public static void Add(ReadOnlySpan<short> left, ReadOnlySpan<short> right, Span<short> target)
            => OperationBase<short, ShortAddAvx2, ShortAddSse2, ShortAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<short> left, ReadOnlySpan<short> right, Span<short> target)
            => OperationBase<short, ShortSubtractAvx2, ShortSubtractSse2, ShortSubtract>(left, right, target);

        public static void And(ReadOnlySpan<short> left, ReadOnlySpan<short> right, Span<short> target)
            => OperationBase<short, ShortAndAvx2, ShortAndSse2, ShortAnd>(left, right, target);

        public static void Or(ReadOnlySpan<short> left, ReadOnlySpan<short> right, Span<short> target)
            => OperationBase<short, ShortOrAvx2, ShortOrSse2, ShortOr>(left, right, target);

        public static void Xor(ReadOnlySpan<short> left, ReadOnlySpan<short> right, Span<short> target)
            => OperationBase<short, ShortXorAvx2, ShortXorSse2, ShortXor>(left, right, target);

        public static void Add(ReadOnlySpan<ushort> left, ReadOnlySpan<ushort> right, Span<ushort> target)
            => OperationBase<ushort, UShortAddAvx2, UShortAddSse2, UShortAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<ushort> left, ReadOnlySpan<ushort> right, Span<ushort> target)
            => OperationBase<ushort, UShortSubtractAvx2, UShortSubtractSse2, UShortSubtract>(left, right, target);

        public static void And(ReadOnlySpan<ushort> left, ReadOnlySpan<ushort> right, Span<ushort> target)
            => OperationBase<ushort, UShortAndAvx2, UShortAndSse2, UShortAnd>(left, right, target);

        public static void Or(ReadOnlySpan<ushort> left, ReadOnlySpan<ushort> right, Span<ushort> target)
            => OperationBase<ushort, UShortOrAvx2, UShortOrSse2, UShortOr>(left, right, target);

        public static void Xor(ReadOnlySpan<ushort> left, ReadOnlySpan<ushort> right, Span<ushort> target)
            => OperationBase<ushort, UShortXorAvx2, UShortXorSse2, UShortXor>(left, right, target);

        public static void Add(ReadOnlySpan<int> left, ReadOnlySpan<int> right, Span<int> target)
            => OperationBase<int, IntAddAvx2, IntAddSse2, IntAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<int> left, ReadOnlySpan<int> right, Span<int> target)
            => OperationBase<int, IntSubtractAvx2, IntSubtractSse2, IntSubtract>(left, right, target);

        public static void And(ReadOnlySpan<int> left, ReadOnlySpan<int> right, Span<int> target)
            => OperationBase<int, IntAndAvx2, IntAndSse2, IntAnd>(left, right, target);

        public static void Or(ReadOnlySpan<int> left, ReadOnlySpan<int> right, Span<int> target)
            => OperationBase<int, IntOrAvx2, IntOrSse2, IntOr>(left, right, target);

        public static void Xor(ReadOnlySpan<int> left, ReadOnlySpan<int> right, Span<int> target)
            => OperationBase<int, IntXorAvx2, IntXorSse2, IntXor>(left, right, target);

        public static void AndNot(ReadOnlySpan<int> left, ReadOnlySpan<int> right, Span<int> target)
            => OperationBase<int, IntAndNotAvx2, IntAndNotSse2, IntAndNot>(left, right, target);

        public static void Add(ReadOnlySpan<uint> left, ReadOnlySpan<uint> right, Span<uint> target)
            => OperationBase<uint, UIntAddAvx2, UIntAddSse2, UIntAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<uint> left, ReadOnlySpan<uint> right, Span<uint> target)
            => OperationBase<uint, UIntSubtractAvx2, UIntSubtractSse2, UIntSubtract>(left, right, target);

        public static void And(ReadOnlySpan<uint> left, ReadOnlySpan<uint> right, Span<uint> target)
            => OperationBase<uint, UIntAndAvx2, UIntAndSse2, UIntAnd>(left, right, target);

        public static void Or(ReadOnlySpan<uint> left, ReadOnlySpan<uint> right, Span<uint> target)
            => OperationBase<uint, UIntOrAvx2, UIntOrSse2, UIntOr>(left, right, target);

        public static void Xor(ReadOnlySpan<uint> left, ReadOnlySpan<uint> right, Span<uint> target)
            => OperationBase<uint, UIntXorAvx2, UIntXorSse2, UIntXor>(left, right, target);

        public static void AndNot(ReadOnlySpan<uint> left, ReadOnlySpan<uint> right, Span<uint> target)
            => OperationBase<uint, UIntAndNotAvx2, UIntAndNotSse2, UIntAndNot>(left, right, target);

        public static void Add(ReadOnlySpan<long> left, ReadOnlySpan<long> right, Span<long> target)
            => OperationBase<long, LongAddAvx2, LongAddSse2, LongAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<long> left, ReadOnlySpan<long> right, Span<long> target)
            => OperationBase<long, LongSubtractAvx2, LongSubtractSse2, LongSubtract>(left, right, target);

        public static void And(ReadOnlySpan<long> left, ReadOnlySpan<long> right, Span<long> target)
            => OperationBase<long, LongAndAvx2, LongAndSse2, LongAnd>(left, right, target);

        public static void Or(ReadOnlySpan<long> left, ReadOnlySpan<long> right, Span<long> target)
            => OperationBase<long, LongOrAvx2, LongOrSse2, LongOr>(left, right, target);

        public static void Xor(ReadOnlySpan<long> left, ReadOnlySpan<long> right, Span<long> target)
            => OperationBase<long, LongXorAvx2, LongXorSse2, LongXor>(left, right, target);

        public static void AndNot(ReadOnlySpan<long> left, ReadOnlySpan<long> right, Span<long> target)
            => OperationBase<long, LongAndNotAvx2, LongAndNotSse2, LongAndNot>(left, right, target);

        public static void Add(ReadOnlySpan<ulong> left, ReadOnlySpan<ulong> right, Span<ulong> target)
            => OperationBase<ulong, ULongAddAvx2, ULongAddSse2, ULongAdd>(left, right, target);

        public static void Subtract(ReadOnlySpan<ulong> left, ReadOnlySpan<ulong> right, Span<ulong> target)
            => OperationBase<ulong, ULongSubtractAvx2, ULongSubtractSse2, ULongSubtract>(left, right, target);

        public static void And(ReadOnlySpan<ulong> left, ReadOnlySpan<ulong> right, Span<ulong> target)
            => OperationBase<ulong, ULongAndAvx2, ULongAndSse2, ULongAnd>(left, right, target);

        public static void Or(ReadOnlySpan<ulong> left, ReadOnlySpan<ulong> right, Span<ulong> target)
            => OperationBase<ulong, ULongOrAvx2, ULongOrSse2, ULongOr>(left, right, target);

        public static void Xor(ReadOnlySpan<ulong> left, ReadOnlySpan<ulong> right, Span<ulong> target)
            => OperationBase<ulong, ULongXorAvx2, ULongXorSse2, ULongXor>(left, right, target);

        public static void AndNot(ReadOnlySpan<ulong> left, ReadOnlySpan<ulong> right, Span<ulong> target)
            => OperationBase<ulong, ULongAndNotAvx2, ULongAndNotSse2, ULongAndNot>(left, right, target);
    }
}

