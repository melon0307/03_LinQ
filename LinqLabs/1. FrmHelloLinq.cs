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
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
        }

        private void IEnumerable_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach(int n in nums)
            {
                listBox1.Items.Add(n);
            }

            listBox1.Items.Add("====================");

            //=========================================================
            // C#內部轉譯

            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }

        private void IEnumerableList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<int> nums = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            foreach(int n in nums)
            {
                listBox1.Items.Add(n);
            }

            listBox1.Items.Add("====================");

            //===========================================================

            List<int>.Enumerator en = nums.GetEnumerator();

            // var自己判斷是什麼型別(萬用型別，但必須有值)
            // var en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            //1. define Data Source
            //2. define query
            //3. execute query

            //1
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //2
            IEnumerable<int> q = from n in nums
                                 where (n <= 3 || n >= 8) && (n%2 == 0)
                                 select n;

            //3
            foreach(int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();


            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = from n in nums
                                 where IsEven(n)
                                 select n;
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private bool IsEven(int n)
        {
            //if (n % 2 == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
             
            // return n % 2 == 0 ? true : false;
            return n % 2 == 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();


            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> q = from n in nums
                                 where n > 5
                                 select new Point(n,n*n);
            foreach (Point n in q)
            {
                listBox1.Items.Add(n.X + ", " + n.Y);
            }

            //===================================

            List<Point> list = q.ToList(); // foreach(item n in q)...list.add(n)...return list
            this.dataGridView1.DataSource = list;

            //===================================

            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] word = { "aaa", "bbb", "Apple", "pineApple", "sugarapple", "ccc" };

            /*var q*/
            IEnumerable<string> q = from w in word
                                    
                                    where w.ToLower().Contains("apple") && w.Length > 5
                                    
                                    select w;

            foreach(string s in q)
            {
                listBox1.Items.Add(s);
            }

            //======================================

            List<string> list = q.ToList();
            this.dataGridView1.DataSource = list;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = this.nwDataSet1.Products;

            IEnumerable<global::LinqLabs.NWDataSet.ProductsRow> q = from p in this.nwDataSet1.Products
                                                                    where !p.IsUnitPriceNull() && p.UnitPrice > 30 && p.UnitPrice < 50 && p.ProductName.ToLower().StartsWith("m")
                                                                    select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IEnumerable<global::LinqLabs.NWDataSet.OrdersRow> q = from o in this.nwDataSet1.Orders
                                                                  where !o.IsOrderDateNull() && o.OrderDate.Year == 1997 && o.OrderDate.Month >=1 && o.OrderDate.Month <=3
                                                                  select o;
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
