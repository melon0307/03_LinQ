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
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void btnIntNoLinq_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 2, 4, 6, 8, 10 };
            List<string> lt = new List<string>();
            TreeNode keyNode = new TreeNode();
            TreeNode numNode = new TreeNode();

            treeView1.Nodes.Clear();
            this.dataGridView1.DataSource = null;
            for (int i = 0; i < nums.Length; i++)
            {
                string Key = MyKey(nums[i]);
                string number = nums[i].ToString();

                if (lt.Contains(Key))
                {
                    for (int j = 0; j < this.treeView1.Nodes.Count; j++)
                    {
                        if (this.treeView1.Nodes[j].Name == Key)
                        {
                            numNode = new TreeNode(number);
                            this.treeView1.Nodes[j].Nodes.Add(number);
                            numNode.Name = number;
                            this.treeView1.Nodes[j].Text = $"{Key} ({this.treeView1.Nodes[j].GetNodeCount(true)})";
                        }
                    }
                }
                else
                {
                    keyNode = treeView1.Nodes.Add(Key);
                    keyNode.Name = Key;
                    lt.Add(Key);

                    numNode = new TreeNode(number);
                    keyNode.Nodes.Add(number);
                    numNode.Name = number;
                }
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

        private void btnGroupByFileSize_Click(object sender, EventArgs e)
        {
            FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();
            var q = files.GroupBy(f => MySizeKey(f.Length))
                .Select(s => new { FileSize = s.Key, Count = s.Count(), Group = s }).ToList();
            dataGridView1.DataSource = q;

            treeView1.Nodes.Clear();
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
            FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();
            var q = files.GroupBy(f => f.CreationTime.Year).OrderBy(o => o.Key)
                .Select(s => new { Year = s.Key, Count = s.Count(), Group = s }).ToList();
            dataGridView1.DataSource = q;

            treeView1.Nodes.Clear();
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
    }
}
