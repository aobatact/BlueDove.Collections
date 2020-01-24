/*
 The MIT License (MIT)

Copyright (c) .NET Foundation and Contributors

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if NETSTANDARD2_0
namespace System
{
    public readonly struct Range
    {
        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public Index Start { get; }
        public Index End { get; }
    }


    public readonly struct Index
    {
        private readonly int _value;

        public Index(int value) { _value = value; }

        public int Value
        {
            get
            {
                if (this._value < 0)
                    return ~this._value;
                return this._value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Index FromStart(int value) { return new Index(value); }

        public static implicit operator Index(int value) { return Index.FromStart(value); }
    }
}
#endif

namespace BlueDove.Searches
{
    public static class BinarySearcher
    {
        public static Range SearchRange<T, TComparable>(this ReadOnlySpan<T> values, TComparable comparable)
            where TComparable : IComparable<T> =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, comparable);

        public static Range SearchRange<T, TComparable>(this Span<T> values, TComparable comparable)
            where TComparable : IComparable<T> =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, comparable);

        public static Range SearchRange<T>(this ReadOnlySpan<ReadOnlyMemory<T>> values, ReadOnlySpan<T> others)
            where T : IComparable<T> =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, others,
                default(ReadOnlyMemorySpanExtractor<T>));

        public static Range SearchRange<T>(this ReadOnlySpan<Memory<T>> values, ReadOnlySpan<T> others)
            where T : IComparable<T> =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, others,
                default(MemorySpanExtractor<T>));

        public static Range SearchRange<T>(this ReadOnlySpan<T[]> values, ReadOnlySpan<T> others)
            where T : IComparable<T> =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, others,
                default(ArraySpanExtractor<T>));

        public static Range SearchRange(this ReadOnlySpan<string> values, ReadOnlySpan<char> others) =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, others,
                default(StringSpanExtractor));

        public static Range SearchRange<T>(this Span<Memory<T>> values, ReadOnlySpan<T> others)
            where T : IComparable<T> =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, others,
                default(MemorySpanExtractor<T>));

        public static Range SearchRange<T>(this Span<T[]> values, ReadOnlySpan<T> others)
            where T : IComparable<T> =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, others,
                default(ArraySpanExtractor<T>));

        public static Range SearchRange(this Span<string> values, ReadOnlySpan<char> others) =>
            BinarySearchHelpers.BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, others,
                default(StringSpanExtractor));

        /// <summary>
        /// Gets the lower bound of the <see cref="comparable"/> in <see cref="values"/> (Included)
        /// </summary>
        /// <returns>Lower Bound (Included)</returns>
        public static int LowerBound<T, TComparable>(this ReadOnlySpan<T> values, TComparable comparable)
            where TComparable : IComparable<T>
            => BinarySearchHelpers.LowerBound(ref MemoryMarshal.GetReference(values), values.Length, comparable);

        /// <summary>
        /// Gets the lower bound of the <see cref="comparable"/> in <see cref="values"/> (Included)
        /// </summary>
        /// <returns>Lower Bound (Included)</returns>
        public static int LowerBound<T, TComparable>(this Span<T> values, TComparable comparable)
            where TComparable : IComparable<T>
            => BinarySearchHelpers.LowerBound(ref MemoryMarshal.GetReference(values), values.Length, comparable);

        /// <summary>
        /// Gets the upper bound of the <see cref="comparable"/> in <see cref="values"/> (Excluded)
        /// </summary>
        /// <returns>Upper Bound (Excluded)</returns>
        public static int UpperBound<T, TComparable>(this ReadOnlySpan<T> values, TComparable comparable)
            where TComparable : IComparable<T>
            => BinarySearchHelpers.UpperBound(ref MemoryMarshal.GetReference(values), values.Length, comparable);

        /// <summary>
        /// Gets the upper bound of the <see cref="comparable"/> in <see cref="values"/> (Excluded)
        /// </summary>
        /// <returns>Upper Bound (Excluded)</returns>
        public static int UpperBound<T, TComparable>(this Span<T> values, TComparable comparable)
            where TComparable : IComparable<T>
            => BinarySearchHelpers.UpperBound(ref MemoryMarshal.GetReference(values), values.Length, comparable);
    }

    public static class BinarySearchHelpers
    {
        //Copied from corefx
        public static int BinarySearch<T, TComparable>(
            ref T spanStart, int length, TComparable comparable)
            where TComparable : IComparable<T>
        {
            int lo = 0;
            int hi = length - 1;
            // If length == 0, hi == -1, and loop will not be entered
            while (lo <= hi)
            {
                // PERF: `lo` or `hi` will never be negative inside the loop,
                //       so computing median using uints is safe since we know 
                //       `length <= int.MaxValue`, and indices are >= 0
                //       and thus cannot overflow an uint. 
                //       Saves one subtraction per loop compared to 
                //       `int i = lo + ((hi - lo) >> 1);`
                int i = (int) (((uint) hi + (uint) lo) >> 1);

                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c == 0)
                {
                    return i;
                }
                else if (c > 0)
                {
                    lo = i + 1;
                }
                else
                {
                    hi = i - 1;
                }
            }

            // If none found, then a negative number that is the bitwise complement
            // of the index of the next element that is larger than or, if there is
            // no larger element, the bitwise complement of `length`, which
            // is `lo` at this point.
            return ~lo;
        }

        public static int BinarySearch<T, TComparable>(
            ref T spanStart, int length, TComparable comparable, out int lo, out int hi)
            where TComparable : IComparable<T>
        {
            lo = 0;
            hi = length - 1;
            // If length == 0, hi == -1, and loop will not be entered
            while (lo <= hi)
            {
                // PERF: `lo` or `hi` will never be negative inside the loop,
                //       so computing median using uints is safe since we know 
                //       `length <= int.MaxValue`, and indices are >= 0
                //       and thus cannot overflow an uint. 
                //       Saves one subtraction per loop compared to 
                //       `int i = lo + ((hi - lo) >> 1);`
                int i = (int) (((uint) hi + (uint) lo) >> 1);

                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c == 0)
                {
                    return i;
                }
                else if (c > 0)
                {
                    lo = i + 1;
                }
                else
                {
                    hi = i - 1;
                }
            }

            // If none found, then a negative number that is the bitwise complement
            // of the index of the next element that is larger than or, if there is
            // no larger element, the bitwise complement of `length`, which
            // is `lo` at this point.
            return ~lo;
        }

        public static Range BinarySearchRange<T, TComparable>(
            ref T spanStart, int length, TComparable comparable)
            where TComparable : IComparable<T>
        {
            var firstMatch = BinarySearch(ref spanStart, length, comparable, out var lo, out var hi);
            if (firstMatch < 0)
                return new Range(~firstMatch, ~firstMatch);
            return new Range(LowerBoundInner(ref spanStart, comparable, lo, firstMatch),
                UpperBoundInner(ref spanStart, comparable, firstMatch, hi));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LowerBound<T, TComparable>(ref T spanStart, int length, TComparable comparable)
            where TComparable : IComparable<T>
        {
            var firstSearch = BinarySearch(ref spanStart, length, comparable, out var lo, out var hi);
            return firstSearch < 0 ? firstSearch : LowerBoundInner(ref spanStart, comparable, lo, firstSearch);
        }

        public static int LowerBoundInner<T, TComparable>(ref T spanStart, TComparable comparable, int lo, int hi)
            where TComparable : IComparable<T>
        {
            while (lo < hi)
            {
                int i = (int) (((uint) hi + (uint) lo) >> 1);
                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                // we know that c is nonnegative but should we use <= instead?
                if (c == 0)
                    hi = i;
                else
                    lo = i + 1;
            }

            return hi;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int UpperBound<T, TComparable>(ref T spanStart, int length, TComparable comparable)
            where TComparable : IComparable<T>
        {
            var firstSearch = BinarySearch(ref spanStart, length, comparable, out var lo, out var hi);
            return firstSearch < 0 ? firstSearch : UpperBoundInner(ref spanStart, comparable, firstSearch, hi);
        }

        public static int UpperBoundInner<T, TComparable>(ref T spanStart, TComparable comparable, int lo, int hi)
            where TComparable : IComparable<T>
        {
            while (lo < hi)
            {
                int i = (int) (((uint) hi + (uint) lo + 1u) >> 1);
                // we know that c is not positive but should we use >= instead?
                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c == 0)
                    lo = i;
                else
                    hi = i - 1;
            }

            return lo + 1;
        }

        public static Range BinarySearchRange<T, TCollection, TSpanExtractor>(ref TCollection spanStart, int length,
            ReadOnlySpan<T> values, TSpanExtractor extractor) where T : IComparable<T>
            where TSpanExtractor : ISpanExtractor<T, TCollection>
        {
            int lo = 0;
            int hi = length - 1;
            int lhi;

            #region FirstMatch

            // If length == 0, hi == -1, and loop will not be entered
            while (lo <= hi)
            {
                // PERF: `lo` or `hi` will never be negative inside the loop,
                //       so computing median using uints is safe since we know 
                //       `length <= int.MaxValue`, and indices are >= 0
                //       and thus cannot overflow an uint. 
                //       Saves one subtraction per loop compared to 
                //       `int i = lo + ((hi - lo) >> 1);`
                var i = (int) (((uint) hi + (uint) lo) >> 1);
                var c = values.SequenceCompareTo(extractor.ExtractSpan(Unsafe.Add(ref spanStart, i)));
                if (c > 0)
                {
                    lo = i + 1;
                }
                else if (c == 0)
                {
                    lhi = i;
                    goto found;
                }
                else
                {
                    hi = i - 1;
                }
            }

            return new Range(lo, lo);

            #endregion

            found:

            #region Lower

            var hlo = lhi;
            while (lhi > lo)
            {
                var i = (int) (((uint) lhi + (uint) lo) >> 1);
                var c = values.SequenceCompareTo(extractor.ExtractSpan(Unsafe.Add(ref spanStart, i)));
                if (c == 0) //(c>=0)
                {
                    lhi = i;
                }
                else
                {
                    lo = i + 1;
                }
            }

            #endregion

            #region Upper

            while (hi > hlo)
            {
                var i = (int) (((uint) hi + (uint) hlo + 1) >> 1);
                var c = values.SequenceCompareTo(extractor.ExtractSpan(Unsafe.Add(ref spanStart, i)));
                if (c == 0) //(c<=0)
                    hlo = i;
                else
                    hi = i - 1;
            }

            #endregion

            return new Range(lhi, hlo + 1);
        }
    }

    public interface ISpanExtractor<T, in TCollection>
    {
        ReadOnlySpan<T> ExtractSpan(TCollection collection);
    }

    public readonly struct ArraySpanExtractor<T> : ISpanExtractor<T, T[]>
    {
        public ReadOnlySpan<T> ExtractSpan(T[] collection) => new ReadOnlySpan<T>(collection);
    }

    public readonly struct MemorySpanExtractor<T> : ISpanExtractor<T, Memory<T>>
    {
        public ReadOnlySpan<T> ExtractSpan(Memory<T> collection) => collection.Span;
    }

    public readonly struct ReadOnlyMemorySpanExtractor<T> : ISpanExtractor<T, ReadOnlyMemory<T>>
    {
        public ReadOnlySpan<T> ExtractSpan(ReadOnlyMemory<T> collection) => collection.Span;
    }

    public readonly struct StringSpanExtractor : ISpanExtractor<char, string>
    {
        public ReadOnlySpan<char> ExtractSpan(string collection) => collection.AsSpan();
    }
}
