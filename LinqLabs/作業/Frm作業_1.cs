using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        int skip;
        bool flag = false;
        public Frm作業_1()
        {
            InitializeComponent();                                                  
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            LoadYearToCombobox();                      
        }      

        private void button13_Click(object sender, EventArgs e)
        {
            int rows = Int32.Parse(textBox1.Text);
            skip += Int32.Parse(textBox1.Text);
            if (skip > this.nwDataSet1.Products.Rows.Count)            
                skip = skip - rows;

            this.dataGridView2.DataSource = this.nwDataSet1.Products.Where(x=>!(x.IsCategoryIDNull()||x.IsQuantityPerUnitNull()
                                                 ||x.IsReorderLevelNull()||x.IsSupplierIDNull()||x.IsUnitPriceNull()||x.IsUnitsInStockNull()
                                                 ||x.IsUnitsOnOrderNull())).Skip(skip).Take(rows).ToList();             
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();

            this.dataGridView1.DataSource = files.Where(f => f.Extension == ".log").ToList();
            
            isOrder(flag);
            flag = !flag;
        }

        private void button6_Click(object sender, EventArgs e)
        {            
            this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            isOrder(flag);
            flag = !flag;         
        }

        private void LoadYearToCombobox()
        {
            this.comboBox1.DataSource = this.nwDataSet1.Orders
                .Select(o => o.OrderDate.Year).Distinct().ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();

            this.dataGridView1.DataSource = files.Where(f => f.CreationTime.Year >= 2017).ToList();

            isOrder(flag);
            flag = !flag;
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();

            this.dataGridView1.DataSource = files.Where(f => f.Length >= 1000000).ToList();

            isOrder(flag);
            flag = !flag;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet1.Orders
                .Where(o => o.OrderDate.Year == (int)comboBox1.SelectedValue).ToList();            

            isOrder(flag);
            flag = !flag;            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int rows = Int32.Parse(textBox1.Text);
            skip -= Int32.Parse(textBox1.Text);
            if (skip < 0)            
                skip = 0;

            this.dataGridView2.DataSource = this.nwDataSet1.Products.Where(x => !(x.IsCategoryIDNull() || x.IsQuantityPerUnitNull()
                                                 || x.IsReorderLevelNull() || x.IsSupplierIDNull() || x.IsUnitPriceNull() || x.IsUnitsInStockNull()
                                                 || x.IsUnitsOnOrderNull())).Skip(skip).Take(rows).ToList();             
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderID = (int)this.dataGridView1.CurrentRow.Cells[0].Value;

            this.dataGridView2.DataSource = this.nwDataSet1.Order_Details
                .Where(o => o.OrderID == orderID).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rows = Int32.Parse(textBox1.Text);            
            this.dataGridView2.DataSource = this.nwDataSet1.Products.Take(rows).ToList();
        }

        private void isOrder(bool flag)
        {
            if (flag)
            {
                this.dataGridView1.CellClick -= dataGridView1_CellContentClick;
                dataGridView2.DataSource = null;                                
            }
            else
            {
                this.dataGridView1.CellClick += dataGridView1_CellContentClick;
            }            
        }
    }
}
