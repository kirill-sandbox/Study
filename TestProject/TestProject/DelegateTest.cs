using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    class DelegateTest
    {
        internal delegate void Test(String str);

        public DelegateTest()
        {
            Console.WriteLine("TEST 1 !!!");
            this.Calc('+', 10, 5);

            Console.WriteLine("TEST 2 !!!");
            this.Calc('/', 45, 3, MyPrintMethod); // this.Calc('/', 45, 3, new Test(MyPrintMethod));

            Console.WriteLine("TEST 3 !!!");
            Test test = new Test(MyPrintMethod);
            test += MyPrintMethod2; // Test.Combine()
            test -= MyPrintMethod; // Test.Remove()
            this.Calc('*', 99, 7, test);

            Console.WriteLine("TEST 4 !!!");
            this.Calc('-', 23, 3, delegate(String str) {
                Console.Write("{{{ ");
                Console.Write(str);
                Console.Write(" }}}");
            });
        }

        public void MyPrintMethod(String str)
        {
            Console.WriteLine("=============");
            Console.WriteLine(str);
            Console.WriteLine("=============");
        }

        public void MyPrintMethod2(String str)
        {
            Console.WriteLine("+++++++++++++");
            Console.WriteLine(str);
            Console.WriteLine("+++++++++++++");
        }

        public void Calc(char action, int a, int b, Test test = null)
        {
            int result = 0;
            switch (action)
            {
                case '+':
                    result = a + b;
                    break;
                case '-':
                    result = a - b;
                    break;
                case '*':
                    result = a * b;
                    break;
                case '/':
                    result = a / b;
                    break;
                default:
                    throw new Exception("Unknown operation");
            }

            String outStr = a.ToString() + " " + action + " " + b.ToString() + " = " + result.ToString();
            if (test != null)
            {
                test(outStr); // test.Invoke(outStr)
                
            }
            else
            {
                Console.WriteLine(outStr);
            }
        }
    }
}
