using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using System.Threading;
using Class_User;
using System.IO;
using Newtonsoft.Json;

namespace T_T_T_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region properties

        private IPAddress MulticastIpAddress = IPAddress.Parse("245.5.5.5");

        public string UserName { get; set; }
        public string ServerIP { get; set; }
        public string CurrentIP { get; set; }
        public int ServerPort { get; set; }
        public int CurrentPort { get; set; }
        public User User { get; set; }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        #region Windows Control

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Btn_Settings_Click(new Button(), new RoutedEventArgs());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void Btn_Min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region User Settings
        private void Btn_UserRoom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings winSettings = new Settings
            {
                Owner = this
            };
            Visibility = Visibility.Hidden;
            if (winSettings.ShowDialog() == true)
            {
                UserName = winSettings.UserName;
                CurrentIP = winSettings.CurrentIP;
                ServerIP = winSettings.ServerIP;
                ServerPort = int.Parse(winSettings.ServerPort);
                CurrentPort = int.Parse(winSettings.CurrentPort);
                InitializeUser();
                Visibility = Visibility.Visible;
            }
            else Application.Current.Shutdown();
        }
        #endregion

        #region Server Connect

        private void InitializeUser()
        {
            try
            {
                UdpClient udpClient = new UdpClient();
                IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);
                byte[] buffer = Encoding.Unicode.GetBytes($"{UserName};{CurrentPort}");
                udpClient.Send(buffer, buffer.Length, iPEnd);
                udpClient.Close();
            }
            catch (SocketException se)
            {
                MessageBox.Show($"Socket error: {se.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            Thread thread = new Thread(UDP_Receive);
            thread.Start();
        }

        private void UDP_Receive()
        {
            while (User == null)
            {
                try
                {
                    UdpClient uClient = new UdpClient(CurrentPort);
                    IPEndPoint ipEnd = null;
                    byte[] responce = uClient.Receive(ref ipEnd);
                    string strResult = Encoding.Unicode.GetString(responce);
                    User = JsonConvert.DeserializeObject<User>(strResult);
                    uClient.Close();
                    Dispatcher.Invoke(() =>
                    {
                        _title.Text = $"{User.Name};{User.GamePort}:{User.GameID}";
                    });
                }
                catch (SocketException se)
                {
                    MessageBox.Show($"Socket error: {se.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void Bnt_Field_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = User.Mark;
        }

        #endregion
    }
}
