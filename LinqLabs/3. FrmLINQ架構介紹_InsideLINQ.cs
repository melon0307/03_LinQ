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
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();            
        }

        private void button30_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList arrlist = new System.Collections.ArrayList();
            arrlist.Add(3);
            arrlist.Add(1);
            arrlist.Add(4);

            var q = from n in arrlist.Cast<int>()
                    where n > 2
                    select new { n/*N=n*/ }; //因dataGridView.DataSource 綁屬性，int無屬性。因此需創建個匿名類別中的屬性，接收原來的int(n)，否則會無資料。
                               // ↑ 會自己判斷 則須寫N = n
            dataGridView1.DataSource = q.ToList();

                
        }

        private void btnMix_Click(object sender, EventArgs e)
        {
            // 找product中，庫存最高的5筆。
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            //var q = (from p in this.nwDataSet1.Products
            //orderby p.UnitsInStock descending
            //select p).Take(5).ToList();

            var q = this.nwDataSet1.Products.OrderByDescending(p => p.UnitsInStock).Take(5).ToList();
            dataGridView1.DataSource = q;
        }

        private void btnAggregation_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.listBox1.Items.Add("sum = "+nums.Sum());
            this.listBox1.Items.Add("min = " + nums.Min());
            this.listBox1.Items.Add("max = " + nums.Max());
            this.listBox1.Items.Add("count = " + nums.Count());
            this.listBox1.Items.Add("avg = " + nums.Average());

            this.listBox1.Items.Add("================");

            this.listBox1.Items.Add("evenSum = "+nums.Where(n => n % 2 == 0).Sum());
            this.listBox1.Items.Add("evenMin = " + nums.Where(n => n % 2 == 0).Min());
            this.listBox1.Items.Add("evenMax = " + nums.Where(n => n % 2 == 0).Max());
            this.listBox1.Items.Add("evenCount = " + nums.Where(n => n % 2 == 0).Count());
            this.listBox1.Items.Add("evenAvg = " + nums.Where(n => n % 2 == 0).Average());

            this.listBox1.Items.Add("================");

            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.listBox1.Items.Add($"max UnitPrice = {this.nwDataSet1.Products.Max(p => p.UnitPrice):c2}");
            this.listBox1.Items.Add($"min UnitPrice = {this.nwDataSet1.Products.Min(p => p.UnitPrice):c2}");
            this.listBox1.Items.Add($"Sum UnitPrice = {this.nwDataSet1.Products.Sum(p => p.UnitPrice):c2}");
            this.listBox1.Items.Add($"Avg UnitPrice = {this.nwDataSet1.Products.Average(p => p.UnitPrice):c2}");
        }
    }
}