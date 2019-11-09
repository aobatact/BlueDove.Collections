using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlueDove.StringDictionaries
{
    public class Trie<TKey, TValue> where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        private readonly Node _root;

        public Trie() : this(new Node(ReadOnlyMemory<TKey>.Empty, default)) { }

        internal Trie(Node root) => _root = root;

        public Node Add(ReadOnlyMemory<TKey> memory, TValue value)
        {
            var items = memory;
            if (SearchNodes(_root, ref items, out var node, out var length))
            {
                var nLength = node.Item.Length;
                return nLength > length ? node.DivideNode(length, value) : node.Add(0, items.Slice(nLength), value);
            }

            return length < 0 ? node.Add(~length, items, value) : node.DivideAndAdd(items.Slice(length), length, value);
        }

        public bool Contains(ReadOnlyMemory<TKey> values, bool prefixMatch = false)
            => SearchNodes(_root, ref values, out var node, out var length) &&
               (prefixMatch || (node.IsTerminalNode && length == 0));

        public bool TryGetValue(ReadOnlyMemory<TKey> values, out TValue value)
        {
            if (SearchNodes(_root, ref values, out var node, out var length) &&
                ((node.IsTerminalNode && length == 0)))
            {
                value = node.Value;
                return true;
            }

            value = default;
            return false;
            
        }

        #region Enumerable

        #endregion

        public sealed class Node
        {
            internal ReadOnlyMemory<TKey> Item;
            internal Node[] _nodes;
            internal int _nodeCount;
            private TValue _value;

            public TValue Value
            {
                get => _value;
                set
                {
                    _value = value;
                    IsTerminalNode = true;
                }
            }

            public bool IsTerminalNode { get; internal set; }

            public ReadOnlyMemory<Node> Nodes => _nodes.AsMemory(0, _nodeCount);
            public ReadOnlySpan<Node> NodesSpan => _nodes.AsSpan(0, _nodeCount);

            internal Node(ReadOnlyMemory<TKey> item, TValue value)
            {
                Item = item;
                IsTerminalNode = true;
                _value = value;
            }

            internal Node(ReadOnlyMemory<TKey> item, Node[] nodes, int nodeCount, TValue value,
                bool isTerminalNode = false)
            {
                Item = item;
                _nodes = nodes;
                _nodeCount = nodeCount;
                IsTerminalNode = isTerminalNode;
                _value = value;
            }

            internal Node DivideNode(int length, TValue value, bool isTerminalNode = true)
            {
                if (length == 0)
                {
                    IsTerminalNode = isTerminalNode;
                    return this;
                }

                var nArray = new Node[2];
                var nChild = new Node(Item.Slice(length), _nodes, _nodeCount, Value, IsTerminalNode);
                nArray[0] = nChild;
                Item = Item.Slice(0, length);
                _nodes = nArray;
                _nodeCount = 1;
                Value = value;
                IsTerminalNode = isTerminalNode;
                return nChild;
            }

            internal Node DivideAndAdd(ReadOnlyMemory<TKey> items, int length, TValue value)
            {
                DivideNode(length, default, false);
                var res = _nodes[1] = new Node(items, value);
                _nodeCount = 2;
                return res;
            }

            internal Node Add(int pos, ReadOnlyMemory<TKey> items, TValue value)
            {
                Node[] newNodes;
                if (_nodeCount == 0)
                {
                    newNodes = new Node[1];
                }
                else
                {
                    if (_nodeCount == _nodes.Length)
                    {
                        newNodes = new Node[_nodeCount << 1];
                        Array.Copy(_nodes, newNodes, pos);
                    }
                    else
                    {
                        newNodes = _nodes;
                    }

                    Array.Copy(_nodes, pos, newNodes, pos + 1, _nodeCount - pos);
                }

                _nodes = newNodes;
                _nodeCount++;
                return newNodes[pos] = new Node(items, value);
            }
        }

        private struct NodeComparable : IComparable<Node>
        {
            private readonly ReadOnlyMemory<TKey> _value;

            public NodeComparable(ReadOnlyMemory<TKey> value)
            {
                _value = value;
            }

            public int CompareTo(Node other)
            {
                if (_value.Length >= other.Item.Length)
                    return _value.Span.Slice(0, other.Item.Length).SequenceCompareTo(other.Item.Span);
                return _value.Span.SequenceCompareTo(other.Item.Span);
                //return _value.Span.SequenceCompareTo(other.Item.Span.Slice(0, _value.Length));
            }

            //_value.Span.SequenceCompareTo(other.Item.Span.Slice(0, _value.Span.Length));
        }

        private static bool TryMatch(ReadOnlySpan<TKey> values, ReadOnlySpan<TKey> item, out int length)
        {
            var shorter = values.Length < item.Length;
            length = shorter ? values.Length : item.Length;
            for (var i = 0; i < length; i++)
            {
                if (values[i].Equals(item[i])) continue;
                length = i;
                return false;
            }

            return shorter;
        }

        private static bool SearchNodes(Node root, ref ReadOnlyMemory<TKey> value, out Node node, out int length)
        {
            var nodeComp = new NodeComparable(value);
            while (true)
            {
                var nodes = root.NodesSpan;
                if (nodes.IsEmpty)
                {
                    length = value.Length;
                    node = root;
                    return true;
                }

                var index = nodes.BinarySearch(nodeComp);
                if (index >= 0)
                {
                    root = nodes[index];
                    value = value.Slice(root.Item.Length);
                    if (value.Length == 0)
                    {
                        length = 0;
                        node = root;
                        return true;
                    }

                    nodeComp = new NodeComparable(value);
                }
                else
                {
                    var mIndex = ~index;
                    var readOnlySpan = value.Span;
                    int lengthA;
                    Node nodeA;
                    bool aRes;
                    if (mIndex == nodes.Length)
                    {
                        nodeA = null;
                        lengthA = 0;
                        aRes = false;
                    }
                    else
                    {
                        nodeA = nodes[mIndex];
                        aRes = TryMatch(readOnlySpan, nodeA.Item.Span, out lengthA);
                    }

                    if (mIndex > 0)
                    {
                        var nodeB = nodes[mIndex - 1];
                        var bRes = TryMatch(readOnlySpan, nodeB.Item.Span, out var lengthB);
                        if (lengthB > lengthA)
                        {
                            length = lengthB;
                            node = nodeB;
                            return bRes;
                        }
                    }

                    if (lengthA == 0)
                    {
                        length = index;
                        node = root;
                        return false;
                    }

                    length = lengthA;
                    node = nodeA;
                    return aRes;
                }
            }
        }
    }

    public static class Trie
    {
        public static void Add<T>(this Trie<char, T> trie, string str, T value)
            => trie.Add(str.AsMemory(), value);

        public static void Add<T>(this Trie<T, Unit> trie, ReadOnlyMemory<T> item) 
            where T : IEquatable<T>, IComparable<T> => trie.Add(item, default);

        public static void Add(this Trie<char, Unit> trie, string str)
            => trie.Add(str.AsMemory(), default);

        public static bool Contains<T>(this Trie<char, T> trie, string str, bool prefixMatch = false)
            => trie.Contains(str.AsMemory(), prefixMatch);
    }

    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
    {
        public bool Equals(Unit other) => true;

        public int CompareTo(Unit other) => 0;

        public override bool Equals(object obj) => obj is Unit;

        public override int GetHashCode() => 0;
    }
}