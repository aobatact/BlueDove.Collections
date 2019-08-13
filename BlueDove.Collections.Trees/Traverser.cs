using System;
using System.Collections;
using System.Collections.Generic;

namespace BlueDove.Collections.Trees
{
    public static class Traverser
    {
        public static IEnumerable<T> PreOrder<T, TChildren>(IMultiBranchTree<T, TChildren> tree)
            where T : IMultiBranchTreeNode<T, TChildren> where TChildren : IEnumerable<T>
            => PreOrder<T, TChildren, NilPredicator<T>, NilPredicator<T>>(tree, default, default);

        public static IEnumerable<T> PreOrder<T, TChildren, TPredicator, TPrunePredicator>(
            IMultiBranchTree<T, TChildren> tree,
            TPredicator predicate, TPrunePredicator prunePredicator)
            where T : IMultiBranchTreeNode<T, TChildren>
            where TPredicator : IPredicator<T>
            where TChildren : IEnumerable<T>
            where TPrunePredicator : IPredicator<T>
        {
            return new PreOrderEnumerator<T, TChildren, TPredicator, TPrunePredicator>(tree, predicate,
                prunePredicator);
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

        private class PreOrderEnumerator<T, TChildren, TPredicator, TPrunePredicator> : IEnumerable<T>, IEnumerator<T>
            where T : IMultiBranchTreeNode<T, TChildren>
            where TChildren : IEnumerable<T>
            where TPredicator : IPredicator<T>
            where TPrunePredicator : IPredicator<T>
        {
            private TPredicator _predicator;
            private TPrunePredicator _prunePredicator;
            private IEnumerator<T>[] _stack;
            private int _state;//stackCount
            private IEnumerator<T> _enumerator;
            public T Current { get; private set; }

            public PreOrderEnumerator(IMultiBranchTree<T, TChildren> tree, TPredicator predicator,
                TPrunePredicator prunePredicator)
            {
                Current = tree.Root;
                this._predicator = predicator;
                this._prunePredicator = prunePredicator;
                _stack = new IEnumerator<T>[4];
                _state = -1;
            }

            public bool MoveNext()
            {
                switch (_state)
                {
                    case -1:
                        _state = -2;
                        if (_predicator.Predicate(Current))
                        {
                            return true;
                        }
                        goto case -2;
                    case -2:
                        if (Current.IsLeaf || _prunePredicator.Predicate(Current))
                            return false;
                        _enumerator = Current.Children.GetEnumerator();
                        _state = 0;
                        break;
                }

                while (true)
                {
                    while (_enumerator.MoveNext())
                    {
                        Current = _enumerator.Current;
                        if (!Current.IsLeaf && !_prunePredicator.Predicate(Current))
                        {
                            if (_state == _stack.Length)
                            {
                                Array.Resize(ref _stack, _stack.Length << 1);
                            }
                            _stack[_state++] = _enumerator;
                            _enumerator = Current.Children.GetEnumerator();
                        }
                        if (_predicator.Predicate(Current))
                            return true;
                    }

                    if (_state == 0)
                        return false;
                    _enumerator = _stack[--_state];
                }
            }

            void IDisposable.Dispose()
            {
                if (_state <= 0) return;
                foreach (var enumerator in _stack.AsSpan(0, _state))
                {
                    enumerator.Dispose();
                }
            }
            
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator.Current => Current;
            public IEnumerator<T> GetEnumerator()
            {
                if(_state != -1)
                    throw new InvalidOperationException();
                return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        
        public static IEnumerable<T> PostOrder<T, TChildren, TPredicator>(IMultiBranchTree<T, TChildren> tree,
            TPredicator predicate)
            where T : IMultiBranchTreeNode<T, TChildren>
            where TPredicator : IPredicator<T>
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

        public static IEnumerable<T> BreadthFirst<T, TChildren, TPredicator>(IMultiBranchTree<T, TChildren> tree,
            TPredicator predicate) where T : IMultiBranchTreeNode<T, TChildren>
            where TChildren : IEnumerable<T>
            where TPredicator : IPredicator<T>
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