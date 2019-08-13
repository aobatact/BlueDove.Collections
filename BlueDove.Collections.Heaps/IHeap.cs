namespace BlueDove.Collections.Heaps
{
    public interface IHeap<T>
    {
        void Push(T value);

        virtual T Peek()
        {
            if (TryPeek(out var value))
            {
                return value;
            }
            BufferUtil.ThrowNoItem();
            return default;
        }

        virtual T Pop()
        {
            if (TryPop(out var value))
            {
                return value;
            }
            BufferUtil.ThrowNoItem();
            return default;
        }
        
        bool TryPeek(out T value);
        bool TryPop(out T value);
        int Count { get; }
        void Clear();
    }
}