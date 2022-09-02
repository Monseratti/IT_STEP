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
using System.Windows.Shapes;

namespace T_T_T_Client
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public string UserName { get; set; }
        public string ServerIP { get; set; }
        public string CurrentIP { get; set; }
        public string ServerPort { get; set; }
        public string CurrentPort { get; set; }

        public Settings()
        {
            UserName = "Roman";
            ServerIP = "127.0.0.1";
            CurrentIP = "127.0.0.1";
            CurrentPort = "8080";
            ServerPort = "8081";
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (UserName != string.Empty && 
                CurrentIP != string.Empty &&
                ServerIP != string.Empty &&
                CurrentPort != string.Empty && 
                ServerPort != string.Empty) DialogResult = true;
            else MessageBox.Show("All properties must be filled");
        }

        private void btn_Cansel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
