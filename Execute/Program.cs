using System;
using System.Threading;

namespace Execute
{
    class Program
    {
        static void Main(string[] args)
        {
            using ThreadPools pool = new ThreadPools(2);
                pool.AddTask(() => {
                    Thread.Sleep(6000);
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    });
            pool.AddTask(() => {
                Thread.Sleep(4000);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
            pool.AddTask(() => {
                Thread.Sleep(2000);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
            pool.AddTask(() => {
                Thread.Sleep(1000);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
