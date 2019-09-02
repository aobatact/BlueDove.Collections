using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BlueDove.VectorMath;
using Xunit;

namespace BlueDove.Collections.Tests
{
    public class AlignedTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(31)]
        [InlineData(32)]
        [InlineData(33)]
        [InlineData(64)]
        [InlineData(200)]
        [InlineData(255)]
        [InlineData(256)]
        [InlineData(257)]
        public unsafe void ALInt256(int length)
        {
            var mem = AlignedMemory.AllocAligned256<int>(length);
            ref var reference = ref MemoryMarshal.GetReference(mem.Span);
            var l = AlignedMemory.NotAlignedLength256(ref reference);
            Assert.Equal(0,l);
        }
    }
}