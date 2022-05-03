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
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
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
                    if (!lt.Contains(number))
                    {
                        numNode = new TreeNode(number);
                        keyNode.Nodes.Add(number);
                        numNode.Name = number;
                        lt.Add(number);
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
                    lt.Add(number);
                }
                keyNode.Text = $"{Key} ({keyNode.Nodes.Count})";
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
            var q = files.GroupBy(f => MySizeKey(f.Length)).ToList();
            dataGridView1.DataSource = q;

            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode nodes = this.treeView1.Nodes.Add(group.Key.ToString());

                foreach (FileInfo item in group)
                {
                    nodes.Nodes.Add(item.ToString());
                }

                nodes.Text = $"{group.Key} ({nodes.Nodes.Count})";
            }
        }

        private void btnGroupByCreationYear_Click(object sender, EventArgs e)
        {
            FileInfo[] files = new DirectoryInfo(@"c:\windows").GetFiles();
            var q = files.GroupBy(f => f.CreationTime.Year).OrderBy(o=>o.Key).ToList();
            dataGridView1.DataSource = q;

            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode nodes = this.treeView1.Nodes.Add(group.Key.ToString());

                foreach (FileInfo item in group)
                {
                    nodes.Nodes.Add(item.ToString());
                }

                nodes.Text = $"{group.Key} ({nodes.Nodes.Count})";
            }
        }
    }
}
