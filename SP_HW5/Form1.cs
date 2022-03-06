using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SP_HW5
{
    public partial class Form1 : Form
    {
        string text = "";
        string text2 = "";
        ThreadStart ts = null;
        Thread t = null;
        public Form1()
        {
            InitializeComponent();
            ts = new ThreadStart(Write);
            t = new Thread(ts);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog() { Filter = "Text file| *.txt" };
            if (opf.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = opf.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(textBox1.Text))
            {
                while (!sr.EndOfStream)
                {
                    text += sr.ReadLine();
                }
            }
            progressBar1.Value = 30;
            var x = new XORCipher();
            if (radioButton1.Checked)
            {
                text2 = x.Encrypt(text, textBox2.Text);
            }
            else if (radioButton2.Checked)
            {
                text2 = x.Decrypt(text, textBox2.Text);
            }
            progressBar1.Value = 60;
            t.Start();
        }
        private void Write()
        {
            using (StreamWriter sw = new StreamWriter(textBox1.Text))
            {
                sw.Write(text2);
            }
            progressBar1.Invoke(new Action(delegate () { progressBar1.Value = 100; }));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            t.Abort();
        }
    }
}
