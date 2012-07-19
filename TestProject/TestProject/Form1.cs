using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {
        GCTest mGCTest;
        ThreadTest mThreadTest = new ThreadTest();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Console.Beep();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mGCTest = new GCTest(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mGCTest = new GCTest(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mGCTest = new GCTest();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mThreadTest.GetThreadPoolConfigs();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mThreadTest.StartThreadSimpleTest();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mThreadTest.StartThreadTest();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int a = 10;

            Console.WriteLine(a.ToString());

            System.Threading.ThreadPool.QueueUserWorkItem(delegate(Object obj) {
                a = 5; // Анонимная функция имеет доступ к локальным переменным метода!
            },null);

            System.Threading.Thread.Sleep(10);

            Console.WriteLine(a.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mThreadTest.TestAsyncFileRead();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mThreadTest.AcyncMathTest();
        }
    }
}
