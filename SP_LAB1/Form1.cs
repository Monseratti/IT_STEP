using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_LAB1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pr = Process.GetProcesses().ToList();
            foreach (var p in pr)
            {
                listView1.Items.Add($"{p.Id,-10}{p.ProcessName}");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {

                listView2.Items.Clear();
                listView3.Items.Clear();
                string[] tmp = listView1.SelectedItems[0].Text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var thrs = Process.GetProcessesByName(tmp[1]);
                foreach (var thr in thrs)
                {
                    try
                    {
                        foreach (ProcessModule item in thr.Modules)
                        {
                            listView2.Items.Add($"{item.ModuleName}_____{item.FileName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        listView2.Items.Add($"{ex.Message}");
                    }
                    try
                    {
                        foreach (ProcessThread item in thr.Threads)
                        {
                            listView3.Items.Add($"{item.Id}_____{item.PriorityLevel}_____{item.StartTime}");
                        }
                    }
                    catch (Exception ex)
                    {
                        listView3.Items.Add($"{ex.Message}");
                    }
                }
            }
        }
    }
}
