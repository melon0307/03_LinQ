using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;
            MessageBox.Show(n1 + ", " + n2);
            Swap1(ref n1,ref n2);    
            MessageBox.Show(n1 + ", " + n2);
            //===================================
            string s1 = "aaa";
            string s2 = "bbb";
            MessageBox.Show(s1 + ", " + s2);
            Swap1(ref s1, ref s2);
            MessageBox.Show(s1 + ", " + s2);
        }

        private void Swap1(ref int x,ref int y)
        {
            int i = x;
            x = y;
            y = i;
        }
        //多型
        private void Swap1(ref string x, ref string y)
        {
            string i = x;
            x = y;
            y = i;
        }

        private void Swap1(ref Point x, ref Point y)
        {
            Point i = x;
            x = y;
            y = i;
        }

        private void Swap1(ref object x, ref object y)
        {
            object i = x;
            x = y;
            y = i;
        }

        private static void Swap2<T>(ref T x, ref T y)
        {
            T i = x;
            x = y;
            y = i;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;
            MessageBox.Show(n1 + ", " + n2);
            //Swap2<int>(ref n1, ref n2);
            Swap2(ref n1, ref n2); // 自動推斷型別
            MessageBox.Show(n1 + ", " + n2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;
            MessageBox.Show(n1 + ", " + n2);
            Swap1(ref n1, ref n2);
            MessageBox.Show(n1 + ", " + n2);
            //==============================
            string s1 = "aaa";
            string s2 = "bbb";
            MessageBox.Show(s1 + ", " + s2);
            Swap2(ref s1, ref s2);
            MessageBox.Show(s1 + ", " + s2);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // C# 1.0 具名方法
            buttonX.Click += new EventHandler(aaa);
            buttonX.Click += ButtonX_Click;
            //=================================================
            // C# 2.0 匿名方法
            buttonX.Click += delegate (object sender1, EventArgs e1)
                             {
                                MessageBox.Show("C# 2.0 匿名方法");
                             };
            //=================================================
            // C# 3.0 匿名方法 lambda ()=> goes to
            buttonX.Click += (object sender1, EventArgs e1) =>
                             { 
                                 MessageBox.Show("3.0 匿名方法 lambda"); 
                             };
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX Click");
        }

        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }

        bool Test(int n)
        {
            return n > 5;
        }

        bool Test1(int n)
        {
            return n % 2 == 0;
        }

        //Step 1 : create delegate 型別
        //Step 2 : create delegate object (new..)
        //Step 3 : call 方法()

        delegate bool MyDelegate(int n);
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(4);
            MessageBox.Show("result : " + result);
            //==========================================
            
            MyDelegate mydelegate = new MyDelegate(Test);
            result = mydelegate(7);
            MessageBox.Show("result : " + result);
            //==========================================

            mydelegate = Test;
            result = mydelegate(3);
            MessageBox.Show("result : " + result);
        }
    }
}
