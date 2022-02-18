using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_LAB3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void ResetData()
        {
            listBox1.Items.Clear();
            List<string> list = new List<string>();
            foreach (Process p in Process.GetProcesses())
            {
                if (!list.Contains(p.ProcessName))
                    list.Add(p.ProcessName);
            }
            foreach (var item in list.OrderBy(o => o.First()).ToList())
            {
                listBox1.Items.Add(item);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ResetData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled && numericUpDown1.Value != 0)
            {
                timer1.Interval = (int)numericUpDown1.Value * 1000;
                timer1.Start();
                button1.Text = "Stop refresh";
            }
            else
            {
                button1.Text = "Start refresh";
                timer1.Stop();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                try
                {
                    textBox1.Text = "";
                    Process[] processes = Process.GetProcessesByName(listBox1.SelectedItems[0].ToString());
                    textBox1.Text += $"Process ID - {processes[0].Id}";
                    textBox1.AppendText(Environment.NewLine);
                    textBox1.Text += $"Start time - {processes[0].StartTime}\n\r";
                    textBox1.AppendText(Environment.NewLine);
                    textBox1.Text += $"Total processor time - {processes[0].TotalProcessorTime}\n\r";
                    textBox1.AppendText(Environment.NewLine);
                    textBox1.Text += $"Count of threads - {processes[0].Threads.Count}\n\r";
                    textBox1.AppendText(Environment.NewLine);
                    textBox1.Text += $"Count of process - {processes.Length}\n\r";
                }
                catch (Exception)
                { }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName(listBox1.SelectedItems[0].ToString());
                    foreach (Process item in processes)
                    {
                        item.CloseMainWindow();
                        item.Close();
                    }
                    ResetData();
                }
                catch (Exception)
                { }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Common file|*.exe";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Items.Add(opf.FileName);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(comboBox1.Text);
                ResetData();
            }
            catch (Exception)
            {
            }
        }
    }
}
