using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_HW1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet();
            timer1.Interval = 2000;
            timer1.Start();
        }
        private void DataSet()
        {
            dataGridView1.Columns.Add("Id", "Process ID");
            dataGridView1.Columns.Add("Name", "Process name");
            dataGridView1.Columns.Add("Thread_C", "Count of threads");
            dataGridView1.Columns.Add("Handle_C", "Count of handles");
            try
            {
                foreach (Process p in Process.GetProcesses())
                {
                    dataGridView1.Rows.Add(p.Id, p.ProcessName, p.Threads.Count, p.HandleCount);
                }
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            sw.Write($"{cell.Value}");
                            sw.Write($"---");
                        }
                        sw.Write("\n");
                    }
                    sw.Flush();
                }
            }
        }
    }
}
