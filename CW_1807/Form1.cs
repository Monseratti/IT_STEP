using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CW_1807
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.Columns.Add("Name", 200);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("Value", -2);
            TreeViewFill();
        }

        public void TreeViewFill()
        {
            RegistryKey[] key = new[] { Registry.CurrentUser, Registry.Users, Registry.CurrentConfig, Registry.LocalMachine, Registry.PerformanceData };
            foreach (var item in key)
            {
                TreeNode node = new TreeNode(item.Name);
                node.Tag = item;
                node.Nodes.AddRange(ChildFill((RegistryKey)node.Tag));
                treeView1.Nodes.Add(node);
            }

        }
        public TreeNode[] ChildFill(RegistryKey key)
        {
            string[] children = key.GetSubKeyNames();
            TreeNode[] treeNode = new TreeNode[children.Length];
            for (int i = 0; i < children.Length; i++)
            {
                treeNode[i] = new TreeNode(children[i]);
                treeNode[i].Tag = key;
            }
            return treeNode;
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            RegistryKey key = null;
            TreeNode node = e.Node;
            if (node.Tag is RegistryKey)
            {
                key = (RegistryKey)node.Tag;
            }
            try
            {
                node.Nodes.Clear();
                foreach (var item in key.GetSubKeyNames())
                {
                    TreeNode node1 = new TreeNode(item);
                    node1.Tag = key.OpenSubKey(item);
                    node1.Nodes.AddRange(ChildFill((RegistryKey)node1.Tag));
                    node.Nodes.Add(node1);
                }
            }
            catch (Exception)
            {
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RegistryKey key = null;
            TreeNode node = e.Node;
            if (node.Tag is RegistryKey)
            {
                key = (RegistryKey)node.Tag;
            }
            string[] vnames = key.GetValueNames();
            listView1.Items.Clear();
            foreach (var item in vnames)
            {
                try
                {
                    string name = "";
                    if (item == name) name = "(default)";
                    else name = item;
                    ListViewItem viewItem = new ListViewItem(name);
                    viewItem.SubItems.Add(key.GetValueKind(item).ToString());
                    viewItem.SubItems.Add(key.GetValue(item).ToString());
                    listView1.Items.Add(viewItem);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
