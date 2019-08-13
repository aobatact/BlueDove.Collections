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
            public float Calc(float l, float r)
                => l + r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatAddSse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> l, Vector128<float> r)
                => Sse.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatAddAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> l, Vector256<float> r)
                => Avx.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct FloatSubtract : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float l, float r)
                => l - r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatSubtractSse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> l, Vector128<float> r)
                => Sse.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatSubtractAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> l, Vector256<float> r)
                => Avx.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct FloatMultiply : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float l, float r)
                => l * r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatMultiplySse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> l, Vector128<float> r)
                => Sse.Multiply(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatMultiplyAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> l, Vector256<float> r)
                => Avx.Multiply(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct FloatDivide : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float l, float r)
                => l / r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct FloatDivideSse : IBinomialOperator<Vector128<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<float> Calc(Vector128<float> l, Vector128<float> r)
                => Sse.Divide(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse.IsSupported;
            }
        }

        private struct FloatDivideAvx : IBinomialOperator<Vector256<float>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<float> Calc(Vector256<float> l, Vector256<float> r)
                => Avx.Divide(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct FloatRem : IBinomialOperator<float>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float Calc(float l, float r)
                => l % r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleAdd : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double l, double r)
                => l + r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
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

        private struct DoubleSubtract : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double l, double r)
                => l - r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleSubtractSse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> l, Vector128<double> r)
                => Sse2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleSubtractAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> l, Vector256<double> r)
                => Avx.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleMultiply : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double l, double r)
                => l * r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleMultiplySse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> l, Vector128<double> r)
                => Sse2.Multiply(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleMultiplyAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> l, Vector256<double> r)
                => Avx.Multiply(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleDivide : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double l, double r)
                => l / r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct DoubleDivideSse2 : IBinomialOperator<Vector128<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector128<double> Calc(Vector128<double> l, Vector128<double> r)
                => Sse2.Divide(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Sse2.IsSupported;
            }
        }

        private struct DoubleDivideAvx : IBinomialOperator<Vector256<double>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<double> Calc(Vector256<double> l, Vector256<double> r)
                => Avx.Divide(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx.IsSupported;
            }
        }

        private struct DoubleRem : IBinomialOperator<double>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double Calc(double l, double r)
                => l % r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteAdd : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte l, sbyte r)
                => (sbyte)((int)l + (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteAddAvx2 : IBinomialOperator<Vector256<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<sbyte> Calc(Vector256<sbyte> l, Vector256<sbyte> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct SByteSubtract : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte l, sbyte r)
                => (sbyte)((int)l - (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteSubtractAvx2 : IBinomialOperator<Vector256<sbyte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<sbyte> Calc(Vector256<sbyte> l, Vector256<sbyte> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct SByteMultiply : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte l, sbyte r)
                => (sbyte)((int)l * (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteDivide : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte l, sbyte r)
                => (sbyte)((int)l / (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct SByteRem : IBinomialOperator<sbyte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sbyte Calc(sbyte l, sbyte r)
                => (sbyte)((int)l % (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteAdd : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte l, byte r)
                => (byte)((uint)l + (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteAddAvx2 : IBinomialOperator<Vector256<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<byte> Calc(Vector256<byte> l, Vector256<byte> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ByteSubtract : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte l, byte r)
                => (byte)((uint)l - (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteSubtractAvx2 : IBinomialOperator<Vector256<byte>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<byte> Calc(Vector256<byte> l, Vector256<byte> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ByteMultiply : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte l, byte r)
                => (byte)((uint)l * (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteDivide : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte l, byte r)
                => (byte)((uint)l / (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ByteRem : IBinomialOperator<byte>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte Calc(byte l, byte r)
                => (byte)((uint)l % (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortAdd : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short l, short r)
                => (short)((int)l + (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortAddAvx2 : IBinomialOperator<Vector256<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<short> Calc(Vector256<short> l, Vector256<short> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ShortSubtract : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short l, short r)
                => (short)((int)l - (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortSubtractAvx2 : IBinomialOperator<Vector256<short>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<short> Calc(Vector256<short> l, Vector256<short> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ShortMultiply : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short l, short r)
                => (short)((int)l * (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortDivide : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short l, short r)
                => (short)((int)l / (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ShortRem : IBinomialOperator<short>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public short Calc(short l, short r)
                => (short)((int)l % (int)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortAdd : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort l, ushort r)
                => (ushort)((uint)l + (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortAddAvx2 : IBinomialOperator<Vector256<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ushort> Calc(Vector256<ushort> l, Vector256<ushort> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UShortSubtract : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort l, ushort r)
                => (ushort)((uint)l - (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortSubtractAvx2 : IBinomialOperator<Vector256<ushort>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ushort> Calc(Vector256<ushort> l, Vector256<ushort> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UShortMultiply : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort l, ushort r)
                => (ushort)((uint)l * (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortDivide : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort l, ushort r)
                => (ushort)((uint)l / (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UShortRem : IBinomialOperator<ushort>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ushort Calc(ushort l, ushort r)
                => (ushort)((uint)l % (uint)r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntAdd : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int l, int r)
                => l + r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntAddAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> l, Vector256<int> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntSubtract : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int l, int r)
                => l - r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntSubtractAvx2 : IBinomialOperator<Vector256<int>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<int> Calc(Vector256<int> l, Vector256<int> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct IntMultiply : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int l, int r)
                => l * r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntDivide : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int l, int r)
                => l / r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct IntRem : IBinomialOperator<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Calc(int l, int r)
                => l % r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntAdd : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint l, uint r)
                => l + r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntAddAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> l, Vector256<uint> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntSubtract : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint l, uint r)
                => l - r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntSubtractAvx2 : IBinomialOperator<Vector256<uint>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<uint> Calc(Vector256<uint> l, Vector256<uint> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct UIntMultiply : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint l, uint r)
                => l * r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntDivide : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint l, uint r)
                => l / r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct UIntRem : IBinomialOperator<uint>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public uint Calc(uint l, uint r)
                => l % r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongAdd : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long l, long r)
                => l + r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongAddAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> l, Vector256<long> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongSubtract : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long l, long r)
                => l - r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongSubtractAvx2 : IBinomialOperator<Vector256<long>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<long> Calc(Vector256<long> l, Vector256<long> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct LongMultiply : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long l, long r)
                => l * r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongDivide : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long l, long r)
                => l / r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct LongRem : IBinomialOperator<long>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Calc(long l, long r)
                => l % r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongAdd : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong l, ulong r)
                => l + r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongAddAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> l, Vector256<ulong> r)
                => Avx2.Add(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongSubtract : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong l, ulong r)
                => l - r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongSubtractAvx2 : IBinomialOperator<Vector256<ulong>>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector256<ulong> Calc(Vector256<ulong> l, Vector256<ulong> r)
                => Avx2.Subtract(l, r);

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => Avx2.IsSupported;
            }
        }

        private struct ULongMultiply : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong l, ulong r)
                => l * r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongDivide : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong l, ulong r)
                => l / r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }

        private struct ULongRem : IBinomialOperator<ulong>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong Calc(ulong l, ulong r)
                => l % r;

            public bool IsSupported
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }
        }
    }
}

