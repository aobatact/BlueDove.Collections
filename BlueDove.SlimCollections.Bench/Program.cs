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
            var d = new LF::BlueDove.SlimCollections.DictionarySlim<int, int>();
            var d2 = new NoLF::BlueDove.SlimCollections.DictionarySlim<int, int>();
            //BenchmarkRunner.Run<IntIntAddBench>();
        }
    }

    public class AddBenchBase<TKey,TValue> where TKey:IEquatable<TKey>
    {
        public AddBenchBase()
        {
            lfdic = new LF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue>();
            dic = new NoLF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue>();
        }
        
        protected LF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue> lfdic;
        protected NoLF::BlueDove.SlimCollections.DictionarySlim<TKey, TValue> dic;

        public void AddLF(TKey key, TValue value)
        {
            lfdic.GetOrAddValueRef(key) = value;
        }        
        
        public void AddNoLF(TKey key, TValue value)
        {
            dic.GetOrAddValueRef(key) = value;
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
    }
    
}