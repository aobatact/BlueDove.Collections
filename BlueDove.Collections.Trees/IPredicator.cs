using System;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Trees
{
    public interface IPredicator<in T>
    {
        bool Predicate(T value);
    }
    
    public readonly struct PredicatePredicator<T> : IPredicator<T>
    {
        private readonly Predicate<T> _predicate;

        public PredicatePredicator(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        public bool Predicate(T value)
        {
            return _predicate(value);
        }

        public static implicit operator PredicatePredicator<T>(Predicate<T> predicate) => new PredicatePredicator<T>(predicate);

        public static implicit operator PredicatePredicator<T>(Func<T, bool> predicate) => new PredicatePredicator<T>(Unsafe.As<Predicate<T>>(predicate));
    }

    public readonly struct NilPredicator<T> : IPredicator<T>
    {
        public bool Predicate(T value)
        {
            return true;
        }
    }

    public readonly struct NotNullPredicator<T> : IPredicator<T>
    {
        public bool Predicate(T value)
        {
            return !(value is null);
        }
    }
}