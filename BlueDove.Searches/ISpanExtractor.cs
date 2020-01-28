using System;

namespace BlueDove.Searches
{
    public interface ISpanExtractor<T, in TCollection>
    {
        ReadOnlySpan<T> ExtractSpan(TCollection collection);
    }

    public readonly struct ArraySpanExtractor<T> : ISpanExtractor<T, T[]>
    {
        public ReadOnlySpan<T> ExtractSpan(T[] collection) => new ReadOnlySpan<T>(collection);
    }

    public readonly struct MemorySpanExtractor<T> : ISpanExtractor<T, Memory<T>>
    {
        public ReadOnlySpan<T> ExtractSpan(Memory<T> collection) => collection.Span;
    }

    public readonly struct ReadOnlyMemorySpanExtractor<T> : ISpanExtractor<T, ReadOnlyMemory<T>>
    {
        public ReadOnlySpan<T> ExtractSpan(ReadOnlyMemory<T> collection) => collection.Span;
    }

    public readonly struct StringSpanExtractor : ISpanExtractor<char, string>
    {
        public ReadOnlySpan<char> ExtractSpan(string collection) => collection.AsSpan();
    }
}
