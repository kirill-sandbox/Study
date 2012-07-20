using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject
{
    class ClassTest
    {
        class A
        {
            public virtual int Calc(int a, int b)
            {
                return a + b;
            }
        }

        class B : A
        {
            public override int Calc(int a, int b)
            {
                return base.Calc(a, b) - 10;
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        class C
        {
            private int mA;
            public int a 
            { 
                get 
                {
                    return mA + 79;
                }
                set
                {
                    mA = value - 79;
                } 
            }

            public int this[int a]
            {
                get { return mA; }
                set { mA = a; }
            }

            public int this[String s]
            {
                get
                {
                    if (s.Equals("test"))
                        return 123;
                    else
                        return 321;
                }
            }

            

        }

        class D
        {
            private int mA;
            public int a { get { return mA; } set { mA = value; } }

            static public D operator +(D d1, D d2)
            {
                D res = new D();
                res.mA = d1.mA + d2.mA;
                return res;
            }
        }

        public static void RunTests()
        {
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
        }

        public static void Test5()
        {
            int i = 5;
            Console.WriteLine("---TEST{0}---", i);

            D a = new D(), b = new D();
            a.a = 5;
            b.a = 5;
            D c = a + b;

            Console.WriteLine("END OF TEST{0}", i);
        }

        public static void Test4()
        {
            int i = 4;
            Console.WriteLine("---TEST{0}---", i);

            C c = new C();

            //c[80] = 0;

            int testResult = c["test"];

            Console.WriteLine(testResult.ToString());
            testResult = c["ggg"];
            Console.WriteLine(testResult.ToString());

            Console.WriteLine("END OF TEST{0}", i);
        }

        public static void Test3()
        {
            int i = 3;
            Console.WriteLine("---TEST{0}---", i);

            C c = new C();
            c.a = 10;

            Console.WriteLine(c.a.ToString());

            Console.WriteLine("END OF TEST{0}", i);
        }

        public static void Test2()
        {
            int i = 2;
            Console.WriteLine("---TEST{0}---", i);

            A a = new A();
            B b = new B();
            a = b;
            Console.WriteLine("A(ref B) " + a.Calc(5, 10).ToString());

            Console.WriteLine("END OF TEST{0}", i);
        }

        public static void Test1()
        {
            int i = 1;
            Console.WriteLine("---TEST{0}---", i);

            A a = new A();
            B b = new B();
            Console.WriteLine("A " + a.Calc(5, 10).ToString());
            Console.WriteLine("B " + b.Calc(5, 10).ToString());

            Console.WriteLine("END OF TEST{0}", i);
        }

    }
}
