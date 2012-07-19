using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestProject
{
    class ThreadTest
    {
        public ThreadTest()
        {
            
        }

        public void StartThreadSimpleTest()
        {
            Console.WriteLine("work...");

            ThreadPool.QueueUserWorkItem(AsyncJob, "work from thread...");

            Console.WriteLine("work again...");
        }

        private static void AsyncJob(Object state)
        {
            Console.WriteLine(state.ToString());
        }

        public void GetThreadPoolConfigs()
        {
            int maxc, maxw;
            ThreadPool.GetMaxThreads(out maxw, out maxc);

            Console.WriteLine("MAX\tw:" + maxw.ToString() + " c: " + maxc.ToString());

            int minc, minw;
            ThreadPool.GetMinThreads(out minw, out minc);
            Console.WriteLine("MIN\tw:" + minw.ToString() + " c: " + minc.ToString());

        }
    }
}
