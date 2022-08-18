using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_1808
{
    public partial class Form1 : Form
    {
        TcpListener listener;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "8080";
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener?.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            listener.Start();
            Thread thread = new Thread(Start) { IsBackground = true };
            thread.Start();
        }

        private void Start()
        {
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    using (StreamReader sr = new StreamReader(client.GetStream(), Encoding.Unicode))
                    {
                        string s = sr.ReadLine();
                        Action action = () =>
                        {
                            listBox1.Items.Add(s);
                        };
                        Invoke(action);
                        if (s.ToLower() == "exit")
                        {
                            listener?.Stop();
                            Close();
                        }
                    }
                    client.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
