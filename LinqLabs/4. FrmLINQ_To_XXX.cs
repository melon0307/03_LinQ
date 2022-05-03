using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<IGrouping</*Key型別*/string,/*來源內物件的型別*/ int>> q = from n in nums
                                                                         group n by n % 2 == 0 ? "偶數" : "奇數";

            var query = nums.GroupBy(n => n % 2).ToList();

            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = query;

            //======================================================
            // TreeView

            foreach(var group in q)
            {
                TreeNode nodes = this.treeView1.Nodes.Add(group.Key.ToString());

                foreach(int item in group)
                {
                    nodes.Nodes.Add(item.ToString());                    
                }

                nodes.Text = $"{group.Key} ({nodes.Nodes.Count})";
            }

            //======================================================
            // ListView

            foreach (var group in q)
            {
                ListViewGroup lvg = this.listView1.Groups.Add(group.Key.ToString(), group.Key.ToString());

                foreach (int item in group)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var q = from n in nums
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new
                    {
                        Key = g.Key,
                        Count = g.Count(),
                        Max = g.Max(),
                        Min = g.Min(),
                        Avg = g.Average(),
                        Group = g
                    };
            //var query = nums.GroupBy(n=>n%2==0?"偶數":"奇數")
            

            this.dataGridView1.DataSource = q.ToList();

            //======================================================

            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string header = $"{group.Key} ({group.Count})";
                TreeNode nodes = this.treeView1.Nodes.Add(group.Key.ToString(),header);

                foreach (int item in group.Group)
                {
                    nodes.Nodes.Add(item.ToString());
                }

                //nodes.Text = $"{group.Key} ({nodes.Nodes.Count})";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            //var q = from n in nums
            //        group n by MyKey(n) into g
            //        select new
            //        {
            //            Key = g.Key,
            //            Count = g.Count(),
            //            Max = g.Max(),
            //            Min = g.Min(),
            //            Avg = g.Average(),
            //            Group = g
            //        };

            var query = nums.GroupBy(n => MyKey(n))
                .Select(g => new { Key = g.Key, Count = g.Count(), Max = g.Max(), Min = g.Min(), Avg = g.Average(), Group = g });

            this.dataGridView1.DataSource = query.ToList();

            treeView1.Nodes.Clear();
            foreach (var group in query)
            {
                string header = $"{group.Key} ({group.Count})";
                TreeNode nodes = this.treeView1.Nodes.Add(group.Key.ToString(), header);

                foreach (int item in group.Group)
                {
                    nodes.Nodes.Add(item.ToString());
                }
            }

            //================================================
            // Chart

            chart1.DataSource = query.ToList();

            this.chart1.Series[0].XValueMember = "Key";
            this.chart1.Series[0].YValueMembers = "Count";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "Key";
            this.chart1.Series[1].YValueMembers = "Avg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private string MyKey(int n)
        {
            if (n < 5)
                return "Small";
            else if (n < 10)
                return "Medium";
            else
                return "Large"; 
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();              
           
            dataGridView1.DataSource = files.GroupBy(n => n.Extension)
                .Select(s => new { Extension = s.Key, Count = s.Count() }).OrderByDescending(s=>s.Count).ToList();             
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);

            dataGridView1.DataSource = this.nwDataSet1.Orders.GroupBy(n => n.OrderDate.Year)
                .Select(s => new { Year = s.Key, Count = s.Count() }).OrderByDescending(s => s.Count).ToList();

            int Count1997 = this.nwDataSet1.Orders.Where(o => o.OrderDate.Year == 1997).Count();
            MessageBox.Show("Count = " + Count1997);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Let

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            int count = (from f in files
                         let s = f.Extension
                         where s == ".exe"
                         select s).Count();

            MessageBox.Show("count = " + count);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "This is a book. this is a pen. this is an apple.";
            char[] chars = { ' ', '.', '?', ',' };
            string[] words = s.Split(chars,StringSplitOptions.RemoveEmptyEntries);

            this.dataGridView1.DataSource = words.GroupBy(w => w/*.ToUpper()*/).Select(w => new { Word = w.Key, Count = w.Count() }).ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 2, 3, 55, 99, 65, 2 };
            int[] nums2 = { 1, 2, 9, 88, 654, 327, 55 };

            IEnumerable<int> q;
            q = nums1.Intersect(nums2); // 交集
            q = nums1.Distinct(); // 去重複
            q = nums1.Union(nums2); // 聯集

            //==================================================

            bool result;
            result = nums1.Any(n => n > 5); //任何大於5的?
            result = nums1.All(n => n > 5); //全部都大於5?
            result = nums1.Contains(5); //包含數字5?

            //==================================================

            int n1;
            n1 = nums1.First(); // 第一個數
            n1 = nums1.Last(); // 最後一個數
            //n1 = nums1.ElementAt(16);
            n1 = nums1.ElementAtOrDefault(16); // 第16個數，超出索引錯誤則為預設(0)

            //==================================================

            var q1 = Enumerable.Range(1, 1000).Select(n => new { n }).ToList(); // 產生 從1開始 到1000
            dataGridView1.DataSource = q1;
            
            var q2 = Enumerable.Repeat(60, 1000).Select(n => new { n }).ToList(); // 產生 重複60 1000個
            dataGridView2.DataSource = q2;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);

            var q = from p in this.nwDataSet1.Products
                    orderby p.CategoryID ascending
                    group p by p.CategoryID  into g
                    select new { CategoryID = g.Key, Avg = $"{g.Average(p => p.UnitPrice):c2}" };

            this.dataGridView1.DataSource = q.ToList();

            //==========================================================
            // 太T-SQL

            var q1 = from p in this.nwDataSet1.Products join c in this.nwDataSet1.Categories                     
                     on p.CategoryID equals c.CategoryID
                     orderby c.CategoryName ascending
                     group p by c.CategoryName into g
                     select new { CategoryName = g.Key, Avg = $"{g.Average(p => p.UnitPrice):c2}" };            

            this.dataGridView2.DataSource = q1.ToList();
        }
    }
}
