using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_1808_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "8080";
            this.Cursor = Cursors.Default;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            using (NetworkStream ns = client.GetStream())
            {
                byte[] buffer = Encoding.Unicode.GetBytes(textBox3.Text);
                ns.Write(buffer, 0, buffer.Length);
            }
            client.Close();
            if (textBox3.Text.ToLower() == "exit")
            {
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
