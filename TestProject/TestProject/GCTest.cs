using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestProject
{
    class GCTest
    {
        class ObjectWithFinalizer
        {
            public ObjectWithFinalizer()
            {
                Console.WriteLine("Construct");
            }

            ~ObjectWithFinalizer()
            {
                Console.WriteLine("Finalize");
            }
        }

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

        public GCTest()
        {
            new ObjectWithFinalizer();
            Timer t = new Timer(Collect, null, 2000, 2000);
        }

        public void TimerCreate()
        {
            using (Timer t = new Timer(TimerCallback, null, 0, 2000))
            {
                Thread.Sleep(5000);
                Console.WriteLine("Stop");
            }
            //Равносильно:
            //Timer t;
            //try
            //{
            //    t = new Timer(TimerCallback, null, 0, 2000);
            //    Thread.Sleep(5000);
            //    Console.WriteLine("Stop");
            //}
            //finally
            //{
            //    t.Dispose();
            //}
        }

        public void TimerCreateWithBug()
        {
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            Thread.Sleep(5000);
            Console.WriteLine("Stop");
        }

        public void TimerCallback(Object obj)
        {
            Console.WriteLine("TimerCallback");
            GC.Collect();
        }

        public void Collect(Object obj)
        {
            GC.Collect();
        }
    }
}
