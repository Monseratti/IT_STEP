using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_1407
{
    public partial class Form1 : Form
    {
        public int counter = 0;
        public BindingList<SomeObject> create_threads = new BindingList<SomeObject>();
        public BindingList<SomeObject> waiting_threads = new BindingList<SomeObject>();
        public BindingList<SomeObject> working_threads = new BindingList<SomeObject>();
        public Semaphore s = null;
        public Form1()
        {
            InitializeComponent();
            Update();
        }
        new public void Update()
        {
            listBox1.DataSource = create_threads;
            listBox2.DataSource = waiting_threads;
            listBox3.DataSource = working_threads;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Enabled)
            {
                s = new Semaphore((int)numericUpDown1.Value, (int)numericUpDown1.Value, "My_S");
                numericUpDown1.Enabled = false;
            }
            counter++;
            SomeObject some = new SomeObject(counter);
            some.Form = this;
            create_threads.Add(some);
            Update();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                (listBox1.SelectedItem as SomeObject).St = SomeObject.Status.waiting;
                waiting_threads.Add(listBox1.SelectedItem as SomeObject);
                create_threads.Remove(listBox1.SelectedItem as SomeObject);
            }
            Update();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                (listBox3.SelectedItem as SomeObject).St = SomeObject.Status.off;
                (listBox3.SelectedItem as SomeObject).Thr.Abort();
                create_threads.Add(listBox3.SelectedItem as SomeObject);
                working_threads.Remove(listBox3.SelectedItem as SomeObject);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (working_threads.Count != 0)
            {
                foreach (var item in working_threads)
                {
                    item.Thr.Abort();
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
