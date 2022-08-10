using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyStreet;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HW_0908
{
    public partial class Form1 : Form
    {
        BindingList<Streets> streets = new BindingList<Streets>();
        public BindingList<Streets> Streets { get => streets; set => streets = value; }
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Streets;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 1024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                socket.Connect(iPEndPoint);
                if (socket.Connected)
                {
                    socket.Send(Encoding.Unicode.GetBytes(tbxID.Text));
                    byte[] data = new byte[1024];
                    int bytes = 0;
                    string message = "";
                    do
                    {
                        bytes = socket.Receive(data);
                        message = Encoding.Unicode.GetString(data, 0, bytes);
                    } while (socket.Available > 0);
                    var _streets = JsonConvert.DeserializeObject<List<Streets>>(message);
                    foreach (var item in _streets)
                    {
                        Streets.Add(item);
                    }
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(DateTime.Now + ": " + ex.Message);
            }
        }
    }
}
