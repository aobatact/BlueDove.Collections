using System.Collections.Generic;

namespace BlueDove.Collections.Trees
{
    public interface IMultiBranchTree<out T, out TChildren> where T : IMultiBranchTreeNode<T, TChildren>
        where TChildren : IEnumerable<T>
    {
        T Root { get; }
    }

    public interface IMultiBranchTree<out T> : IMultiBranchTree<T, IEnumerable<T>> where T : IMultiBranchTreeNode<T>
    {
    }
    
    public interface IMultiBranchTreeNode<out T, out TChildren> where T : IMultiBranchTreeNode<T,TChildren> where TChildren : IEnumerable<T>
    {
        TChildren Children { get; }
        bool IsLeaf { get; }
    }

    public interface IMultiBranchTreeNode<out T> : IMultiBranchTreeNode<T, IEnumerable<T>>
        where T : IMultiBranchTreeNode<T>
    {
    }

    public interface IEnumerable<out T, out TEnumerator> : IEnumerable<T>
        where TEnumerator : IEnumerator<T>
    {
        new TEnumerator GetEnumerator();
    }
}