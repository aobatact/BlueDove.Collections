using System.Collections;
using System.Collections.Generic;

namespace BlueDove.Collections.Trees
{
    public interface IMultiBranchTree<out T, out TChildren> where T : IMultiBranchTreeNode<T, TChildren>
        where TChildren : IEnumerable<T>
    {
        T Root { get; }
    }

    public interface IMultiBranchTreeNode<out T, out TChildren> where T : IMultiBranchTreeNode<T, TChildren>
        where TChildren : IEnumerable<T>
    {
        TChildren Children { get; }
        bool IsLeaf { get; }
    }

    public interface IMultiBranchTreeNode<TValue, out T, out TChildren> : IMultiBranchTreeNode<T, TChildren>
        where T : IMultiBranchTreeNode<TValue, T, TChildren> where TChildren : IEnumerable<T>
    {
        TValue Value { get; set; }
    }

    public interface IEnumerable<out T, out TEnumerator> : IEnumerable<T>
        where TEnumerator : IEnumerator<T>
    {
        new TEnumerator GetEnumerator();
    }

    public struct GeneralEnumerableWrapper<T, TEnumerable> : IEnumerable<T, IEnumerator<T>>
        where TEnumerable : IEnumerable<T>
    {
        private TEnumerable _enumerable;
        IEnumerator<T> IEnumerable<T, IEnumerator<T>>.GetEnumerator() => _enumerable.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _enumerable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _enumerable.GetEnumerator();
    }
}
