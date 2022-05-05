using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        NorthwindEntities dbContext = new NorthwindEntities();
        string[] question1 = { "共幾個 學員成績 ?", "找出 前面三個 的學員所有科目成績", "找出 後面兩個 的學員所有科目成績", "找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績", "找出學員 'bbb' 的成績", "找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)", "找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績", "數學不及格 ... 是誰" };
        string[] question2 = { "個人 sum, min, max, avg", "各科 sum, min, max, avg" };
        public Frm考試()
        {
            InitializeComponent();
            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };
        }

        List<Student> students_scores;
        List<Student> students_scores2;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績
            // 
            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績					
            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
            // 數學不及格 ... 是誰 
            #endregion

            ClearData();
            int index = this.comboBox1.SelectedIndex;

            switch (index)
            {
                case 0:
                    this.listBox1.Items.Add("共 " + students_scores.Count + " 個學員成績");
                    break;
                case 1:
                    this.dataGridView1.DataSource = students_scores.Take(3).ToList();
                    break;
                case 2:
                    this.dataGridView1.DataSource = students_scores.Skip(students_scores.Count - 2).ToList();
                    break;
                case 3:
                    this.dataGridView1.DataSource = students_scores
                        .Where(s => s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc")
                        .Select(s => new { s.Name, s.Chi, s.Eng }).ToList();
                    break;
                case 4:
                    this.dataGridView1.DataSource = students_scores.Where(s => s.Name == "bbb").ToList();
                    break;
                case 5:
                    this.dataGridView1.DataSource = students_scores.Where(s => s.Name != "bbb").ToList();
                    break;
                case 6:
                    this.dataGridView1.DataSource = students_scores
                        .Where(s => s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc")
                        .Select(s => new { s.Name, s.Chi, s.Math }).ToList();
                    break;
                case 7:
                    this.dataGridView1.DataSource = students_scores.Where(s => s.Math < 60).ToList();
                    break;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            //各科 sum, min, max, avg

            ClearData();
            int index = this.comboBox2.SelectedIndex;

            switch (index)
            {
                case 0:
                    this.dataGridView1.DataSource = students_scores
                        .Select(s => new
                        {
                            Name = s.Name,
                            Sum = s.Chi + s.Eng + s.Math,
                            Min = Min(s.Chi, s.Eng, s.Math),
                            Max = Max(s.Chi, s.Eng, s.Math),
                            Avg = $"{(double)(s.Chi + s.Eng + s.Math) / 3:f2}"
                        })
                        .ToList();
                    break;
                case 1:
                    var chi = students_scores.Select(s => new { Subject = "Chinese", Sum = s.Chi })
                        .GroupBy(g => g.Subject)
                        .Select(s => new { Subject = s.Key, Sum = s.Sum(p => p.Sum), Min = s.Min(p => p.Sum), Max = s.Max(p => p.Sum), Avg = $"{ s.Average(p => p.Sum):f2}" })
                        .ToList();
                    var eng = students_scores.Select(s => new { Subject = "English", Sum = s.Eng })
                        .GroupBy(g => g.Subject)
                        .Select(s => new { Subject = s.Key, Sum = s.Sum(p => p.Sum), Min = s.Min(p => p.Sum), Max = s.Max(p => p.Sum), Avg = $"{ s.Average(p => p.Sum):f2}" })
                        .ToList();
                    var math = students_scores.Select(s => new { Subject = "Chinese", Sum = s.Math })
                        .GroupBy(g => g.Subject)
                        .Select(s => new { Subject = s.Key, Sum = s.Sum(p => p.Sum), Min = s.Min(p => p.Sum), Max = s.Max(p => p.Sum), Avg = $"{ s.Average(p => p.Sum):f2}" })
                        .ToList();

                    chi.Add(eng[0]);
                    chi.Add(math[0]);
                    dataGridView1.DataSource = chi;
                    break;
            }
        }

        Random rd = new Random();
        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)

            ClearData();
            students_scores2 = new List<Student>();
            students_scores2.Clear();

            for (int i = 0; i < 100; i++)
            {
                students_scores2.Add(new Student { Name = i.ToString(), Chi = rd.Next(50, 101) });
            }

            this.dataGridView1.DataSource = students_scores2
                .Select(s => new { Name = s.Name, Split = MySplit(s.Chi), Chi = s.Chi })
                .OrderByDescending(o => o.Chi)
                .ToList();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%

            this.dataGridView1.DataSource = this.students_scores2.GroupBy(g => g.Chi)
                .Select(s => new
                {
                    Score = s.Key,
                    Count = s.Count(),
                    Percentage = $"{((double)s.Count()/ students_scores2.Count)*100:f2}%"
                })
                .OrderByDescending(o=>o.Percentage)
                .ToList();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            var q = this.dbContext.Order_Details.AsEnumerable()
                .Select(o => new { Year = o.Order.OrderDate.Value.Year, SalesFigures = o.UnitPrice * o.Quantity })
                .GroupBy(g => g.Year)
                .Select(s => new { Year = s.Key, SalesFigures = $"{s.Sum(o => o.SalesFigures):c2}" })
                .OrderByDescending(s => s.SalesFigures);            

            listBox1.Items.Add("年度最高銷售金額: " + q.First().SalesFigures);
            listBox1.Items.Add("年度最低銷售金額: " + q.Last().SalesFigures);
            listBox1.Items.Add("================================");
            listBox1.Items.Add("哪一年總銷售最好: " + q.First().Year);
            listBox1.Items.Add("哪一年總銷售最不好: " + q.Last().Year);
            listBox1.Items.Add("================================");

            var query = this.dbContext.Order_Details.AsEnumerable()
                .Select(o => new { Month = o.Order.OrderDate.Value.Year + "年 " + o.Order.OrderDate.Value.Month + "月", SalesFigures = o.UnitPrice * o.Quantity })
                .GroupBy(g => g.Month)
                .Select(s => new { Month = s.Key, SalesFigures = $"{s.Sum(o => o.SalesFigures):c2}" })
                .OrderByDescending(s => s.SalesFigures);

            listBox1.Items.Add("哪一個月總銷售最好: " + query.First().Month);
            listBox1.Items.Add("哪一個月總銷售最不好: " + query.Last().Month);

            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "Year";
            chart1.Series[0].YValueMembers = "SalesFigures";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            chart2.DataSource = query.ToList();
            chart2.Series[0].XValueMember = "Month";
            chart2.Series[0].YValueMembers = "SalesFigures";
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            // 年度最高銷售金額 年度最低銷售金額
            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            this.button36.Enabled = true;
            this.comboBox1.DataSource = question1;
        }

        void ClearData()
        {
            this.dataGridView1.DataSource = null;
            this.listBox1.Items.Clear();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            this.button37.Enabled = true;
            this.comboBox2.DataSource = question2;
        }
        private int Max(int s1, int s2, int s3)
        {
            int result = s1;
            if (result < s2)
                result = s2;
            else if (result < s3)
                result = s3;
            return result;
        }

        private int Min(int s1, int s2, int s3)
        {
            int result = s1;
            if (result > s2)
                result = s2;
            else if (result > s3)
                result = s3;
            return result;
        }

        private string MySplit(int n)
        {
            if (n >= 60 && n <= 69)
                return "待加強";
            else if (n >= 70 && n <= 89)
                return "佳";
            else if (n >= 90)
                return "優良";
            else
                return "差勁";
        }

        private string GrowthRate(decimal q1, decimal q2)
        {
            string result = $"{(q1 - q2) / q2 * 100:f2}%";
            return result;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearData();
            var q = this.dbContext.Order_Details.AsEnumerable()
                    .Select(o => new { Year = o.Order.OrderDate.Value.Year, SalesFigures = o.UnitPrice * o.Quantity })
                    .GroupBy(g => g.Year)
                    .Select(s => new { Year = s.Key, SalesFigures = s.Sum(o => o.SalesFigures) })
                    .OrderBy(s => s.Year);

            this.dataGridView1.DataSource = q.Skip(1)
                .Zip(q.Take(q.Count() - 1), (q1, q2) => new { Year = q1.Year, GrowthRate = GrowthRate(q1.SalesFigures, q2.SalesFigures) })
                .ToList();
        }       
    }
}
