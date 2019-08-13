using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Heaps
{
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

    public class TreeBinomialHeap<T> : IHeap<T>
        where T : IComparable<T>
    {
        private Node root;
        private Stack<Node> cache;
        class Node
        {
            public Node(){}
            public Node(T data, int size, Node parent, Node next, Node child)
            {
                this.data = data;
                Size = size;
                Parent = parent;
                Next = next;
                Child = child;
            }

            public T data { get; set; }
            public int Size { get; set; }
            public Node Parent { get; set; }
            public Node Next { get; set; }
            public Node Child { get; set; }

            public Node ChildLast
            {
                get
                {
                    if (Child == null)
                        return null;
                    var n = Child;
                    while (n.Next != null)
                    {
                        n = n.Next;
                    }

                    return n;
                }
            }

            public void Clear()
            {
                data = default;
                Size = 0;
                Parent = null;
                Next = null;
                Child = null;
            }
        }
        
        Node Publish()
        {
            if (cache.TryPop(out var p))
            {
                return p;
            }
            return new Node();
        }

        Node Merge(Node a, Node b)
        {
            Node c = null;
            Node h = Publish();
            var n = h;
            if (a.Size == b.Size)
            {
                var bm = b.Next;
                (h.Next, a) = SubMerge(a, b);
                b = bm;
            }
            else if(a.Size < b.Size)
            {
                h.Next = a;
                a = a.Next;
            }
            else
            {
                h.Next = b;
                b = b.Next;
            }

            while (true)
            {
                if (c?.Size == a.Size)
                {
                    if (a.Size == b.Size)
                    {
                        n = n.Next = c;
                        c = null;
                        (a, b) = SubMerge(a, b);
                    }
                    else
                    {
                        (n.Next, a) = SubMerge(c, a);
                        n = n.Next;
                        c = null;
                    }
                }

                if (a.Size == b.Size)
                {
                }
                throw new NotImplementedException();
            }
            
            
        }

        static (Node Head, Node Next) SubMerge(Node l, Node r)
        {
            Debug.Assert(l.Size == r.Size);
            Node p, q;
            (p, q) = l.data.CompareTo(r.data) >= 0 ? (r, l) : (l, r);
            p.ChildLast.Next = q;
            q.Parent = p;
            p.Size <<= 1;
            return (p, q.Next);
        }
        
        public void Push(T value)
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public T Pop()
        {
            throw new NotImplementedException();
        }

        public bool TryPeek(out T value)
        {
            throw new NotImplementedException();
        }

        public bool TryPop(out T value)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}