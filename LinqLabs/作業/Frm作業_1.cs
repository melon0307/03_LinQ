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
        public Frm作業_1()
        {
            InitializeComponent();                                    
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            LoadYearToCombobox();            
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        } 
        

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
            int rows = Int32.Parse(textBox1.Text);
            skip += Int32.Parse(textBox1.Text);
            if (skip > this.nwDataSet1.Products.Rows.Count)            
                skip = skip - rows;            

            var q = this.nwDataSet1.Products.Where(x=>!(x.IsCategoryIDNull()||x.IsQuantityPerUnitNull()
                                                 ||x.IsReorderLevelNull()||x.IsSupplierIDNull()||x.IsUnitPriceNull()||x.IsUnitsInStockNull()
                                                 ||x.IsUnitsOnOrderNull())).Skip(skip).Take(rows).ToList();
            this.dataGridView2.DataSource = q;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files =  dir.GetFiles();

            //var ex  
            IEnumerable<FileInfo> ex = from n in files
                                       where n.Extension == ".log"
                                       select n;

            this.dataGridView1.DataSource = ex.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {            
            this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            
            //this.bindingSource1.DataSource = this.nwDataSet1.Orders;
            //this.dataGridView1.DataSource = this.bindingSource1;
        }

        private void LoadYearToCombobox()
        {
            IEnumerable<int> year = (from o in this.nwDataSet1.Orders
                                     select o.OrderDate.Year).Distinct();
            this.comboBox1.DataSource = year.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            //var createYear
            IEnumerable<FileInfo> createYear = from n in files
                                               where n.CreationTimeUtc.Year >= 2017
                                               select n;

            this.dataGridView1.DataSource = createYear.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            //var length
            IEnumerable<FileInfo> length = from n in files
                                           where n.Length >= 1000000
                                           select n;

            this.dataGridView1.DataSource = length.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            IEnumerable<global::LinqLabs.NWDataSet.OrdersRow> selectedYear = from o in this.nwDataSet1.Orders
                                                                             where o.OrderDate.Year == (int)comboBox1.SelectedValue
                                                                             select o;
            this.dataGridView1.DataSource = selectedYear.ToList();

            //this.bindingSource1.DataSource = selectedYear.ToList();
            //this.dataGridView1.DataSource = this.bindingSource1;
            //int position = this.bindingSource1.Position;
            //this.dataGridView2.DataSource = selectedYear.ToList()[position].GetOrder_DetailsRows();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int rows = Int32.Parse(textBox1.Text);
            skip -= Int32.Parse(textBox1.Text);
            if (skip < 0)            
                skip = 0;            

            var q = this.nwDataSet1.Products.Where(x => !(x.IsCategoryIDNull() || x.IsQuantityPerUnitNull()
                                                 || x.IsReorderLevelNull() || x.IsSupplierIDNull() || x.IsUnitPriceNull() || x.IsUnitsInStockNull()
                                                 || x.IsUnitsOnOrderNull())).Skip(skip).Take(rows).ToList();
            this.dataGridView2.DataSource = q;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderID = (int)this.dataGridView1.CurrentRow.Cells[0].Value;
            IEnumerable<global::LinqLabs.NWDataSet.Order_DetailsRow> odr = from od in this.nwDataSet1.Order_Details
                                                                           where od.OrderID == orderID
                                                                           select od;
            this.dataGridView2.DataSource = odr.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rows = Int32.Parse(textBox1.Text);            
            this.dataGridView2.DataSource = this.nwDataSet1.Products.Take(rows).ToList();
        }
    }
}
