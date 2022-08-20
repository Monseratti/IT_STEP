using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace AT_1808
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string strIP { get; set; }
        public int RemotePort { get; set; }
        public int LocalPort { get; set; }
        public string message { get; set; }
        public string userName { get; set; }
        IPAddress RemoteIPAddr;

        public MainWindow()
        {
            strIP = "224.5.5.5";
            RemotePort = 7777;
            message = "test";
            InitializeComponent();
        }
        void Listener()
        {
            while (true)
            {
                try
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8080);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 2);
                    socket.Bind(endPoint);
                    IPAddress iP = IPAddress.Parse(strIP);
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(iP, IPAddress.Any));
                    byte[] buffer = new byte[1024];
                    socket.Receive(buffer);
                    Action action = () => { lbxChat.Items.Add(Encoding.Unicode.GetString(buffer)); };
                    Dispatcher.Invoke(action);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void btn_SetPort_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_LPort.Text != string.Empty || tbx_LPort.Text != "")
            {

                LocalPort = Convert.ToInt32(tbx_LPort.Text);
                Thread listen = new Thread(Listener);
                listen.IsBackground = true;
                listen.Start();
            }
            else
            {
                MessageBox.Show("Set local port");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RemoteIPAddr = IPAddress.Parse("127.0.0.1");
            string _mes = $"[{DateTime.Now}] {userName}: {message}";
            UdpClient uClient = new UdpClient();
            IPEndPoint ipEnd = new IPEndPoint(RemoteIPAddr, RemotePort);
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(_mes);
                uClient.Send(bytes, bytes.Length, ipEnd);
            }
            catch (SocketException sockEx)
            {
                Console.WriteLine("Ошибка сокета:" + sockEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка:" + ex.Message);
            }
            finally
            {
                uClient.Close();
            }
            message = string.Empty;
        }
    }
}

