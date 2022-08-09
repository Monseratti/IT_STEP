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
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Goods;

namespace CW_0908
{
    public partial class Form1 : Form
    {
        BindingList<Goods.Goods> MyGoods = new BindingList<Goods.Goods>();
        public Form1()
        {
            InitializeComponent();
            dgvData.DataSource = MyGoods;
        }

        private void btnSendID_Click(object sender, EventArgs e)
        {

            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8080);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                socket.Connect(iPEndPoint);
                if (socket.Connected)
                {
                    byte[] data = Encoding.Unicode.GetBytes(tbxID.Text);
                    socket.Send(data);
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);

                    Goods.Goods tmp = JsonConvert.DeserializeObject<Goods.Goods>(builder.ToString());
                    if (tmp != null) MyGoods.Add(tmp);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
