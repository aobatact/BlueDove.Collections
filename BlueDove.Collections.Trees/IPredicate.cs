using System;
using System.Runtime.CompilerServices;

namespace BlueDove.Collections.Trees
{
    public interface IPredicate<in T>
    {
        bool Predicate(T value);
    }
    
    public readonly struct PredicatePredicate<T> : IPredicate<T>
    {
        private readonly Predicate<T> _predicate;

        public PredicatePredicate(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        public bool Predicate(T value)
        {
            return _predicate(value);
        }

        public static implicit operator PredicatePredicate<T>(Predicate<T> predicate) => new PredicatePredicate<T>(predicate);

        public static implicit operator PredicatePredicate<T>(Func<T, bool> predicate) => new PredicatePredicate<T>(Unsafe.As<Predicate<T>>(predicate));
    }

    public readonly struct NilPredicate<T> : IPredicate<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Predicate(T value)
        {
            return true;
        }
    }

    public readonly struct NotNullPredicate<T> : IPredicate<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Predicate(T value)
        {
            return !(value is null);
        }
    }
}