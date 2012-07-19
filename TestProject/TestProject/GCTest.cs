using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestProject
{
    class GCTest
    {
        public GCTest(Boolean bug)
        {
            if (bug)
            {
                TimerCreateWithBug();
            }
            else
            {
                TimerCreate();
            }
        }

        public void TimerCreate()
        {
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            Thread.Sleep(5000);
            t.Dispose();
            Console.WriteLine("Stop");
        }

        public void TimerCreateWithBug()
        {
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            Thread.Sleep(5000);
            //t.Dispose();
            Console.WriteLine("Stop");
        }

        public void TimerCallback(Object obj)
        {
            Console.WriteLine("TimerCallback");
            GC.Collect();
        }
    }
}
