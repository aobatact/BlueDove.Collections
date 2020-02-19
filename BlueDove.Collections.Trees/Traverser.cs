using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BlueDove.Collections.Trees
{
    public static class Traverser
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PreOrder<TTree, T, TChildren, TEnumerator>(this TTree tree)
            where TTree : IMultiBranchTree<T, TChildren>
            where T : IMultiBranchTreeNode<T, TChildren>
            where TChildren : IEnumerable<T, TEnumerator>
            where TEnumerator : IEnumerator<T>
            => PreOrder<TTree, T, TChildren, TEnumerator, NotNullPredicate<T>>(tree, default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PreOrder<TTree, T, TChildren, TEnumerator, TPredicate>(
            this TTree tree, TPredicate predicate)
            where TTree : IMultiBranchTree<T, TChildren>
            where T : IMultiBranchTreeNode<T, TChildren>
            where TPredicate : IPredicate<T>
            where TChildren : IEnumerable<T, TEnumerator>
            where TEnumerator : IEnumerator<T>
            => PreOrder<TTree, T, TChildren, TEnumerator, TPredicate, NotNullPredicate<T>>(tree, predicate,
                default);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PreOrder<TTree, T, TChildren, TEnumerator, TPredicate, TPrunePredicate>(
            this TTree tree, TPredicate predicate, TPrunePredicate prunePredicate)
            where TTree : IMultiBranchTree<T, TChildren>
            where T : IMultiBranchTreeNode<T, TChildren>
            where TPredicate : IPredicate<T>
            where TChildren : IEnumerable<T, TEnumerator>
            where TEnumerator : IEnumerator<T>
            where TPrunePredicate : IPredicate<T>
        {
            return new PreOrderEnumerator<T, TChildren, TEnumerator, TPredicate, TPrunePredicate>(tree.Root, predicate,
                prunePredicate);
            /*
            var target = tree.Root;
            if (predicate.Predicate(target)) yield return target;
            if (target.IsLeaf) yield break;
            var stack = new Stack<IEnumerator<T>>();

            IEnumerator<T> enumerator = null;
            try
            {
                L:
                enumerator = target.Children.GetEnumerator();
                L2:
                if (enumerator.MoveNext())
                {
                    target = enumerator.Current;
                    if (predicate.Predicate(target))
                        yield return target;
                    if (target.IsLeaf)
                        goto L2;
                    stack.Push(enumerator);
                    goto L;
                }

                enumerator.Dispose();

                if (stack.Count > 0)
                {
                    enumerator = stack.Pop();
                    goto L2;
                }
            }
            finally
            {
                enumerator?.Dispose();
                while (stack.Count > 0)
                {
                    stack.Pop().Dispose();
                }
            }
            */
        }

        private class PreOrderEnumerator<T, TChildren, TEnumerator, TPredicate, TPrunePredicate> : IEnumerable<T>, IEnumerator<T>
            where T : IMultiBranchTreeNode<T, TChildren>
            where TChildren : IEnumerable<T, TEnumerator>
            where TEnumerator : IEnumerator<T>
            where TPredicate : IPredicate<T>
            where TPrunePredicate : IPredicate<T>
        {
            private TPredicate _predicate;
            private TPrunePredicate _prunePredicate;
            private TEnumerator[] _stack;
            private int _state;//stackCount
            private TEnumerator _enumerator;
            public T Current { get; private set; }
            private readonly T root;

            public PreOrderEnumerator(T root, TPredicate predicate, TPrunePredicate prunePredicate)
            {
                Current = this.root = root;
                _predicate = predicate;
                _prunePredicate = prunePredicate;
                _stack = new TEnumerator[4];
                _state = -1;
            }

            public bool MoveNext()
            {
                if (_state < 0)
                {
                    if (MoveNextInitStates())
                    {
                        if (_state != 0)
                            return true;
                    }
                    else
                        return false;
                }
                while (true)
                {
                    while (_enumerator.MoveNext())
                    {
                        Current = _enumerator.Current;
                        if (!_prunePredicate.Predicate(Current) && !Current.IsLeaf)
                        {
                            if (_state == _stack.Length)
                            {
                                Array.Resize(ref _stack, _stack.Length << 1);
                            }
                            _stack[_state++] = _enumerator;
                            _enumerator = Current.Children.GetEnumerator();
                        }
                        if (_predicate.Predicate(Current))
                            return true;
                    }
                        
                    _enumerator.Dispose();
                    if (_state != 0)
                        _enumerator = _stack[--_state];
                    else
                        return false;
                }
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private bool MoveNextInitStates()
            {
                string msg;
                switch (_state)
                {
                    case -1:
                        msg = "Not Initialized as Enumerator. Call GetEnumerator() before MoveNext";
                        break;
                    case -2:
                        _state = -3;
                        if (_predicate.Predicate(Current))
                            return true;
                        goto case -3;
                    case -3:
                        if (_prunePredicate.Predicate(Current) || Current.IsLeaf)
                            return false;
                        _enumerator = Current.Children.GetEnumerator();
                        _state = 0;
                        return true;
                    case -4:
                        msg = "PreOrderEnumerator has been Disposed";
                        break;
                    case int.MinValue:
                        msg = "State Stack Overflows";
                        break;
                    default:
                        msg = "Invalid State";
                        break;
                }
                throw new InvalidOperationException(msg);
            }

            void IDisposable.Dispose()
            {
                if (_state > 0)
                    foreach (var enumerator in _stack.AsSpan(0, _state))
                        enumerator.Dispose();
                _stack = null;
                _state = -4;
            }

            void IEnumerator.Reset()
            {
                Current = root;
                _state = -2;
            }

            object IEnumerator.Current => Current;

            public IEnumerator<T> GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref _state, -2, -1) == -1 ||
                    Interlocked.CompareExchange(ref _state, -2, -4) == -4)
                    return this;
                return new PreOrderEnumerator<T, TChildren, TEnumerator, TPredicate, TPrunePredicate>(root,
                    _predicate, _prunePredicate);
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public static IEnumerable<T> PostOrder<T, TChildren, TPredicate>(IMultiBranchTree<T, TChildren> tree,
            TPredicate predicate)
            where T : IMultiBranchTreeNode<T, TChildren>
            where TPredicate : IPredicate<T>
            where TChildren : IEnumerable<T>
        {
            var target = tree.Root;
            if (target.IsLeaf)
            {
                if (predicate.Predicate(target))
                    yield return target;
                yield break;
            }

            var stack = new Stack<(T, IEnumerator<T>)>();
            IEnumerator<T> enumerator = null;
            try
            {
                L:
                enumerator = target.Children.GetEnumerator();
                L2:
                if (enumerator.MoveNext())
                {
                    var next = enumerator.Current;
                    if (next is null) goto L2;
                    if (next.IsLeaf)
                    {
                        if (predicate.Predicate(next))
                            yield return next;
                        goto L2;
                    }

                    stack.Push((target, enumerator));
                    target = next;
                    goto L;
                }

                if (predicate.Predicate(target))
                    yield return target;
                if (stack.Count > 0)
                {
                    (target, enumerator) = stack.Pop();
                    goto L2;
                }
            }
            finally
            {
                enumerator?.Dispose();
                while (stack.Count > 0)
                {
                    stack.Pop().Item2?.Dispose();
                }
            }
        }

        public static IEnumerable<T> BreadthFirst<T, TChildren, TPredicate>(IMultiBranchTree<T, TChildren> tree,
            TPredicate predicate) where T : IMultiBranchTreeNode<T, TChildren>
            where TChildren : IEnumerable<T>
            where TPredicate : IPredicate<T>
        {
            var target = tree.Root;
            yield return target;
            if (target.IsLeaf) yield break;
            var childrenQueue = new Queue<TChildren>();
            childrenQueue.Enqueue(target.Children);

            while (childrenQueue.Count > 0)
            {
                var children = childrenQueue.Dequeue();
                var enumerator = children.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        if (current is null) continue;
                        if (predicate.Predicate(current))
                            yield return current;
                        if (!current.IsLeaf)
                        {
                            childrenQueue.Enqueue(current.Children);
                        }
                    }
                }
                finally
                {
                    enumerator.Dispose();
                }
            }
        }
    }
}