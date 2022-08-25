using LiveCharts.Wpf;
using LiveCharts;
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

namespace CW_2508
{
    /// <summary>
    /// Логика взаимодействия для MyChart.xaml
    /// </summary>
    public partial class MyChart : UserControl
    {
        public MyChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string Name_X { get; set; }
        public string Name_Y { get; set; }

    }
}

