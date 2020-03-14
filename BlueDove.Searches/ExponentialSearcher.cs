using System;
using System.Runtime.CompilerServices;

namespace BlueDove.Searches
{
    public static class ExponentialSearchHelper
    {
        public static int FirstMatch<T, TComparable>(ref T spanStart, int length, TComparable comparable, out int lo,
            out int hi) where TComparable : IComparable<T>
        {
            hi = 1;
            while (true)
            {
                if (hi < length)
                {
                    var c = comparable.CompareTo(Unsafe.Add(ref spanStart, hi));
                    if (c > 0)
                    {
                        hi <<= 1;
                        continue;
                    }

                    if (c < 0) // c != 0
                    {
                        lo = hi >> 1;
                        return BinarySearchHelpers.FirstMatchInner(ref spanStart, comparable, ref lo, ref hi);
                    }
                    // c == 0
                    /*  //getting the closest hi of pow2
                    int f;
                    lo = (f = hi) >> 1;
                    {
                        while (true)
                        {
                            hi <<= 1;
                            if (hi >= length)
                            {
                                hi = length;
                                break;
                            }
                            var c2 = comparable.CompareTo(Unsafe.Add(ref spanStart, hi));
                            if (c2 < 0)
                            {
                                break;
                            }
                        }
                    }

                    return f;
                    */
                    lo = hi >> 1;
                    var f = hi;
                    hi = length;
                    return f;
                }
                else
                {
                    lo = hi >> 1;
                    hi = length;
                    return BinarySearchHelpers.FirstMatchInner(ref spanStart, comparable, ref lo, ref hi);
                }
            }
        }
    }
}
