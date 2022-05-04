using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            this.dbContext.Database.Log = Console.Write;
        }

        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {            

            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;

            dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();

            MessageBox.Show(this.dbContext.Products.First().Category.CategoryName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 預存程序 => 方法  

            this.dataGridView1.DataSource = this.dbContext.Sales_by_Year(new DateTime(1997, 1, 1), DateTime.Now);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //var q = from p in this.dbContext.Products.AsEnumerable()           //this.dbContext.Products後面加上AsEnumerable() 就不會出現第60行錯誤
            //        orderby p.UnitsInStock descending, p.ProductID descending  // 若UnitsInStock相同，
            //        select new                                                 // 則依照ProductID排序
            //        {
            //            p.ProductID,
            //            p.ProductName,
            //            p.UnitPrice,
            //            p.UnitsInStock,
            //            TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
            //        };                                                             

            //System.NotSupportedException: 'LINQ to Entities 無法辨識方法 'System.String Format(System.String, System.Object)' 方法，而且這個方法無法轉譯成存放區運算式。'

            var q = this.dbContext.Products.AsEnumerable().OrderByDescending(p => p.UnitsInStock) 
                .ThenByDescending(p => p.ProductID)   // this.dbContext.Products後面加上AsEnumerable() 就不會出現第60行錯誤(T-SQL指令 只到dbContext結束)
                .Select(s => new { s.ProductID, s.ProductName, s.UnitPrice, s.UnitsInStock, TotalPrice = $"{s.UnitPrice * s.UnitsInStock:c2}" });

            dataGridView1.DataSource = q.ToList();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.dataGridView3.DataSource = this.dbContext.Products
                .Select(s => new { s.ProductID, s.ProductName, s.CategoryID, s.Category.CategoryName }).ToList();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //inner join
            var q1 = from p in this.dbContext.Products
                     join c in this.dbContext.Categories
                     on p.CategoryID equals c.CategoryID
                     select new { p.ProductID, p.ProductName, p.CategoryID, p.Category.CategoryName };

            this.dataGridView2.DataSource = q1.ToList();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var q = from c in this.dbContext.Categories
                    from p in c.Products
                    select new { p.ProductID, p.ProductName, c.CategoryID, c.CategoryName };
            
            this.dataGridView1.DataSource = q.ToList();


        }

        private void button11_Click(object sender, EventArgs e)
        {
            //var q = from p in this.dbContext.Products.AsEnumerable()
            //        group p by p.Category.CategoryName into g
            //        select new { CategoryName = g.Key, AvgUnitPrice = $"{g.Average(p => p.UnitPrice):c2}" };

            var q = this.dbContext.Products.AsEnumerable().GroupBy(p => p.Category.CategoryName)
                .Select(g => new { CategoryName = g.Key, AvgUnitPrice = $"{g.Average(p => p.UnitPrice):c2}" });

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Orders.AsEnumerable()
                .GroupBy(p => p.OrderDate.Value.Year) // DateTime?允許空值，需用.Value.Year 才能如願
                .Select(s => new { Year = s.Key, Count = s.Count() }).ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product prod = new Product { ProductName = DateTime.Now.ToLongTimeString(), Discontinued = true };
            this.dbContext.Products.Add(prod);

            this.dbContext.SaveChanges();
        }
    }
}
