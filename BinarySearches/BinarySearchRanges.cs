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

namespace BinarySearches
{
    public static class BinarySearchRanges
    {
        public static Range BinarySearchRange<T, TComparable>(this ReadOnlySpan<T> values, TComparable comparable)
            where TComparable : IComparable<T> =>
            BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, comparable);

        public static Range BinarySearchRange<T, TComparable>(this Span<T> values, TComparable comparable)
            where TComparable : IComparable<T> =>
            BinarySearchRange(ref MemoryMarshal.GetReference(values), values.Length, comparable);

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
                int i = (int)(((uint)hi + (uint)lo) >> 1);
 
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
                int i = (int)(((uint)hi + (uint)lo) >> 1);
 
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
                int i = (int)(((uint)hi + (uint)lo) >> 1);
 
                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c == 0)
                {
                    lhi = i;
                    goto found;
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

            return new Range(lo, lo);
            #endregion
            
            found:

            #region Lower

            var hlo = lhi;
            while (lhi > lo)
            {
                int i = (int) (((uint) lhi + (uint) lo) >> 1);
                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c >= 0)
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
                int i = (int) (((uint) lhi + (uint) lo) >> 1);
                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c <= 0)
                {
                    hlo = i;
                }
                else
                {
                    hi = i - 1;
                }
            }
            
            #endregion

            return new Range(lhi, hlo);
            
        }
        
        public static int LowerBound<T, TComparable>(
            ref T spanStart, int length, TComparable comparable)
            where TComparable : IComparable<T>
        {
            int lo = 0;
            int hi = length - 1;
            // If length == 0, hi == -1, and loop will not be entered
            while (hi - lo > 1)
            {
                int i = (int) (((uint) hi + (uint) lo) >> 1);
                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c >= 0)
                {
                    hi = i;
                }
                else
                {
                    lo = i + 1;
                }
            }
            return hi;
        }
        
        public static int UpperBound<T, TComparable>(
            ref T spanStart, int length, TComparable comparable)
            where TComparable : IComparable<T>
        {
            int lo = 0;
            int hi = length - 1;
            // If length == 0, hi == -1, and loop will not be entered

            while (hi > lo)
            {
                int i = (int) (((uint) hi + (uint) lo) >> 1);
                int c = comparable.CompareTo(Unsafe.Add(ref spanStart, i));
                if (c <= 0)
                {
                    lo = i;
                }
                else
                {
                    hi = i - 1;
                }
            }
            return lo;
        }
    }
}