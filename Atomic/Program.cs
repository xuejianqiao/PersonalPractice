using System;
using System.Threading;
using static System.Console;
namespace Atomic
{
    class Program
    {
        private static void Main(string[] args)
        {
            WriteLine("Correct counter");
            var c1 = new CounterNoLock();
            var t1 = new Thread(() => TestCounter(c1));
            var t2 = new Thread(() => TestCounter(c1));
            var t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            WriteLine($"Total count: {c1.Count}");
            ReadLine();
        }
        static void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }
        class CounterNoLock : CounterBase
        {
            private int _count;
            public int Count => _count;
            public override void Increment()
            {
                Interlocked.Increment(ref _count);
            }
            public override void Decrement()
            {
                Interlocked.Decrement(ref _count);
            }
        }
        abstract class CounterBase
        {
            public abstract void Increment();
            public abstract void Decrement();
        }


    }
}
