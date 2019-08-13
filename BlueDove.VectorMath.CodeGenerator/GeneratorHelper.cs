using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BlueDove.VectorMath.CodeGenerator
{
    public static class Program
    {
        static int Main(string[] args)
        {
            var dir = Directory.GetCurrentDirectory();
            Console.WriteLine(dir);
            var target = dir.Replace(".CodeGenerator\\bin\\Release\\netcoreapp3.0", 
                String.Empty);
            Console.WriteLine(target);

            Generate(target);
            return 0;
        }

        private static void Generate(string directory)
        {
            using var biWriter = new StreamWriter(directory + "\\VectorOperations.BinomialOperators.g.cs");
            biWriter.WriteLine(GeneratorHelper.Header);
            using var opWriter = new StreamWriter(directory + "\\VectorOperations.g.cs"); 
            opWriter.WriteLine(GeneratorHelper.Header);
            Span<int> size = stackalloc int[3] {0, 128, 256};
            var ops = new string[3];
            foreach (ArithmeticTypes aType in Enum.GetValues(typeof(ArithmeticTypes)))
            {
                foreach (OperationType oType in Enum.GetValues(typeof(OperationType)))
                {
                    ops.AsSpan().Fill(string.Empty);
                    foreach (var i in size)
                    {
                        var binomialOperator =
                            GeneratorHelper.CreateBinomialOperator(oType, aType, i, out var typeName);
                        if (string.IsNullOrEmpty(binomialOperator))
                        {
                            if (i == 0) goto NoOp;
                            continue;
                        }
                        biWriter.WriteLine(binomialOperator);
                        ops[i / 128] = typeName;
                    }

                    var n = 0;
                    foreach (var op in ops)
                    {
                        if (string.IsNullOrEmpty(op))
                        {
                            n++;
                        }
                    }

                    if (n > 1) continue;
                    
                    opWriter.WriteLine(GeneratorHelper.CreateOperator(oType, aType, ops));
                    
                    NoOp: ;
                }
            }
            biWriter.WriteLine(GeneratorHelper.Footer);
            opWriter.WriteLine(GeneratorHelper.Footer);
        }
    }
    
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
    public enum ArithmeticTypes
    {
        Float = 0,
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
    
    public static class GeneratorHelper
    {
        private static string OpStr(OperationType type, ArithmeticTypes art)
        {
            switch (art)
            {
                case ArithmeticTypes.Float:
                case ArithmeticTypes.Double:
                case ArithmeticTypes.Int:
                case ArithmeticTypes.UInt:
                case ArithmeticTypes.Long:
                case ArithmeticTypes.ULong:
                    return type switch
                    {
                        OperationType.Nop => string.Empty,
                        OperationType.RW => string.Empty,
                        OperationType.Add => "{0} + {1}",
                        OperationType.Subtract => "{0} - {1}",
                        OperationType.Multiply => "{0} * {1}",
                        OperationType.Divide => "{0} / {1}",
                        OperationType.Rem => "{0} % {1}",
                        OperationType.Sqrt => string.Empty,
                        OperationType.Abs => string.Empty,
                        _ => string.Empty
                    };

                case ArithmeticTypes.SByte:
                case ArithmeticTypes.Short:
                    return type switch
                    {
                        OperationType.Nop => string.Empty,
                        OperationType.RW => string.Empty,
                        OperationType.Add => $"({ArithmeticTypeStrings[(int)art]})((int){{0}} + (int){{1}})",
                        OperationType.Subtract => $"({ArithmeticTypeStrings[(int)art]})((int){{0}} - (int){{1}})",
                        OperationType.Multiply => $"({ArithmeticTypeStrings[(int)art]})((int){{0}} * (int){{1}})",
                        OperationType.Divide => $"({ArithmeticTypeStrings[(int)art]})((int){{0}} / (int){{1}})",
                        OperationType.Rem => $"({ArithmeticTypeStrings[(int)art]})((int){{0}} % (int){{1}})",
                        OperationType.Sqrt => string.Empty,
                        OperationType.Abs => string.Empty,
                        _ => string.Empty
                    };
                    
                case ArithmeticTypes.Byte:
                case ArithmeticTypes.UShort:
                    return type switch
                    {
                        OperationType.Nop => string.Empty,
                        OperationType.RW => string.Empty,
                        OperationType.Add => $"({ArithmeticTypeStrings[(int)art]})((uint){{0}} + (uint){{1}})",
                        OperationType.Subtract => $"({ArithmeticTypeStrings[(int)art]})((uint){{0}} - (uint){{1}})",
                        OperationType.Multiply => $"({ArithmeticTypeStrings[(int)art]})((uint){{0}} * (uint){{1}})",
                        OperationType.Divide => $"({ArithmeticTypeStrings[(int)art]})((uint){{0}} / (uint){{1}})",
                        OperationType.Rem => $"({ArithmeticTypeStrings[(int)art]})((uint){{0}} % (uint){{1}})",
                        OperationType.Sqrt => string.Empty,
                        OperationType.Abs => string.Empty,
                        _ => string.Empty
                    };
                
                default:
                    return string.Empty;
            }
        }
        
        private static readonly string[] ArithmeticTypeStrings = 
            {"float", "double", "sbyte", "byte", "short", "ushort", "int", "uint", "long", "ulong"};
        
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
                            switch (op)
                            {
                                case OperationType.RW:
                                case OperationType.Add:
                                case OperationType.Subtract:
                                    return IntrinsicsTypes.Avx2;
                                default:
                                    return IntrinsicsTypes.None;
                            }
                        case ArithmeticTypes.Int:
                        case ArithmeticTypes.UInt:
                            switch (op)
                            {
                                case OperationType.RW:
                                case OperationType.Add:
                                case OperationType.Subtract:
                                    return IntrinsicsTypes.Avx2;
                                default:
                                    return IntrinsicsTypes.None;
                            }
                        case ArithmeticTypes.Long:
                        case ArithmeticTypes.ULong:
                            switch (op)
                            {
                                case OperationType.RW:
                                case OperationType.Add:
                                case OperationType.Subtract:
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

        public const string Header = @"//Caution : This is a generated file so change in this file might disapeare.
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace BlueDove.VectorMath
{
    public static partial class VectorOperations
    {";

        public const string Footer = @"    }
}
";
        
        public static string CreateBinomialOperator(OperationType op, ArithmeticTypes art, int size, out string typeName)
        {
            typeName = string.Empty;
            if(op == OperationType.RW) return string.Empty;
            if (size != 0)
            {
                var iType = GetIntrinsicsTypes(op, art, size);
                if (iType <= IntrinsicsTypes.None)
                    return string.Empty;
                var typeString = ArithmeticTypeStrings[(int)art];
                var vector = $"Vector{size}<{typeString}>";
                typeName = $"{art}{op}{iType}";
                return $@"
        private struct {typeName} : IBinomialOperator<{vector}>
        {{
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public {vector} Calc({vector} l, {vector} r)
                => {$"{iType}.{op}(l, r)"};

            public bool IsSupported
            {{
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => {iType}.IsSupported;
            }}
        }}";
            }
            else
            {
                var typeString = ArithmeticTypeStrings[(int)art];
                var opStr = OpStr(op, art);
                if (opStr == string.Empty) return default;
                typeName = $"{art}{op}";
                return $@"
        private struct {typeName} : IBinomialOperator<{typeString}>
        {{
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public {typeString} Calc({typeString} l, {typeString} r)
                => {string.Format(opStr, "l", "r")};

            public bool IsSupported
            {{
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => true;
            }}
        }}";
            }
        }

        public static string CreateOperator(OperationType oType, ArithmeticTypes aType, string[] ops)
        {
            var atStr = ArithmeticTypeStrings[(int) aType];
            var op256 = (string.IsNullOrEmpty(ops[2]) ? $"NilOperator<Vector256<{atStr}>>" : ops[2]);
            var op128 = (string.IsNullOrEmpty(ops[1]) ? $"NilOperator<Vector128<{atStr}>>" : ops[1]);
            return $@"
        public static void {oType}(ReadOnlySpan<{atStr}> l, ReadOnlySpan<{atStr}> r, Span<{atStr}> target)
            => OperationBase<{atStr}, {op256}, {op128}, {ops[0]}>(l, r, target);";
        }
    }
}
