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
using System.Runtime.CompilerServices;

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