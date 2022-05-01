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
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();            
            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);
            LoadYearToComboBox();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {            
            this.dataGridView1.DataSource = this.awDataSet1.ProductPhoto.ToList();
        }

        private void btnSelectDate_Click(object sender, EventArgs e)
        {            
            DateTime date1 = dateTimePicker1.Value < dateTimePicker2.Value ? dateTimePicker1.Value : dateTimePicker2.Value;
            DateTime date2 = dateTimePicker1.Value > dateTimePicker2.Value ? dateTimePicker1.Value : dateTimePicker2.Value;
                
            this.dataGridView1.DataSource = this.awDataSet1.ProductPhoto
                .Where(p => p.ModifiedDate >= date1 && p.ModifiedDate <= date2).ToList();
        }

        private void btnSelectYear_Click(object sender, EventArgs e)
        {            
            int selectedYear = Int32.Parse(this.comboBox3.Text);
            this.dataGridView1.DataSource = 
                this.awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Year == selectedYear).ToList();            
        }

        private void LoadYearToComboBox()
        {
            this.comboBox3.DataSource = 
                this.awDataSet1.ProductPhoto.Select(s => s.ModifiedDate.Year).Distinct().ToList();
        }

        private void btnSelectQuarter_Click(object sender, EventArgs e)
        {
            int selectedYear = Int32.Parse(this.comboBox3.Text);

            this.dataGridView1.DataSource = this.awDataSet1.ProductPhoto
                .Where(p => p.ModifiedDate.Year == selectedYear
                && (((p.ModifiedDate.Month) - 1 )/ 3) == (this.comboBox2.SelectedIndex)).ToList();             
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] bytes = (byte[]) this.dataGridView1.CurrentRow.Cells[3].Value;            
            pictureBox1.Image = Image.FromStream(new MemoryStream(bytes));
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            label1.Text = dataGridView1.Rows.Count.ShowCount();
        }
    }
}
static class MyExtension
{
    internal static string ShowCount(this int n) => $"共 {n} 筆";
}
