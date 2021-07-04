using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Execute
{
    class ExecuteThread
    {
        Action action;
        public void Stop()
        {
            check = true;
            thread.Join();
            thread = null;
        }
        bool check;
        Thread thread;
        private void Work()
        {
            thread = new Thread(() =>
            {
                while (true)
                {
                    if (action != null)
                    {
                        action.Invoke();
                        action = null;
                    }
                    if (check) break;
                    lock (block)
                    {
                        if (queueTask.TryDequeue(out var result))
                        {
                            this.action = result;
                        }
                    }
                    Thread.Sleep(40);
                }
            });
            thread.IsBackground = false;
            thread.Start();
        }
        object obj;
        readonly static object block = new object();
        ConcurrentQueue<Action> queueTask;
        public ExecuteThread(ConcurrentQueue<Action> queue)
        {
            this.obj = new object();
            this.queueTask = queue;
            Work();
        }
    }
}
