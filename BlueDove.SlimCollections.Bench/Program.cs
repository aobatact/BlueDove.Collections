extern alias LF;
extern alias NoLF;

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BlueDove.SlimCollections.Bench
{

    class Program
    {
        static void Main(string[] args)
        {
            //var d = new LF::BlueDove.SlimCollections.DictionarySlim<int, int>();
            //var d2 = new NoLF::BlueDove.SlimCollections.DictionarySlim<int, int>();
            //BenchmarkRunner.Run<IntIntAddBench>();
            BenchmarkRunner.Run<IntIntSearchBench>();
        }
    }

    public sealed class AddBenchBase<TKey,TValue> where TKey:IEquatable<TKey>
    {
        public AddBenchBase()
        {
            lfdic = new LF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue>();
            dic = new NoLF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue>();
        }

        private LF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue> lfdic;
        private NoLF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue> dic;

        public void AddLF(TKey key, TValue value)
        {
            lfdic.GetOrAddValueRef(key) = value;
        }        
        
        public void AddNoLF(TKey key, TValue value)
        {
            dic.GetOrAddValueRef(key) = value;
        }

        public void SearchLF(TKey key)
        {
            lfdic.ContainsKey(key);
        }

        public void SearchNoLF(TKey key)
        {
            dic.ContainsKey(key);
        }
        
        public void Clear()
        {
            lfdic.Clear();
            dic.Clear();
        }
    }

    public class IntIntAddBench
    {
        private AddBenchBase<int, int> bb;
        private int[] x;
        private int[] y;

        [Params(1000000)]
        public int Length;
        
        [GlobalSetup]
        public void SetUp()
        {
            bb = new AddBenchBase<int, int>();
            x = new int[Length];
            y = new int[Length];
            var ran = new Random();
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = ran.Next();
                y[i] = ran.Next();
            }
        }

        [Benchmark]
        public void LF()
        {
            for (var i = 0; i < x.Length; i++)
            {
                bb.AddLF(x[i], y[i]);
            }
        }        
        
        [Benchmark]
        public void NoLF()
        {
            for (var i = 0; i < x.Length; i++)
            {
                bb.AddNoLF(x[i], y[i]);
            }
        }

        [IterationCleanup]
        public void CleanUp()
        {
            bb.Clear();
        }
    }

    [ShortRunJob]
    public class IntIntSearchBench
    {
        private AddBenchBase<int, int> bb;
        private int[] x;
        private int[] y;
        private Random ran;
        [ParamsSource(nameof(Source))]
        public int Length;

        public IEnumerable<int> Source()
        {
            for (int i = 3; i < 20; i++)
            {
                yield return (int) ((1 << i) * 0.75);
                yield return (int) ((1 << i) * 0.875);
                yield return 1 << i;
            }
        }
        
        [GlobalSetup]
        public void SetUp()
        {
            bb = new AddBenchBase<int, int>();
            x = new int[Length];
            y = new int[Length];
            ran = new Random();
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = ran.Next();
                y[i] = ran.Next();
            }

            for (int i = 0; i < x.Length; i++)
            {
                bb.AddLF(x[i],y[i]);
                bb.AddNoLF(x[i],y[i]);
            }
        }

        [Benchmark]
        public void LF()
        {
            bb.SearchLF(ran.Next());
        }        
        [Benchmark]
        public void NoLF()
        {
            bb.SearchNoLF(ran.Next());
        }
        
    }
}