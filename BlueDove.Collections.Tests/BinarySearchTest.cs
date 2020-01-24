using System;
using Xunit;
using Xunit.Abstractions;

namespace BlueDove.Searches.Tests
{
    public class BinarySearchTest
    {
        private ITestOutputHelper _helper;

        [Theory]
        [InlineData(new[] {1, 3, 4}, 1, 0, 1)]
        [InlineData(new[] {1, 3, 4}, 2, 1, 1)]
        [InlineData(new[] {1, 3, 4}, 3, 1, 2)]
        [InlineData(new[] {1, 3, 3, 4}, 3, 1, 3)]
        public void RangeTest(int[] data, int target, int start, int end)
        {
            var range = data.AsSpan().SearchRange(target);
            Assert.Equal(start, range.Start.Value);
            Assert.Equal(end, range.End.Value);
        }

        [Theory]
        [InlineData(new[] {1, 3, 4}, 1, 0)]
        [InlineData(new[] {1, 3, 4}, 2, ~1)]
        [InlineData(new[] {1, 3, 4}, 3, 1)]
        [InlineData(new[] {1, 3, 3, 4}, 3, 1)]
        [InlineData(new[] {1, 3, 3, 3, 4}, 3, 1)]
        [InlineData(new[] {1, 3, 3, 3, 3, 4}, 3, 1)]
        [InlineData(new[] {1, 1, 1, 1, 3, 4}, 3, 4)]
        [InlineData(new[] {1, 1, 1, 3, 4}, 4, 4)]
        [InlineData(new[] {1, 1, 1, 1, 3, 4}, 4, 5)]
        public void LowerBoundTest(int[] data, int target, int lower)
        {
            var res = data.AsSpan().LowerBound(target);
            Assert.Equal(lower, res);
        }

        [Theory]
        [InlineData(new[] {1, 3, 4}, 1, 1)]
        [InlineData(new[] {1, 3, 4}, 2, ~1)]
        [InlineData(new[] {1, 3, 4}, 3, 2)]
        [InlineData(new[] {1, 3, 3, 4}, 3, 3)]
        [InlineData(new[] {1, 3, 3, 3, 4}, 3, 4)]
        [InlineData(new[] {1, 3, 3, 3, 3, 4}, 3, 5)]
        [InlineData(new[] {1, 1, 1, 1, 3, 4}, 3, 5)]
        [InlineData(new[] {1, 1, 1, 3, 4}, 4, 5)]
        [InlineData(new[] {1, 1, 1, 1, 3, 4}, 4, 6)]
        public void UpperBoundTest(int[] data, int target, int upper)
        {
            var res = data.AsSpan().UpperBound(target);
            Assert.Equal(upper, res);
        }

        [Theory]
        [InlineData(new[] {"a", "b"}, "a", 0, 1)]
        [InlineData(new[] {"ab", "b"}, "a", 0, 0)]
        [InlineData(new[] {"a", "ab", "b"}, "a", 0, 1)]
        [InlineData(new[] {"a", "a", "ab", "b"}, "a", 0, 2)]
        public void ExtractRangeTest(string[] values, string value, int lo, int hi)
        {
            var res = values.AsSpan().SearchRange("a".AsSpan());
            Assert.Equal(lo,res.Start.Value);
            Assert.Equal(hi,res.End.Value);
        }
    }
}
