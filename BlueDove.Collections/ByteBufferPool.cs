using System;
using System.Buffers;
using System.Collections.Generic;

namespace BlueDove.Collections
{
    public class ByteBufferPool : MemoryPool<byte>
    {
        private readonly byte[] _buffer;
        private readonly SortedSet<Segment> _segments;
        internal struct Segment : IComparable<Segment>
        {
            public readonly int Offset;
            public readonly int Length;

            public Segment(int offset, int length)
            {
                Offset = offset;
                Length = length;
            }

            public int CompareTo(Segment other) 
                => Offset.CompareTo(other.Offset);
        }
        
        public readonly struct ByteBufferOwner : IMemoryOwner<byte>
        {
            private readonly ByteBufferPool _pool;
            private readonly Segment _segment;

            internal ByteBufferOwner(ByteBufferPool pool, Segment segment)
            {
                _pool = pool;
                _segment = segment;
            }

            public void Dispose()
            {
                _pool?.Release(_segment);
            }

            public Memory<byte> Memory => new Memory<byte>(_pool._buffer, _segment.Offset, _segment.Length);
            public Span<byte> Span => new Span<byte>(_pool._buffer, _segment.Offset, _segment.Length);
            public ArraySegment<byte> ArraySegment =>
                new ArraySegment<byte>(_pool._buffer, _segment.Offset, _segment.Length);
        }

        public ByteBufferPool(int capacity)
        {
            _buffer = new byte[capacity];
            _segments = new SortedSet<Segment>();
        }

        public bool TryRent(int length, out ByteBufferOwner bufferOwner)
        {
            var seg = RentInner(length);
            if (seg.Length == 0)
            {
                bufferOwner = default;
                return false;
            }
            bufferOwner = new ByteBufferOwner(this, seg);
            return true;
        }

        private Segment RentInner(int length)
        {
            var s = 0;
            if (_segments.Count > 0)
            {
                foreach (var segment in _segments)
                {
                    var n = s + length;
                    if (segment.Offset < n)
                    {
                        s = segment.Offset + segment.Length;
                        continue;
                    }
                    break;
                }
            }

            if ((uint) length + (uint) s > _buffer.Length)
                return default;
            var seg = new Segment(s,length);
            _segments.Add(seg);
            return seg;
        }

        private void Release(Segment segment)
        {
            _segments.Remove(segment);
        }

        protected override void Dispose(bool disposing)
        {
        }

        public override IMemoryOwner<byte> Rent(int minBufferSize = -1)
        {
            if (minBufferSize == -1)
            {
                minBufferSize = 4;
            }
            var seg = RentInner(minBufferSize);
            return seg.Length == 0 ? default(IMemoryOwner<byte>) : new ByteBufferOwner(this, seg);
        }

        public override int MaxBufferSize => _buffer.Length;
    }

}