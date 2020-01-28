using System;
using System.Linq;
using BlueDove.StringDictionaries;
using Xunit;

namespace BlueDove.Collections.Tests
{
    public class TrieTest
    {
        [Fact]
        public void ContainsTest()
        {
            var trie = new Trie<char,Unit>();
            trie.Add("abcd");
            trie.Add("xyz");
            trie.Add("abcz");
            Assert.True(trie.Contains("abcd"));
            Assert.True(trie.Contains("abcd", true));
            Assert.True(trie.Contains("abcz"));
            Assert.True(trie.Contains("ab", true));
            Assert.True(trie.Contains("abc", true));
            trie.Add("abc");
            trie.Add("ab");
            Assert.True(trie.Contains("abc"));
            Assert.True(trie.Contains("ab"));
            Assert.False(trie.Contains("abcx"));
            Assert.False(trie.Contains("abczx"));
            Assert.True(trie.Contains("x", true));
            
        }
    }
}