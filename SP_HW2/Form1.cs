using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_HW2
{
    public partial class Form1 : Form
    {
        Dictionary<int, int> SystemProcesses = new Dictionary<int, int>();
        public Form1()
        {
            InitializeComponent();
        }
        private int GetParentProcessId(int id)
        {
            int parentId = 0;

            using (ManagementObject obj = new ManagementObject($"Win32_Process.Handle={id}"))
            {
                try
                {
                    obj.Get();
                    parentId = int.Parse(obj["ParentProcessId"].ToString());
                }
                catch (Exception) { }
            };
            return parentId;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcesses())
            {
                SystemProcesses.Add(process.Id, GetParentProcessId(process.Id));
            }
            try
            {
                foreach (var sp in SystemProcesses)
                {
                    if (sp.Key == sp.Value)
                    {
                        treeView1.Nodes.Add($"{Process.GetProcessById(sp.Key).ProcessName}, {sp.Key}");
                    }
                    else
                    {
                        bool flag = false;
                        foreach (var sp1 in SystemProcesses)
                        {
                            if (sp.Value == sp1.Key)
                            {
                                flag = true;
                            }
                        }
                        foreach (TreeNode item in treeView1.Nodes)
                        {
                            if (item.Text == Process.GetProcessById(sp.Key).ProcessName)
                            {
                                flag = true;
                            }
                        }
                        if (!flag)
                        treeView1.Nodes.Add($"{Process.GetProcessById(sp.Key).ProcessName}, {sp.Key}");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (var sp in SystemProcesses)
            {
                foreach (TreeNode item in treeView1.Nodes)
                {
                    try
                    {
                        if (item.Text == $"{Process.GetProcessById(sp.Value).ProcessName}, {sp.Value}"
                            && item.Text != $"{Process.GetProcessById(sp.Key).ProcessName}, {sp.Key}")
                        {
                            item.Nodes.Add($"{Process.GetProcessById(sp.Key).ProcessName}, {sp.Key}");
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
