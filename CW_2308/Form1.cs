using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static CW_2308.Form1;

namespace CW_2308
{
    public partial class Form1 : Form
    {
        List<Course> courseList;
        public Form1()
        {
            InitializeComponent();
            LoadCourses();
        }

        delegate void SetCbx(List<Course> courses);

        private async void LoadCourses()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
                    HttpResponseMessage message = await client.SendAsync(request).ConfigureAwait(false);
                    string resp = await message.Content.ReadAsStringAsync();
                    courseList = JsonConvert.DeserializeObject<List<Course>>(resp);
                    courseList.Add(new Course() 
                    { R030 = 980, Cc = "UAH", Exchangedate = DateTime.Today.ToShortDateString(), Rate = 1, Txt = "Українська гривня" });
                    courseList = courseList.OrderBy(o => o.Txt).ToList();
                    comboBox1.BeginInvoke(new SetCbx(SetComboBox1Value), courseList);
                    comboBox2.BeginInvoke(new SetCbx(SetComboBox2Value), courseList);
                }
            }
        }

        private void SetComboBox1Value(List<Course> courses)
        {
            comboBox1.Items.AddRange(courses.ToArray());
            comboBox1.SelectedItem = courses.Where(o => o.Cc == "USD").First();
        }
        private void SetComboBox2Value(List<Course> courses)
        {
            comboBox2.Items.AddRange(courses.ToArray());
            comboBox2.SelectedItem = courses.Where(o => o.Cc == "UAH").First();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public class Course
        {
            [JsonProperty("r030")]
            public int R030 { get; set; }

            [JsonProperty("txt")]
            public string Txt { get; set; }

            [JsonProperty("rate")]
            public double Rate { get; set; }

            [JsonProperty("cc")]
            public string Cc { get; set; }

            [JsonProperty("exchangedate")]
            public string Exchangedate { get; set; }

            public override string ToString()
            {
                return $"{Txt}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != string.Empty)
                {
                    double value = (comboBox1.SelectedItem as Course).Rate / (comboBox2.SelectedItem as Course).Rate * Convert.ToDouble(textBox1.Text);
                    textBox2.Text = value.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cc1 = (comboBox1.SelectedItem as Course).Cc;
            string Cc2 = (comboBox2.SelectedItem as Course).Cc;
            comboBox1.SelectedItem = courseList.Where(o => o.Cc == Cc2).First();
            comboBox2.SelectedItem = courseList.Where(o => o.Cc == Cc1).First();
            textBox1_TextChanged(textBox1, new EventArgs());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(textBox1, new EventArgs());
        }
    }
}
