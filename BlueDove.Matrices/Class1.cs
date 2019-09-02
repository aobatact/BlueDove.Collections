using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace BlueDove.Matrices
{
    public interface IMatrix<T>
    {
        /// <summary>
        /// Return the value of matrix
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="column">column</param>
        ref T this[int row,int column] { get; }
        int RowDimension { get; }
        int ColumnDimension { get; }
    }

    public interface IDenseMatrix<T> : IMatrix<T>
    {
        Span<T> Values { get; }
    }

    public readonly struct DoubleDenseMatrix : IDenseMatrix<double>
    {
        public int RowDimension { get; }
        public int ColumnDimension { get; }
        private readonly double[] _values;
        
        public DoubleDenseMatrix(int rowDimension, int columnDimension)
        {
            RowDimension = rowDimension;
            ColumnDimension = columnDimension;
            _values = new double[rowDimension * columnDimension];
        }

        public ref double this[int x, int y] 
            => ref _values[GetIndex(x, y)];

        private int GetIndex(int x, int y) => x * ColumnDimension + y;

        public Span<double> Values => _values;
        public Span<double> GetColumn(int row) => _values.AsSpan(row * ColumnDimension, ColumnDimension);

        public ref double Head => ref _values[0];
    }

    public static class MatrixExtensions
    {
        
    }

    public static class Decompositions
    {
        public static void LUDoolittle(DoubleDenseMatrix ddm)
        {
            if (ddm.ColumnDimension != ddm.RowDimension)
            {
                throw new InvalidOperationException();
            }
            var dimension = ddm.RowDimension;
            ref var head = ref ddm.Head;
            var tSize = dimension * dimension;
            var k = (IntPtr)0;
            var cCache = dimension < 1024 ? stackalloc double[dimension] : new double[dimension];
            ref var cRef = ref MemoryMarshal.GetReference(cCache);
            for (var i = 0; i < dimension - 1; i++, k += dimension + 1)
            {
                ref var rHead = ref Unsafe.Add(ref head, k);
                for (var j = i + 1; j < dimension; j++)
                {
                    ref var cHead = ref Unsafe.Add(ref rHead,dimension);
                    cHead /= rHead;
                    ref var m = ref Unsafe.Add(ref cHead, 1);
                    if (Avx.IsSupported && dimension - j >= Vector256<double>.Count)
                    {
                        var v = Vector256.Create(cHead);
                        ref var cv = ref Unsafe.As<double, Vector256<double>>(ref cRef);
                        var offset = 0;
                        var l = 0;
                        do
                        {
                            Unsafe.Add(ref cv, l++) =
                                Avx.Multiply(Unsafe.As<double, Vector256<double>>(ref Unsafe.Add(ref m, offset)), v);
                            offset += Vector256<double>.Count;
                        } while (offset < (dimension - i));

                        if (offset != dimension - i)
                        {
                            offset -= Vector256<double>.Count;
                            do
                            {
                                throw new NotImplementedException();
                                
                            } while (offset < (dimension - i));
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }
    }
}