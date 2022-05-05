using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        NorthwindEntities dbContext = new NorthwindEntities();
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void btnIntNoLinq_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 2, 4, 6, 8, 10 };

            ClearData();

            foreach (int n in nums)
            {
                string Key = MyKey(n);
                if (this.treeView1.Nodes[Key] == null)
                {
                    this.treeView1.Nodes.Add(Key, Key);
                }
                this.treeView1.Nodes[Key].Nodes.Add(n.ToString());
                this.treeView1.Nodes[Key].Text = $"{Key} ({this.treeView1.Nodes[Key].GetNodeCount(true)})";
            }
        }

        private string MyKey(int n)
        {
            if (n < 5)
                return "Small";
            else if (n < 9)
                return "Medium";
            else
                return "Large";
        }

        private string MySizeKey(long n)
        {
            if (n < 100000)
                return "Small";
            else if (n < 500000)
                return "Medium";
            else
                return "Large";
        }

        private string MyPriceKey(decimal n)
        {
            if (n < 20)
                return "Low";
            else if (n < 50)
                return "Midium";
            else
                return "High";
        }

        private void ClearData()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            this.treeView1.Nodes.Clear();
        }

        private void btnGroupByFileSize_Click(object sender, EventArgs e)
        {            
            var q = new DirectoryInfo(@"c:\windows").GetFiles().GroupBy(f => MySizeKey(f.Length))
                .Select(s => new { FileSize = s.Key, Count = s.Count(), Group = s }).ToList();

            ClearData();
            dataGridView1.DataSource = q;            

            foreach (var group in q)
            {
                string header = $"{group.FileSize} ({group.Count})";
                TreeNode nodes = this.treeView1.Nodes.Add(group.FileSize.ToString(),header);

                foreach (FileInfo item in group.Group)
                {
                    nodes.Nodes.Add(item.ToString());
                }                
            }
        }

        private void btnGroupByCreationYear_Click(object sender, EventArgs e)
        {            
            var q = new DirectoryInfo(@"c:\windows").GetFiles().GroupBy(f => f.CreationTime.Year).OrderBy(o => o.Key)
                .Select(s => new { Year = s.Key, Count = s.Count(), Group = s }).ToList();

            ClearData();
            dataGridView1.DataSource = q;            

            foreach (var group in q)
            {
                string header = $"{group.Year} ({group.Count})";
                TreeNode nodes = this.treeView1.Nodes.Add(group.Year.ToString(),header);

                foreach (FileInfo item in group.Group)
                {
                    nodes.Nodes.Add(item.ToString());
                }                
            }
        }

        private void btnProductPrice_Click(object sender, EventArgs e)
        {
            var q = this.dbContext.Products.AsEnumerable().OrderBy(o=>o.UnitPrice)
                .Where(p => p.UnitPrice != null).GroupBy(p => MyPriceKey(p.UnitPrice.Value))
                .Select(s => new { Key = s.Key, Count = s.Count(), Group = s }).ToList();
            
            ClearData();
            dataGridView1.DataSource = q;            

            foreach(var group in q)
            {
                string header = $"{group.Key} ({group.Count})";
                TreeNode nodes = this.treeView1.Nodes.Add(group.Key.ToString(), header);

                foreach(Product item in group.Group)
                {
                    string product = $"{item.UnitPrice:c2} - {item.ProductName}";
                    nodes.Nodes.Add(item.ToString(), product);
                }
            }
        }

        private void btnOrdersGroupByYear_Click(object sender, EventArgs e)
        {
            var q = this.dbContext.Orders.AsEnumerable().OrderBy(o => o.OrderDate.Value.Year)
                .GroupBy(o => o.OrderDate.Value.Year)
                .Select(s => new { Year = s.Key, Count = s.Count(), Group = s });
            
            ClearData();
            dataGridView1.DataSource = q;
            
            foreach(var group in q)
            {
                string header = $"{group.Year} ({group.Count})";
                TreeNode nodes = this.treeView1.Nodes.Add(group.Year.ToString(), header);

                foreach(Order item in group.Group)
                {
                    string order = $"OrderID: {item.OrderID}";
                    nodes.Nodes.Add(item.ToString(), order);
                }
            }
        }

        private void btnOrdersGroupByYearMonth_Click(object sender, EventArgs e)
        {
            var q = this.dbContext.Orders.AsEnumerable().OrderBy(o => o.OrderDate)
                .GroupBy(o => o.OrderDate.Value.ToString("yyyy-MM"))
                .Select(s => new { Date = s.Key, Count = s.Count(), Group = s });

            ClearData();
            dataGridView1.DataSource = q;

            foreach(var group in q)
            {
                string header = $"{group.Date} ({group.Count})";
                TreeNode nodes = this.treeView1.Nodes.Add(group.Date.ToString(), header);

                foreach(var item in group.Group)
                {
                    string order = $"OrderID: {item.OrderID}";
                    nodes.Nodes.Add(item.ToString(), order);
                }
            }
        }

        private void btnSalesFigures_Click(object sender, EventArgs e)
        {
            decimal SalesFigures =this.dbContext.Order_Details.Sum(od => od.UnitPrice * od.Quantity);

            MessageBox.Show("北風訂單銷售總額為: " + $"{SalesFigures:c2}");
        }

        private void btnBest5Employees_Click(object sender, EventArgs e)
        {
            var q = this.dbContext.Order_Details.AsEnumerable()
                    .Select(s => new{ Name = s.Order.Employee.FirstName + s.Order.Employee.LastName,SalesFigures = s.UnitPrice * s.Quantity})
                    .GroupBy(g => g.Name)
                    .Select(s => new { Name = s.Key, SalesFigures = s.Sum(p => p.SalesFigures) })
                    .OrderByDescending(o => o.SalesFigures)
                    .Select(s => new { Name = s.Name, SalesFigures = $"{s.SalesFigures:c2}" }).Take(5).ToList();

            ClearData();
            this.dataGridView1.DataSource = q;
        }

        private void btnTop5UnitPrice_Click(object sender, EventArgs e)
        {
            ClearData();
            this.dataGridView1.DataSource = this.dbContext.Products.AsEnumerable()
                .OrderByDescending(p => p.UnitPrice)
                .Select(s => new { s.ProductID, s.ProductName, s.Category.CategoryName, s.UnitPrice, s.UnitsInStock })
                .Take(5).ToList();
        }

        private void btnAnyUnitPriceOver300_Click(object sender, EventArgs e)
        {
            bool result = this.dbContext.Products.AsEnumerable().Any(p => p.UnitPrice > 300);
            MessageBox.Show("NW 產品有任何一筆單價大於300 ??  "+ result);
        }
    }
}
