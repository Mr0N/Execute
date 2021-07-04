using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace Execute
{
    public class ThreadPools : IDisposable
    {
        public void AddTask(Action task)
        {
            actions.Enqueue(task);
            if (maxThread > executeThreads.Count) 
                executeThreads.Add(new ExecuteThread(this.actions));
        }

        public void Dispose()
        {
            foreach (var item in executeThreads)
                item.Stop();
        }

        ConcurrentQueue<Action> actions;
        ConcurrentBag<ExecuteThread> executeThreads;

        int maxThread;
        public ThreadPools(int maxThread =1)
        {
            this.actions = new ConcurrentQueue<Action>();
            this.maxThread = maxThread;
            this.executeThreads = new ConcurrentBag<ExecuteThread>();
        }
    }
}