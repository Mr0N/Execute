using System;
using System.Threading;

namespace Execute
{
    class Program
    {
        static void Main(string[] args)
        {
            using ThreadPools pool = new ThreadPools(6);
            for (int i = 0; i < 1000; i++)
                pool.AddTask(() => {
                    Console.WriteLine($"{Index} ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                    });
            
            Console.WriteLine("OK");
            Console.ReadKey();
        }
        static int Index { get {
                lock (block) return index++;
            } }
        static int index = 0;
        static object block = new object();
    }
}
