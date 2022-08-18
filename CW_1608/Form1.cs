using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Newtonsoft.Json;


namespace CW_1608
{
    public partial class Form1 : Form
    {
        TcpListener tcpListener;

        IPAddress _serverAdress = IPAddress.Parse("127.0.0.1");
        int _serverPort = 8084;

        //IPAddress _clientAdress = IPAddress.Parse("127.0.0.1");
        //int _clientPort = 8081;

        ImageList image = new ImageList();

        public Form1()
        {
            InitializeComponent();
            image.ImageSize = new Size(32, 32);
            pbx_Screen.SizeMode = PictureBoxSizeMode.StretchImage;
            btn_Start.Click += Btn_Start_Click;
            FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpListener?.Stop();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(_serverAdress, _serverPort);
            tcpListener = new TcpListener(iPEndPoint);
            tcpListener.Start();
            ClientConnection(5000);
            Thread _thread = new Thread(ReceiveScreenshots) { IsBackground = true };
            _thread.Start();
        }

        private void ClientConnection(object message)
        {
            try
            {
                string _mes = message.ToString();
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                NetworkStream nStream = tcpClient.GetStream();
                StreamWriter sr = new StreamWriter(nStream, Encoding.Unicode);
                sr.Write(_mes);
                //byte[] buffer = Encoding.Unicode.GetBytes(message as string);
                //nStream.Write(buffer, 0, buffer.Length);
                sr.Close();
                nStream.Close();
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceiveScreenshots()
        {
            while (true)
            {
                TcpClient cl = tcpListener.AcceptTcpClient();
                if (cl.Connected)
                {
                    NetworkStream ns = cl.GetStream();
                    Image img = Image.FromStream(ns);
                    image.Images.Add(img);
                    Action action = () =>
                    {
                        vlw_Screenshots.LargeImageList = image;
                        ListViewItem item = new ListViewItem();
                        item.ImageIndex = image.Images.Count;
                        vlw_Screenshots.Items.Add(item);
                        pbx_Screen.Image = img;
                    };
                    Invoke(action);
                    ns.Close();
                }
                cl.Close();
            }
        }

        private void vlw_Screenshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in vlw_Screenshots.SelectedItems)
            {
                pbx_Screen.Image = image.Images[item.ImageIndex];
            }
        }
    }
}
