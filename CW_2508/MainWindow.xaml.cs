using CW_2508.Classes;
using Newtonsoft.Json;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Shell;

namespace CW_2508
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string MyAPIKey { get; set; }
        public string MyCity { get; set; }
        public List<Country> Country { get; set; }
        public WeatherData Weather { get; set; }

        public MainWindow()
        {
            MaxWidth = SystemParameters.WorkArea.Width;
            MaxHeight = SystemParameters.WorkArea.Height;
            MyAPIKey = "767afa044a6cfd0d5886594427b539f3";
            InitializeComponent();
        }

        #region FormControl
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Get_Cities
        delegate void SetCbx(List<Country> countries);

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri($"http://api.openweathermap.org/geo/1.0/direct?q={MyCity},&limit=5&appid={MyAPIKey}");//%20UA
                    HttpResponseMessage message = await client.SendAsync(request).ConfigureAwait(false);
                    string resp = await message.Content.ReadAsStringAsync();
                    Country = JsonConvert.DeserializeObject<List<Country>>(resp);
                    await cbxCities.Dispatcher.BeginInvoke(new SetCbx(SetData), Country);
                }
            }
        }

        private void SetData(List<Country> countries)
        {
            cbxCities.Items.Clear();
            foreach (var item in countries)  cbxCities.Items.Add(item);
            if (cbxCities.Items.Count != 0)  cbxCities.SelectedIndex = 0;
        }
        #endregion

        #region Get_Weather
        private async void cbxCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxCities.SelectedIndex != -1)
            {
                try
                {
                    Country selectCountry = cbxCities.SelectedItem as Country;
                    tbxLat.Text = selectCountry.lat.ToString();
                    tbxLon.Text = selectCountry.lon.ToString();
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpRequestMessage request = new HttpRequestMessage())
                        {
                            request.Method = HttpMethod.Get;
                            request.RequestUri = new Uri($"http://api.openweathermap.org/data/2.5/forecast?lat={selectCountry.lat}&lon={selectCountry.lon}&appid={MyAPIKey}&units=metric");
                            HttpResponseMessage message = await client.SendAsync(request).ConfigureAwait(false);
                            string resp = await message.Content.ReadAsStringAsync();
                            Weather = JsonConvert.DeserializeObject<WeatherData>(resp);
                        }
                    }
                    Dispatcher.Invoke(() =>
                    {
                        List<double> tempList = new List<double>();
                        List<double> humidityList = new List<double>();
                        List<string> labels = new List<string>();
                        foreach (var weather in Weather.List)
                        {
                            tempList.Add(weather.Main.Temp);
                            humidityList.Add(weather.Main.Humidity);
                            labels.Add(weather.DtTxt);
                        }
                        FillCharts(mychTemp, tempList, labels, "Temp, C°", "Temperature");
                        FillCharts(mychH, humidityList, labels, "Humidity, PA", "Humidity");
                        mychTemp.Visibility = Visibility.Visible;
                        mychH.Visibility = Visibility.Visible;
                    }
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    mychTemp.Visibility = Visibility.Hidden;
                    mychH.Visibility = Visibility.Hidden;
                    tbxLat.Text = string.Empty;
                    tbxLon.Text = string.Empty;
                });
            }
        }
        private void FillCharts(MyChart chart, List<double> values, List<string> labels, string name_Y, string title)
        {
            chart.SeriesCollection.Clear();
            chart.Labels = labels.ToArray();
            chart.Name_X = "Days";
            chart.Name_Y = name_Y;
            var item = new LineSeries();
            var value = new ChartValues<double>(values);
            item.Title = title;
            item.Values = value;
            chart.SeriesCollection.Add(item);
        }
        #endregion
    }
}
