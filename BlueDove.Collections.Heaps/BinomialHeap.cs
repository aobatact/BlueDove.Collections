using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Heaps
{
#if !NETSTANDARD2_0
    public class ArrayBinomialHeap<T> : IHeap<T>
        where T : IComparable<T>
    {
        private readonly T[][] _values;
        private readonly T[] _singleMerger;
        public ArrayBinomialHeap()
        {
            _values = new T[32][];
            _singleMerger = new T[1];
        }
        
        private static void Merge(T[][] a, T[] b, int mc)
        {
            Debug.Assert((uint) mc <= 32);
            T[] upper = null;
            var l = -1;
            while (++l < mc)
            {
                if (a[l] == null)
                {
                    if (upper == null)
                    {
                        a[l] = GetSlice(b,l).ToArray();
                    }
                    else
                    {
                        upper = SubMerge(upper, GetSlice(b,l));
                    }
                }
                else
                {
                    (a[l], upper) = (upper, SubMerge(a[l], GetSlice(b,l)));
                }
            }
        }

        private static Span<T> GetSlice(T[] values, int k)
        {
            var length = (1 << k);
            return values.AsSpan(length, length);
        }

        private static void Merge(T[][] a, T[][] b, int mc)
        {
            Debug.Assert((uint) mc <= 32);
            T[] upper = null;
            var l = -1;
            while (++l < mc)
            {
                if (a[l] == null)
                    if (upper == null)
                        a[l] = b[l];
                    else
                        upper = SubMerge(upper, b[l]);
                else if (b[l] == null)
                    (a[l], upper) = (null, SubMerge(upper, a[l]));
                else
                    (a[l], upper) = (upper, SubMerge(a[l], b[l]));
            }
        }

        private static T[] SubMerge(Span<T> a, Span<T> b)
        {
            var target = new T[a.Length << 1];
            SubMerge(a, b, target);
            return target;
        }
        
        private static void SubMerge(Span<T> a, Span<T> b, Span<T> target)
        {
            Debug.Assert(a.Length == b.Length);
            Debug.Assert(a.Length << 1 == target.Length);
            Span<T> p, q;
            if (a[0].CompareTo(b[0]) > 0)
            {
                p = b;
                q = a;
            }
            else
            {
                p = a;
                q = b;
            }
            p.CopyTo(target);
            q.CopyTo(target.Slice(p.Length));
        }
        
        public void Push(T value)
        {
            if (_values[0] == null)
            {
                _values[0] = new[] {value};
                Count++;
                return;
            }
            var l = 0;
            var upper = _singleMerger;
            upper[0] = value;
            while (++l < 32)
            {
                if (_values[l] == null)
                    break;
                upper = SubMerge(_values[l], upper);
            }
            _values[l] = upper;
            Count++;
        }

        private (int Index, T minValue) MinSearch()
        {
            var ind = -1;
            while (++ind < _values.Length)
            {
                if (_values[++ind] != null) goto  M;
            }

            return (-1, default);
            M:
            var min = _values[ind][0];
            for (var i = 1; i < _values.Length; i++)
            {
                if (_values[i][0].CompareTo(min) >= 0) continue;
                min = _values[i][0];
                ind = i;
            }

            return (ind,min);
        }

        public T Peek()
        {
            var (index, minValue) = MinSearch();
            if (index == -1) BufferUtil.ThrowNoItem();
            return minValue;
        }

        public T Pop()
        {
            var (index, value) = MinSearch();
            if (index == -1) BufferUtil.ThrowNoItem();
            Merge(_values, _values[index], 32);
            Count--;
            return value;
        }

        public bool TryPeek(out T value)
        {
            int index;
            (index, value) = MinSearch();
            if (index != -1) return true;
            return false;
        }

        public bool TryPop(out T value)
        {
            
            int index;
            (index, value) = MinSearch();
            if (index == -1) return false;
            Merge(_values, _values[index], 32);
            Count--;
            return true;
        }

        public int Count { get; private set; }

        public void Clear()
        {
            _values.AsSpan().Fill(null);
        }
    }

#nullable enable
    public class TreeBinomialHeap<T> : IHeap<T>
        where T : IComparable<T>
    {
        private sealed class Node
        {
            public Node(T data, int size = 1, Node? parent = default, Node? next = default, Node? child = default)
            {
                this.Data = data;
                Degree = size;
                Parent = parent;
                Next = next;
                Child = child;
            }

            public Node? Parent { get; set; }
            public Node? Next { get; set; }
            public Node? Child { get; set; }
            public int Degree { get; set; }
            public T Data { get; set; }
            public int Size => 1 << Degree;

            public Node? ChildLast
            {
                get
                {
                    if (Child == null)
                        return null;
                    var n = Child;
                    while (n.Next != null) n = n.Next;

                    return n;
                }
            }
        }
        private Node? root;

        private static Node Union(Node a, Node? b)
        {
            if (b == null)
                return a;
            Node? al = a;
            Node c, h;
            Node? r = null;
            Node? m;
            if (al.Degree < b.Degree)
            {
                h = c = al;
                SwapNext(ref al, ref b);
            }
            else if (al.Degree > b.Degree)
            {
                h = c = b;
                SwapNext(ref b,ref al);
            }
            else
            {
                var tb = b.Next;
                h = c = Merge(al, b);
                al = al.Next;
                b = tb;
            }

            while (true)
            {
                if (al == null)
                {
                    m = b;
                    break;
                }
                if (b == null)
                {
                    m = al;
                    break;
                }
                if (al.Degree == b.Degree)
                {
                    if (r != null) c = c.Next = r;

                    var bn = b.Next;
                    var an = al.Next;
                    r = Merge(al, b);
                    al = an;
                    b = bn;
                }
                else if (al.Degree < b.Degree)
                {
                    ConnectNext(ref al, ref r, ref c);
                }
                else
                {
                    ConnectNext(ref b, ref r, ref c);
                }
            }

            Debug.Assert(m != null);
            h.Next = m;
            return h;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConnectNext(ref Node min, ref Node? r, ref Node h)
        {
#nullable disable
            ref Node mr = ref min;
#nullable enable
            if (r != null)
            {
                if (mr.Degree == r.Degree)
                {
                    var an = mr.Next;
                    r = Merge(mr, r);
                    mr = an;
                    return;
                }
                Debug.Assert(mr.Degree > r.Degree);
                h = h.Next = r;
                r = null;
            }
            var an1 = mr.Next;
            h = h.Next = mr;
            mr = an1;
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapNext(ref Node node, ref Node node1)
        {
            #nullable disable
            var t = node.Next;
            node.Next = node1;
            node1 = node1.Next;
            node = t;
            #nullable enable
        }

        private static Node Merge(Node x, Node y)
        {
            if (x.Data.CompareTo(y.Data) > 0) (x, y) = (y, x);
            Debug.Assert(x.Degree == y.Degree);
            y.Parent = x;
            y.Next = x.Child;
            x.Child = y;
            x.Degree++;
            return x;
        }

        public void Push(T value)
        {
            root = Union(new Node(value), root);
            Count++;
        }

        public T Peek()
        {
            if(root == null) BufferUtil.ThrowNoItem();
            return SearchMin(out _).Data;
        }

        public T Pop()
        {
            if (root == null) BufferUtil.ThrowNoItem();
            var min = SearchMin(out var prev);
            if (prev != null)
            {
                Debug.Assert(min == prev.Next);
                prev.Next = min.Next;
            }
            root = Union(root, min.Child);
            Count--;
            return min.Data;
        }

        private Node SearchMin(out Node? prev)
        {
#nullable disable
            var c = root;
            var m = c;
            prev = null;
            while (c != null)
            {
                var cn = c.Next;
                if (m.Data.CompareTo(cn.Data) > 0)
                {
                    m = cn;
                    prev = c;
                }

                c = cn;
            }
            
            return m;
#nullable enable
        }

        public bool TryPeek([MaybeNullWhen(false)]out T value)
        {
            if (root == null)
            {
#nullable disable
                value = default;
                return false;
#nullable enable
            }

            value = SearchMin(out _).Data;
            return true;
        }

        public bool TryPop([MaybeNullWhen(false)]out T value)
        {            
            if (root == null)
            {
#nullable disable
                value = default;
                return false;
#nullable enable
            }

            value = Pop();
            return true;
        }

        public int Count { get; private set; }
        public void Clear()
        {
            root = null;
            Count = 0;
        }
    }
#endif
}