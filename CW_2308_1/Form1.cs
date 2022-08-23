using Newtonsoft.Json;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CW_2308_1
{
    public partial class Form1 : Form
    {
        BindingList<Department> departments;
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        delegate void SetListBox(BindingList<Department> departments);

        private async void LoadData()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri("https://api.privatbank.ua/p24api/pboffice?json&city=&address=");
                    HttpResponseMessage message = await client.SendAsync(request).ConfigureAwait(false);
                    string resp = await message.Content.ReadAsStringAsync();
                    departments = JsonConvert.DeserializeObject<BindingList<Department>>(resp);
                    departments = new BindingList<Department>(departments.OrderBy(o => o.State).ToList());
                    listBox1.BeginInvoke(new SetListBox(SetLBX), departments);
                }
            }
        }
        private void SetLBX(BindingList<Department> departments)
        {
            listBox1.DataSource = departments;
        }

        public class Department
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("index")]
            public string Index { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            public override string ToString()
            {
                return $"{City}, {Index}: {Name}; {Address}, {Phone}, {Email}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {

                BindingList<Department> filtedDept = new BindingList<Department>(departments.Where(o => o.City.Contains(textBox1.Text)).ToList());
                listBox1.DataSource = filtedDept;
            }
            else
            {
                listBox1.DataSource = departments;
            }
        }
    }
}
