using System;
using System.Collections.Generic;
using BlueDove.Collections.Heaps;
using Xunit;

namespace BlueDove.Collections.Tests
{
    public class HeapTest
    {
        public static void HeapTryTestHelper<T, THeap>(THeap heap, IEnumerable<T> data, T max) where THeap : IHeap<T>
            where T : IComparable<T>
        {
            var min = max;
            var count = 0;
            foreach (var value in data)
            {
                heap.Push(value);
                if (min.CompareTo(value) > 0)
                {
                    min = value;
                }
                Assert.True(heap.TryPeek(out var head));
                Assert.True(head.CompareTo(value) <= 0);
                Assert.Equal(++count,heap.Count);
            }
            Assert.True(heap.TryPop(out var prev));
            Assert.Equal(--count,heap.Count);
            while (heap.TryPop(out var current))
            {
                Assert.Equal(--count, heap.Count);
                Assert.True(prev.CompareTo(current) <= 0);
                prev = current;
            }
        }
        
        public static void HeapTestHelper<T, THeap>(THeap heap, IEnumerable<T> data, T max) where THeap : IHeap<T>
            where T : IComparable<T>
        {
            var min = max;
            var count = 0;
            foreach (var value in data)
            {
                heap.Push(value);
                if (min.CompareTo(value) > 0)
                {
                    min = value;
                }
                var head = heap.Peek();
                Assert.True(head.CompareTo(value) <= 0);
                Assert.Equal(++count,heap.Count);
            }
            var prev = heap.Pop();
            Assert.Equal(--count,heap.Count);
            while (heap.Count > 0)
            {
                var current = heap.Pop();
                Assert.Equal(--count, heap.Count);
                Assert.True(prev.CompareTo(current) <= 0);
                prev = current;
            }
        }
        
        public static IEnumerable<object[]> IntTestInputs(int lengthMax = 100, int count = 10)
        {
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                var length = random.Next(lengthMax);
                var values = new int[length];
                for (int j = 0; j < values.Length; j++)
                {
                    values[j] = random.Next();
                }
                yield return new object[] {values};
            }
        }

        [Theory]
        [InlineData(new int[]{1,3,556,34,6,44,332,4,3456,346,342,5,32,3,3,3,0,0,0})]
        //[MemberData(nameof(IntTestInputs),129,100)]
        public void ArrayBinaryHeapTest<T>(IEnumerable<int> data)
        {
            HeapTestHelper(new ArrayBinaryHeap<int>(), data, int.MaxValue);
        }
        
    }
}
