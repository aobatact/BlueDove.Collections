// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Copied from corefxlab

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

#if !NETSTANDARD2_0
namespace BlueDove.SlimCollections
{
    /// <inheritdoc cref="DictionarySlim{TKey,TValue}"/>
    /// <summary>
    /// A lightweight Dictionary with three principal differences compared to <see cref="Dictionary{string, TValue}"/>
    ///
    /// 1) It is possible to do "get or add" in a single lookup using <see cref="GetOrAddValueRef(string)"/>. For
    /// values that are value types, this also saves a copy of the value.
    /// 2) It assumes it is cheap to equate values.
    /// 3) It assumes the keys implement <see cref="IEquatable{string}"/> or else Equals() and they are cheap and sufficient.
    /// </summary>
    /// <remarks>
    /// 1) This avoids having to do separate lookups (<see cref="Dictionary{string, TValue}.TryGetValue(string, out TValue)"/>
    /// followed by <see cref="Dictionary{string, TValue}.Add(string, TValue)"/>.
    /// There is not currently an API exposed to get a value by ref without adding if the key is not present.
    /// 2) This means it can save space by not storing hash codes.
    /// 3) This means it can avoid storing a comparer, and avoid the likely virtual call to a comparer.
    /// </remarks>
    public class StringDictionarySlim<TValue> : DictionarySlim<string, TValue>
    {

        /// <summary>
        /// Construct with default capacity.
        /// </summary>
        public StringDictionarySlim()
        { }

        /// <summary>
        /// Construct with at least the specified capacity for
        /// entries before resizing must occur.
        /// </summary>
        /// <param name="capacity">Requested minimum capacity</param>
        public StringDictionarySlim(int capacity) : base(capacity)
        { }

        public StringDictionarySlim(DictionarySlim<string,TValue> values)
            :base(values) { }

        /// <summary>
        /// Looks for the specified key in the dictionary.
        /// </summary>
        /// <param name="key">Key to look for</param>
        /// <returns>true if the key is present, otherwise false</returns>
        public bool ContainsKey(ReadOnlySpan<char> key)
        {
            //if (key is null) ThrowHelper.ThrowKeyArgumentNullException();
            Entry[] entries = _entries;
            int collisionCount = 0;
            //string.GetHashCode(key);
            for (int i = _buckets[string.GetHashCode(key) & (_buckets.Length-1)] - 1;
                    (uint)i < (uint)entries.Length; i = entries[i].next)
            {
                if (key.SequenceEqual(entries[i].key))
                    return true;
                if (collisionCount == entries.Length)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }

            return false;
        }

        /// <summary>
        /// Gets the value if present for the specified key.
        /// </summary>
        /// <param name="key">Key to look for</param>
        /// <param name="value">Value found returned by ref, otherwise default(TValue)</param>
        /// <returns>true if the key is present, otherwise false</returns>
        public bool TryGetValue(ReadOnlySpan<char> key, out TValue value)
        {
            //if (key is null) ThrowHelper.ThrowKeyArgumentNullException();
            Entry[] entries = _entries;
            int collisionCount = 0;
            for (int i = _buckets[string.GetHashCode(key) & (_buckets.Length - 1)] - 1;
                    (uint)i < (uint)entries.Length; i = entries[i].next)
            {
                if (key.SequenceEqual(entries[i].key))
                {
                    value = entries[i].value;
                    return true;
                }
                if (collisionCount == entries.Length)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Removes the entry if present with the specified key.
        /// </summary>
        /// <param name="key">Key to look for</param>
        /// <returns>true if the key is present, false if it is not</returns>
        public bool Remove(ReadOnlySpan<char> key)
        {
            //if (key is null) ThrowHelper.ThrowKeyArgumentNullException();
            Entry[] entries = _entries;
            int bucketIndex = string.GetHashCode(key) & (_buckets.Length - 1);
            int entryIndex = _buckets[bucketIndex] - 1;

            int lastIndex = -1;
            int collisionCount = 0;
            while (entryIndex != -1)
            {
                Entry candidate = entries[entryIndex];
                if (key.SequenceEqual(candidate.key))
                {
                    if (lastIndex != -1)
                    {   // Fixup preceding element in chain to point to next (if any)
                        entries[lastIndex].next = candidate.next;
                    }
                    else
                    {   // Fixup bucket to new head (if any)
                        _buckets[bucketIndex] = candidate.next + 1;
                    }

                    entries[entryIndex] = default;

                    entries[entryIndex].next = -3 - _freeList; // New head of free list
                    _freeList = entryIndex;

                    _count--;
                    return true;
                }
                lastIndex = entryIndex;
                entryIndex = candidate.next;

                if (collisionCount == entries.Length)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }

            return false;
        }

        // Not safe for concurrent _reads_ (at least, if either of them add)
        // For concurrent reads, prefer TryGetValue(key, out value)
        /// <summary>
        /// Gets the value for the specified key, or, if the key is not present,
        /// adds an entry and returns the value by ref. This makes it possible to
        /// add or update a value in a single look up operation.
        /// </summary>
        /// <param name="key">Key to look for</param>
        /// <returns>Reference to the new or existing value</returns>
        public ref TValue GetOrAddValueRef(ReadOnlySpan<char> key)
        {
            //if (key is null) ThrowHelper.ThrowKeyArgumentNullException();
            Entry[] entries = _entries;
            int collisionCount = 0;
            int bucketIndex = string.GetHashCode(key) & (_buckets.Length - 1);
            for (int i = _buckets[bucketIndex] - 1;
                    (uint)i < (uint)entries.Length; i = entries[i].next)
            {
                if (key.SequenceEqual(entries[i].key))
                    return ref entries[i].value;
                if (collisionCount == entries.Length)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
                }
                collisionCount++;
            }

            return ref AddKey(key.ToString(), bucketIndex);
        }
    }
}
#endif
