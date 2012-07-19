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
    }
}
