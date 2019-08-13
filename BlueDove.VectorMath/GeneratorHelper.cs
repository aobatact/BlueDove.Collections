using System.Diagnostics;
//<#@ assembly name="$(SolutionDir)\BlueDove.BlueDove.VectorMath.CodeGenerator\bin\Release\netcoreapp3.0\BlueDove.BlueDove.VectorMath.CodeGenerator.dll" #>
namespace BlueDove.VectorMath.CodeGenerator
{
    public static class GeneratorHelper
    {
        public enum OperationType
        {
            Nop,
            RW,
            Add,
            Subtract,
            Multiply,
            Divide,
            Rem,
            Sqrt,
            Abs,
        }

        private static string OpStr(OperationType type)
        {
            return type switch
            {
                OperationType.Nop => string.Empty,
                OperationType.RW => string.Empty,
                OperationType.Add => "{0}+{1}",
                OperationType.Subtract => "{0}-{1}",
                OperationType.Multiply => "{0}*{1}",
                OperationType.Divide => "{0}/{1}",
                OperationType.Rem => "{0}%{1}",
                OperationType.Sqrt => string.Empty,
                OperationType.Abs => string.Empty,
                _ => string.Empty,
            };
        }

        public enum ArithmeticTypes : byte
        {
            Float,
            Double,
            SByte,
            Byte,
            Short,
            UShort,
            Int,
            UInt,
            Long,
            ULong,
        }

        private static readonly string[] arithmeticTypeStrings = new[]
            {"float", "double", "sbyte", "byte", "short", "ushort", "int", "uint", "long", "ulong"};

        public enum IntrinsicsTypes
        {
            None,
            Operator,
            Sse,
            Sse2,
            Sse3,
            Ssse3,
            Sse41,
            Sse42,
            Avx,
            Avx2
        }

        private static IntrinsicsTypes GetIntrinsicsTypes(OperationType op, ArithmeticTypes art, int size)
        {
            switch (size)
            {
                case 256:
                    switch (art)
                    {
                        case ArithmeticTypes.Double:
                        case ArithmeticTypes.Float:
                            switch (op)
                            {
                                case OperationType.RW:
                                case OperationType.Add:
                                case OperationType.Subtract:
                                case OperationType.Multiply:
                                case OperationType.Divide:
                                    return IntrinsicsTypes.Avx;
                                default:
                                    return IntrinsicsTypes.None;
                            }
                        case ArithmeticTypes.SByte:
                        case ArithmeticTypes.Byte:
                        case ArithmeticTypes.Short:
                        case ArithmeticTypes.UShort:
                        case ArithmeticTypes.Int:
                        case ArithmeticTypes.UInt:
                        case ArithmeticTypes.Long:
                        case ArithmeticTypes.ULong:
                            switch (op)
                            {
                                case OperationType.RW:
                                case OperationType.Add:
                                case OperationType.Subtract:
                                case OperationType.Multiply:
                                case OperationType.Divide:
                                    return IntrinsicsTypes.Avx2;
                                default:
                                    return IntrinsicsTypes.None;
                            }

                        default:
                            return IntrinsicsTypes.None;
                    }
                case 128:
                    switch (art)
                    {
                        case ArithmeticTypes.Double:
                            switch (op)
                            {
                                case OperationType.RW:
                                case OperationType.Add:
                                case OperationType.Subtract:
                                case OperationType.Multiply:
                                case OperationType.Divide:
                                    return IntrinsicsTypes.Sse2;
                                default:
                                    return IntrinsicsTypes.None;
                            }
                        case ArithmeticTypes.Float:
                            switch (op)
                            {
                                case OperationType.RW:
                                case OperationType.Add:
                                case OperationType.Subtract:
                                case OperationType.Multiply:
                                case OperationType.Divide:
                                    return IntrinsicsTypes.Sse;
                                default:
                                    return IntrinsicsTypes.None;
                            }
                    }

                    break;
                case 0:
                    switch (op)
                    {
                        case OperationType.RW:
                        case OperationType.Add:
                        case OperationType.Subtract:
                        case OperationType.Multiply:
                        case OperationType.Divide:
                            return IntrinsicsTypes.Operator;
                        default:
                            return IntrinsicsTypes.None;
                    }
                default:
                    Debug.Assert(false);
                    goto case 0;
            }

            return IntrinsicsTypes.None;
        }

        public static string CreateBinomialOperator(OperationType op, ArithmeticTypes art, int size)
        {
            const string format = @"
                private struct {0}{1}{2} : IBinomialOperator<{3}>
                {{
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    public {3} Calc({3} l, {3} r)
                        => {4};

                    public bool IsSupported
                    {{
                        [MethodImpl(MethodImplOptions.AggressiveInlining)]
                        get => {2}.IsSupported;
                    }}
                }}";
            if (size != 0)
            {
                var iType = GetIntrinsicsTypes(op, art, size);
                if (iType <= IntrinsicsTypes.None)
                    return string.Empty;
                var typeString = arithmeticTypeStrings[(int)art];
                var vec = $"Vector{size}<{typeString}>";
                var opStr = $"{iType}.{op}(l, r)";
                return string.Format(format, typeString, op, iType, vec, opStr);
            }
            else
            {
                var typeString = arithmeticTypeStrings[(int)art];
                var vec = $"{typeString}";
                var opStr = OpStr(op);
                if (opStr == string.Empty) return default;
                var opStr2 = string.Format(opStr, "l", "r");
                return string.Format(format, typeString, op, string.Empty, vec, opStr2);
            }
        }
    }
}
