using System;
using System.Runtime.CompilerServices;

namespace BlueDove.StringDictionaries
{
    public interface ICodeConverter<in T>
    {
        int GetCode(T value);
        int GetOrAddCode(T value);
    }

    internal class DoubleArrayInner<T, TConverter> where TConverter : ICodeConverter<T>
    {
        private int[] @base;
        private int[] check;
        private TConverter _converter;

        public int Search(ReadOnlySpan<T> values)
        {
            var i = 0;
            foreach (var value in values)
            {
                if (!Search(i, value, out var j))
                {
                    break;
                }
                if (j < 0)
                {
                    return j;
                }
                i = j;
            }
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Search(int current, T value, out int next) 
            => Search(current, _converter.GetCode(value), out next);

        public bool Search(int current, int code, out int next)
        {
            next = @base[current] + code;
            if (@base.Length <= next)
            {
                return false;
            }
            return check[next] == current;
        }
    }
}
