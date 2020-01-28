//Caution : This is a generated file so change in this file might disapeare.
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace BlueDove.VectorMath
{
    public static partial class VectorOperations
    {

        private struct FloatAdd : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float left, float right)
                => left + right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatAddSse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> left, Vector128<float> right)
                => Sse.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatAddAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> left, Vector256<float> right)
                => Avx.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct FloatSubtract : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float left, float right)
                => left - right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatSubtractSse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> left, Vector128<float> right)
                => Sse.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatSubtractAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> left, Vector256<float> right)
                => Avx.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct FloatMultiply : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float left, float right)
                => left * right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatMultiplySse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> left, Vector128<float> right)
                => Sse.Multiply(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatMultiplyAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> left, Vector256<float> right)
                => Avx.Multiply(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct FloatDivide : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float left, float right)
                => left / right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatDivideSse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> left, Vector128<float> right)
                => Sse.Divide(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatDivideAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> left, Vector256<float> right)
                => Avx.Divide(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleAdd : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double left, double right)
                => left + right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleAddSse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> left, Vector128<double> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleAddAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> left, Vector256<double> right)
                => Avx.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleSubtract : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double left, double right)
                => left - right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleSubtractSse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> left, Vector128<double> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleSubtractAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> left, Vector256<double> right)
                => Avx.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleMultiply : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double left, double right)
                => left * right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleMultiplySse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> left, Vector128<double> right)
                => Sse2.Multiply(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleMultiplyAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> left, Vector256<double> right)
                => Avx.Multiply(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleDivide : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double left, double right)
                => left / right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleDivideSse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> left, Vector128<double> right)
                => Sse2.Divide(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleDivideAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> left, Vector256<double> right)
                => Avx.Divide(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct SByteAdd : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte left, sbyte right)
                => (sbyte)((int)left + (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteAddSse2 : IBinomialOperator<Vector128<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<sbyte> Calc(Vector128<sbyte> left, Vector128<sbyte> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct SByteAddAvx2 : IBinomialOperator<Vector256<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<sbyte> Calc(Vector256<sbyte> left, Vector256<sbyte> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct SByteSubtract : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte left, sbyte right)
                => (sbyte)((int)left - (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteSubtractSse2 : IBinomialOperator<Vector128<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<sbyte> Calc(Vector128<sbyte> left, Vector128<sbyte> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct SByteSubtractAvx2 : IBinomialOperator<Vector256<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<sbyte> Calc(Vector256<sbyte> left, Vector256<sbyte> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct SByteAnd : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte left, sbyte right)
                => (sbyte)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteAndSse2 : IBinomialOperator<Vector128<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<sbyte> Calc(Vector128<sbyte> left, Vector128<sbyte> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct SByteAndAvx2 : IBinomialOperator<Vector256<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<sbyte> Calc(Vector256<sbyte> left, Vector256<sbyte> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct SByteOr : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte left, sbyte right)
                => (sbyte)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteOrSse2 : IBinomialOperator<Vector128<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<sbyte> Calc(Vector128<sbyte> left, Vector128<sbyte> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct SByteOrAvx2 : IBinomialOperator<Vector256<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<sbyte> Calc(Vector256<sbyte> left, Vector256<sbyte> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct SByteXor : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte left, sbyte right)
                => (sbyte)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteXorSse2 : IBinomialOperator<Vector128<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<sbyte> Calc(Vector128<sbyte> left, Vector128<sbyte> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct SByteXorAvx2 : IBinomialOperator<Vector256<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<sbyte> Calc(Vector256<sbyte> left, Vector256<sbyte> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct SByteMultiply : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte left, sbyte right)
                => (sbyte)((int)left * (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteDivide : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte left, sbyte right)
                => (sbyte)((int)left / (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteAdd : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte left, byte right)
                => (byte)((uint)left + (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteAddSse2 : IBinomialOperator<Vector128<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<byte> Calc(Vector128<byte> left, Vector128<byte> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ByteAddAvx2 : IBinomialOperator<Vector256<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<byte> Calc(Vector256<byte> left, Vector256<byte> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ByteSubtract : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte left, byte right)
                => (byte)((uint)left - (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteSubtractSse2 : IBinomialOperator<Vector128<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<byte> Calc(Vector128<byte> left, Vector128<byte> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ByteSubtractAvx2 : IBinomialOperator<Vector256<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<byte> Calc(Vector256<byte> left, Vector256<byte> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ByteAnd : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte left, byte right)
                => (byte)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteAndSse2 : IBinomialOperator<Vector128<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<byte> Calc(Vector128<byte> left, Vector128<byte> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ByteAndAvx2 : IBinomialOperator<Vector256<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<byte> Calc(Vector256<byte> left, Vector256<byte> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ByteOr : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte left, byte right)
                => (byte)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteOrSse2 : IBinomialOperator<Vector128<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<byte> Calc(Vector128<byte> left, Vector128<byte> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ByteOrAvx2 : IBinomialOperator<Vector256<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<byte> Calc(Vector256<byte> left, Vector256<byte> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ByteXor : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte left, byte right)
                => (byte)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteXorSse2 : IBinomialOperator<Vector128<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<byte> Calc(Vector128<byte> left, Vector128<byte> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ByteXorAvx2 : IBinomialOperator<Vector256<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<byte> Calc(Vector256<byte> left, Vector256<byte> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ByteMultiply : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte left, byte right)
                => (byte)((uint)left * (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteDivide : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte left, byte right)
                => (byte)((uint)left / (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortAdd : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short left, short right)
                => (short)((int)left + (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortAddSse2 : IBinomialOperator<Vector128<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<short> Calc(Vector128<short> left, Vector128<short> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ShortAddAvx2 : IBinomialOperator<Vector256<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<short> Calc(Vector256<short> left, Vector256<short> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ShortSubtract : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short left, short right)
                => (short)((int)left - (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortSubtractSse2 : IBinomialOperator<Vector128<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<short> Calc(Vector128<short> left, Vector128<short> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ShortSubtractAvx2 : IBinomialOperator<Vector256<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<short> Calc(Vector256<short> left, Vector256<short> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ShortAnd : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short left, short right)
                => (short)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortAndSse2 : IBinomialOperator<Vector128<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<short> Calc(Vector128<short> left, Vector128<short> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ShortAndAvx2 : IBinomialOperator<Vector256<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<short> Calc(Vector256<short> left, Vector256<short> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ShortOr : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short left, short right)
                => (short)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortOrSse2 : IBinomialOperator<Vector128<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<short> Calc(Vector128<short> left, Vector128<short> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ShortOrAvx2 : IBinomialOperator<Vector256<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<short> Calc(Vector256<short> left, Vector256<short> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ShortXor : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short left, short right)
                => (short)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortXorSse2 : IBinomialOperator<Vector128<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<short> Calc(Vector128<short> left, Vector128<short> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ShortXorAvx2 : IBinomialOperator<Vector256<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<short> Calc(Vector256<short> left, Vector256<short> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ShortMultiply : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short left, short right)
                => (short)((int)left * (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortDivide : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short left, short right)
                => (short)((int)left / (int)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortAdd : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort left, ushort right)
                => (ushort)((uint)left + (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortAddSse2 : IBinomialOperator<Vector128<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ushort> Calc(Vector128<ushort> left, Vector128<ushort> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UShortAddAvx2 : IBinomialOperator<Vector256<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ushort> Calc(Vector256<ushort> left, Vector256<ushort> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UShortSubtract : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort left, ushort right)
                => (ushort)((uint)left - (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortSubtractSse2 : IBinomialOperator<Vector128<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ushort> Calc(Vector128<ushort> left, Vector128<ushort> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UShortSubtractAvx2 : IBinomialOperator<Vector256<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ushort> Calc(Vector256<ushort> left, Vector256<ushort> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UShortAnd : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort left, ushort right)
                => (ushort)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortAndSse2 : IBinomialOperator<Vector128<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ushort> Calc(Vector128<ushort> left, Vector128<ushort> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UShortAndAvx2 : IBinomialOperator<Vector256<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ushort> Calc(Vector256<ushort> left, Vector256<ushort> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UShortOr : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort left, ushort right)
                => (ushort)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortOrSse2 : IBinomialOperator<Vector128<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ushort> Calc(Vector128<ushort> left, Vector128<ushort> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UShortOrAvx2 : IBinomialOperator<Vector256<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ushort> Calc(Vector256<ushort> left, Vector256<ushort> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UShortXor : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort left, ushort right)
                => (ushort)(left & right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortXorSse2 : IBinomialOperator<Vector128<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ushort> Calc(Vector128<ushort> left, Vector128<ushort> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UShortXorAvx2 : IBinomialOperator<Vector256<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ushort> Calc(Vector256<ushort> left, Vector256<ushort> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UShortMultiply : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort left, ushort right)
                => (ushort)((uint)left * (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortDivide : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort left, ushort right)
                => (ushort)((uint)left / (uint)right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntAdd : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left + right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntAddSse2 : IBinomialOperator<Vector128<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<int> Calc(Vector128<int> left, Vector128<int> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct IntAddAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> left, Vector256<int> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntSubtract : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left - right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntSubtractSse2 : IBinomialOperator<Vector128<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<int> Calc(Vector128<int> left, Vector128<int> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct IntSubtractAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> left, Vector256<int> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntAnd : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left & right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntAndSse2 : IBinomialOperator<Vector128<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<int> Calc(Vector128<int> left, Vector128<int> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct IntAndAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> left, Vector256<int> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntOr : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left | right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntOrSse2 : IBinomialOperator<Vector128<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<int> Calc(Vector128<int> left, Vector128<int> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct IntOrAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> left, Vector256<int> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntXor : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left ^ right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntXorSse2 : IBinomialOperator<Vector128<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<int> Calc(Vector128<int> left, Vector128<int> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct IntXorAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> left, Vector256<int> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntAndNot : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left & ~right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntAndNotSse2 : IBinomialOperator<Vector128<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<int> Calc(Vector128<int> left, Vector128<int> right)
                => Sse2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct IntAndNotAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> left, Vector256<int> right)
                => Avx2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntMultiply : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left * right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntDivide : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int left, int right)
                => left / right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntAdd : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left + right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntAddSse2 : IBinomialOperator<Vector128<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<uint> Calc(Vector128<uint> left, Vector128<uint> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UIntAddAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> left, Vector256<uint> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntSubtract : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left - right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntSubtractSse2 : IBinomialOperator<Vector128<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<uint> Calc(Vector128<uint> left, Vector128<uint> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UIntSubtractAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> left, Vector256<uint> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntAnd : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left & right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntAndSse2 : IBinomialOperator<Vector128<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<uint> Calc(Vector128<uint> left, Vector128<uint> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UIntAndAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> left, Vector256<uint> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntOr : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left | right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntOrSse2 : IBinomialOperator<Vector128<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<uint> Calc(Vector128<uint> left, Vector128<uint> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UIntOrAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> left, Vector256<uint> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntXor : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left ^ right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntXorSse2 : IBinomialOperator<Vector128<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<uint> Calc(Vector128<uint> left, Vector128<uint> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UIntXorAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> left, Vector256<uint> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntAndNot : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left & ~right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntAndNotSse2 : IBinomialOperator<Vector128<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<uint> Calc(Vector128<uint> left, Vector128<uint> right)
                => Sse2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct UIntAndNotAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> left, Vector256<uint> right)
                => Avx2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntMultiply : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left * right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntDivide : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint left, uint right)
                => left / right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongAdd : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left + right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongAddSse2 : IBinomialOperator<Vector128<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<long> Calc(Vector128<long> left, Vector128<long> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct LongAddAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> left, Vector256<long> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongSubtract : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left - right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongSubtractSse2 : IBinomialOperator<Vector128<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<long> Calc(Vector128<long> left, Vector128<long> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct LongSubtractAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> left, Vector256<long> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongAnd : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left & right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongAndSse2 : IBinomialOperator<Vector128<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<long> Calc(Vector128<long> left, Vector128<long> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct LongAndAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> left, Vector256<long> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongOr : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left | right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongOrSse2 : IBinomialOperator<Vector128<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<long> Calc(Vector128<long> left, Vector128<long> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct LongOrAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> left, Vector256<long> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongXor : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left ^ right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongXorSse2 : IBinomialOperator<Vector128<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<long> Calc(Vector128<long> left, Vector128<long> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct LongXorAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> left, Vector256<long> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongAndNot : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left & ~right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongAndNotSse2 : IBinomialOperator<Vector128<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<long> Calc(Vector128<long> left, Vector128<long> right)
                => Sse2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct LongAndNotAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> left, Vector256<long> right)
                => Avx2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongMultiply : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left * right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongDivide : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long left, long right)
                => left / right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongAdd : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left + right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongAddSse2 : IBinomialOperator<Vector128<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ulong> Calc(Vector128<ulong> left, Vector128<ulong> right)
                => Sse2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ULongAddAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> left, Vector256<ulong> right)
                => Avx2.Add(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongSubtract : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left - right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongSubtractSse2 : IBinomialOperator<Vector128<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ulong> Calc(Vector128<ulong> left, Vector128<ulong> right)
                => Sse2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ULongSubtractAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> left, Vector256<ulong> right)
                => Avx2.Subtract(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongAnd : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left & right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongAndSse2 : IBinomialOperator<Vector128<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ulong> Calc(Vector128<ulong> left, Vector128<ulong> right)
                => Sse2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ULongAndAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> left, Vector256<ulong> right)
                => Avx2.And(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongOr : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left | right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongOrSse2 : IBinomialOperator<Vector128<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ulong> Calc(Vector128<ulong> left, Vector128<ulong> right)
                => Sse2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ULongOrAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> left, Vector256<ulong> right)
                => Avx2.Or(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongXor : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left ^ right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongXorSse2 : IBinomialOperator<Vector128<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ulong> Calc(Vector128<ulong> left, Vector128<ulong> right)
                => Sse2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ULongXorAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> left, Vector256<ulong> right)
                => Avx2.Xor(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongAndNot : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left & ~right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongAndNotSse2 : IBinomialOperator<Vector128<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<ulong> Calc(Vector128<ulong> left, Vector128<ulong> right)
                => Sse2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct ULongAndNotAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> left, Vector256<ulong> right)
                => Avx2.AndNot(left, right);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongMultiply : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left * right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongDivide : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong left, ulong right)
                => left / right;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }
    }
}

