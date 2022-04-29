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
    }
}