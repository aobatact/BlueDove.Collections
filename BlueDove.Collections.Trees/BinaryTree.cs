using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Trees
{
    public class BinaryTree<T> : IMultiBranchTree<BinaryTree<T>.BinaryTreeNode, BinaryTree<T>.BinaryTreeNode>
    {
        public sealed class BinaryTreeNode : IMultiBranchTreeNode<T, BinaryTreeNode, BinaryTreeNode>,
            IEnumerable<BinaryTreeNode, BinaryTreeNode.Enumerator>, IEquatable<BinaryTreeNode>
        {
            public BinaryTreeNode() { }
            public BinaryTreeNode(T value) { Value = value; }
            BinaryTreeNode IMultiBranchTreeNode<BinaryTreeNode, BinaryTreeNode>.Children => this;
            public BinaryTreeNode Left { get; set; }
            public BinaryTreeNode Right { get; set; }
            public T Value { get; set; }
            public bool IsLeaf => Left is null && Right is null;
            public Enumerator GetEnumerator() => new Enumerator(this);
            IEnumerator<BinaryTreeNode> IEnumerable<BinaryTreeNode>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public struct Enumerator : IEnumerator<BinaryTreeNode>
            {
                private readonly BinaryTreeNode node;
                private int state;
                public BinaryTreeNode Current
                {
                    [method : MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => state switch
                    {
                        1 => node.Left,
                        2 => node.Right,
                        _ => null,
                    };
                }

                public Enumerator(BinaryTreeNode binaryTreeNode)
                {
                    node = binaryTreeNode;
                    state = 0;
                }

                public bool MoveNext()
                {
                    switch (state)
                    {
                        case 0:
                            if (node.Left is null)
                                goto case 1;
                            state = 1;
                            return true;
                        case 1:
                            state = 2;
                            return !(node.Right is null);
                    }
                    return false;
                }

                void IEnumerator.Reset() { state = 0; }

                object? IEnumerator.Current => Current;

                void IDisposable.Dispose() { }
            }

            public bool Equals(BinaryTreeNode other)
            {
                return ReferenceEquals(this, other);
            }

            public override bool Equals(object obj)
            {
                return ReferenceEquals(this, obj);
            }

            public override int GetHashCode() { throw new NotImplementedException(); }
        }

        public BinaryTreeNode Root { get; }

        public BinaryTree(T value) { Root = new BinaryTreeNode(value); }
    }
}
