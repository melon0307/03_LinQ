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
            Swap1(ref n1, ref n2);
            MessageBox.Show(n1 + ", " + n2);
            //===================================
            string s1 = "aaa";
            string s2 = "bbb";
            MessageBox.Show(s1 + ", " + s2);
            Swap1(ref s1, ref s2);
            MessageBox.Show(s1 + ", " + s2);
        }

        private void Swap1(ref int x, ref int y)
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
            // C# 3.0 匿名方法 lambda 運算式 ()=> goes to
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

            //C#2.0======================================
            //匿名方法
            mydelegate = delegate (int n)
                                  {
                                      return n > 5;
                                  };
            result = mydelegate(8);
            MessageBox.Show("result : " + result);

            //C#3.0======================================
            //匿名方法
            mydelegate = n => n > 5; //3.0可省略型別。設計初衷:為了可當參數使用
            result = mydelegate(4);
            MessageBox.Show("result : " + result);
        }

        List<int> MyWhere(int[] nums, MyDelegate myDelegate)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (myDelegate(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }

        private void btnMyWhere_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> LargeList = MyWhere(nums, Test); //第二個參數原訂為委派，但只要方法符合委派的規則，都可以當參數

            listBox1.Items.Add("LargeList, Test");
            foreach (int n in LargeList)
            {
                listBox1.Items.Add(n);
            }
            listBox1.Items.Add("==========");

            //第2個參數使用3.0 Lambda匿名方法====================================
            List<int> evenList = MyWhere(nums, x => x % 2 == 0);
            listBox1.Items.Add("evenList, x => x % 2 == 0");
            foreach (int n in evenList)
            {
                listBox1.Items.Add(n);
            }

            List<int> oddList = MyWhere(nums, x => x % 2 == 1);
            listBox2.Items.Add("oddList, x => x % 2 == 1");
            foreach (int n in oddList)
            {
                listBox2.Items.Add(n);
            }
        }

        IEnumerable<int> MyIterator(int[] nums, MyDelegate myDelegate)
        {
            foreach (int n in nums)
            {
                if (myDelegate(n))
                {
                    yield return n;
                }
            }
        }

        private void btnYieldReturn_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = MyIterator(nums, n => n > 5);

            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = nums.Where<int>(n => n > 5);

            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }

            //=================================================
            string[] words = { "aaa", "bbbbbbbb", "cccccccc", "dd" };
            IEnumerable<string> q1 = words.Where<string>(w => w.Length > 3);

            foreach (string w in q1)
            {
                listBox2.Items.Add(w);
            }

            //==================================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            var q2 = this.nwDataSet1.Products.Where(x => x.UnitPrice > 30).ToList();
            this.dataGridView1.DataSource = q2;
        }

        private void btnObjectInitializer_Click(object sender, EventArgs e)
        {
            Mypoint p = new Mypoint();
            p.P1 = 100;

            List<Mypoint> list = new List<Mypoint>();
            // 建構子方法() 小括號
            list.Add(new Mypoint());
            list.Add(new Mypoint(100));
            list.Add(new Mypoint(100,100));
            list.Add(new Mypoint("xxx"));

            // 建構子方法，物件初始化{} 大括號 --不用麻煩的建一堆多型在類別內
            list.Add(new Mypoint { P1 = 1, P2 = 2, Field1 = "a", Field2 = "b" });
            list.Add(new Mypoint { P1 = 100, P2 = 200, });
            list.Add(new Mypoint { P1 = 1 });
            this.dataGridView1.DataSource = list;

            //============================================================
            List<Mypoint> list2 = new List<Mypoint>
            {
                new Mypoint { P1 = 1, P2 = 2, Field1 = "a", Field2 = "b" },
                new Mypoint { P1 = 11, P2 = 22, Field1 = "a", Field2 = "b" },
                new Mypoint { P1 = 111, P2 = 222, Field1 = "a", Field2 = "b" },
                new Mypoint { P1 = 1111, P2 = 2222, Field1 = "a", Field2 = "b" },
            };

            this.dataGridView2.DataSource = list2;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var x = new { P1 = 1, P2 = 4 };
            var y = new { P1 = 10, P2 = 40 };
            var z = new { UserName="name", Password = "password" };

            listBox1.Items.Add(x.GetType());
            listBox1.Items.Add(y.GetType());
            listBox1.Items.Add(z.GetType());

            //=================================
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var q = from n in nums
            //        where n > 5
            //        select new { N = n, S = n * n, C = n * n * n };

            dataGridView1.DataSource = nums.Where(n => n > 5).Select(n => new { N = n, S = n * n, C = n * n * n }).ToList();

            //================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var q = from p in this.nwDataSet1.Products
                    where p.UnitPrice > 30
                    select new { 產品編號 = p.ProductID, 產品名稱 = p.ProductName, p.UnitPrice, p.UnitsInStock, 總價 = $"{p.UnitPrice * p.UnitsInStock:c2}" } ;

            dataGridView2.DataSource = q.ToList();
        }


        private void btnExtend_Click(object sender, EventArgs e)
        {
            string s1 = "abcd";
            MessageBox.Show("s1.WordCount: "+s1.WordCount());

            string s2 = "123456789";
            MessageBox.Show("s2.WordCount: " + s2.WordCount());

            //===================================================
            char @char = s2.Chars(3);
            MessageBox.Show("s2.char(3): " + @char);
        }
    }

    public static class MystringExtend //擴充方法 類別與方法必須為靜態
    {
        public static int WordCount(this string s) //參數為 呼叫者
        {
            return s.Length;
        }

        public static char Chars(this string s,int index)
        {
            return s[index];
        }
    }

    public class Mypoint
    {
        public Mypoint()
        {

        }

        public Mypoint(int p1)
        {
            this.P1 = p1;
        }

        public Mypoint(int p1,int p2)
        {
            this.P1 = p1;
            this.P2 = p2;
        }

        public Mypoint(string Field1)
        {

        }
        private int m_p;
        public string Field1, Field2;
        public int P1
        {
            set
            {
                m_p = value;
            }
            get
            {
                return m_p;
            }
        }

        public int P2 { set; get; }
    }
}
