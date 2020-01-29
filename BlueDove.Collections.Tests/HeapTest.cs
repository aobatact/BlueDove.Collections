using System;
using System.Collections.Generic;
using BlueDove.Collections.Heaps;
using Xunit;

namespace BlueDove.Collections.Tests
{
    public class HeapTest
    {
        internal static void HeapTryTestHelper<T, THeap>(THeap heap, IEnumerable<T> data, T max) where THeap : IHeap<T>
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
            Assert.Equal(0, heap.Count);
        }

        internal static void HeapTestHelper<T, THeap>(THeap heap, IEnumerable<T> data, T max) where THeap : IHeap<T>
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
                Assert.True(prev.CompareTo(current) <= 0, $" {prev} <= {current} ");
                prev = current;
            }
        }
        
        internal static IEnumerable<object[]> IntTestInputs(int lengthMax = 100, int count = 10)
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
        [InlineData(new[]{0})]
        [InlineData(new[]{1,2,3,5,6,7})]
        [InlineData(new[]{1,2,4,6,8,4,5,3,3,5,7,5,4})]
        [InlineData(new[]{1,3,556,34,6,44,332,4,3456,346,342,5,32,3,3,3,0,0,0})]
        public void ArrayBinaryHeapTest(IEnumerable<int> data)
        {
            HeapTestHelper(new ArrayBinaryHeap<int>(), data, int.MaxValue);
        }

        [Theory]
        [InlineData(new[]{0})]
        [InlineData(new[]{1,2,3,5,6,7})]
        [InlineData(new[]{1,2,4,6,8,4,5,3,3,5,7,5,4})]
        [InlineData(new[]{1,3,556,34,6,44,332,4,3456,346,342,5,32,3,3,3,0,0,0})]        
        public void ArrayBinaryHeapTryTest(IEnumerable<int> data)
        {
            HeapTryTestHelper(new ArrayBinaryHeap<int>(), data, int.MaxValue);
        }
        
        [Theory]
        [InlineData(new[]{0})]
        [InlineData(new[]{1,2,3,5,6,7})]
        [InlineData(new[]{1,2,4,6,8,4,5,3,3,5,7,5,4})]
        [InlineData(new[]{1,3,556,34,6,44,332,4,3456,346,342,5,32,3,3,3,0,0,0})]
        public void RadixHeapTest(IEnumerable<int> data)
        {
            HeapTestHelper(new RadixHeap<int,IntValueConverter>(), data, int.MaxValue);
        }

        [Theory]
        [InlineData(new[]{0})]
        [InlineData(new[]{1,2,3,5,6,7})]
        [InlineData(new[]{1,2,4,6,8,4,5,3,3,5,7,5,4})]
        [InlineData(new[]{1,3,556,34,6,44,332,4,3456,346,342,5,32,3,3,3,0,0,0})]        
        public void RadixHeapTryTest(IEnumerable<int> data)
        {
            HeapTryTestHelper(new RadixHeap<int, IntValueConverter>(), data, int.MaxValue);
        }
        
    }
}
