using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace TestProject
{
    class ThreadTest
    {
        internal delegate Int64 AcyncMathDeligate(Int64 n);

        struct AsyncFileReadResult
        {
            public int sz;
            public byte[] buf;
            public FileStream fs;
        }

        public Int64 AcyncMath(Int64 n)
        {
            Int64 sum = 0;
            for (Int64 i = 0; i <= n; i++)
            {
                checked // При переполнение будет исключение, а не просто перезапись sum
                {
                    sum += i;
                }
            }
            return sum;
        }

        public void AcyncMathTest() // В принципе аналогично операциям воода-вывода
        {
            AcyncMathDeligate acyncMathDeligate = AcyncMath;
            acyncMathDeligate.BeginInvoke(1000000000, AcyncMathTestResult, acyncMathDeligate); // При значении 100000000000 EndInvoke вернёт исключение переполнения
        }

        private void AcyncMathTestResult(IAsyncResult res)
        {
            AcyncMathDeligate acyncMathDeligate = (AcyncMathDeligate)res.AsyncState;
            try
            {
                Int64 sum = acyncMathDeligate.EndInvoke(res);
                Console.WriteLine(sum.ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void TestAsyncFileRead()
        {
            AsyncFileReadResult afrr = new AsyncFileReadResult();
            afrr.sz = 15;
            afrr.buf = new byte[15];
            afrr.fs = new FileStream(@"..\test.txt", FileMode.Open, FileAccess.Read, FileShare.Read, afrr.sz, FileOptions.Asynchronous);
            afrr.fs.BeginRead(afrr.buf, 0, afrr.sz, TestAsyncFileReadResult, afrr);
        }

        private void TestAsyncFileReadResult(IAsyncResult res)
        {
            AsyncFileReadResult afrr = (AsyncFileReadResult)res.AsyncState;
            Int32 bytesRead = afrr.fs.EndRead(res);
            afrr.fs.Close();
            Console.WriteLine(bytesRead.ToString());

            StringBuilder sb = new StringBuilder();
            foreach (byte b in afrr.buf) // Возможно не стоит так делать... надо уточнить
            {
                sb.Append((char)b);
            }

            Console.WriteLine(sb.ToString());
        }

        public ThreadTest()
        {
            // System.Threading.Timer ставит задачи в Пул потоков
            // System.Windows.Forms.Timer работает на основе SetTimer windows (WM_TIMER)
        }

        public void StartThreadTest()
        {
            Thread thread = new Thread(SecondAsyncJob); // Выделенный поток
            thread.Start("1");
            Thread.Sleep(100);
            Console.WriteLine("2");
            thread.Join(); // Дождаться завершения
            Console.WriteLine("2");

        }

        private static void SecondAsyncJob(Object state)
        {
            Console.WriteLine(state.ToString());
            Thread.Sleep(2000);
            Console.WriteLine(state.ToString());
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
