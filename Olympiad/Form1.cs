using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Olympiad.Classes;

namespace Olympiad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using (OlympaidContext db = new OlympaidContext())
            {
                db.Countries.Add(new Country() { Name = "Ukraine" });
                db.SaveChanges();
                db.Olympaids.Add(new Olympaid() {
                    Year = 2021,
                    ParentCountryId = db.Countries.Where(o => o.Name.Equals("Ukraine")).Select(x => x.Id).FirstOrDefault(),
                    CityName = "kyiv", IsWinter = false
                })
                comboBox1.DataSource = db.Olympaids.Select(o=>o.Year).ToList();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked == true) comboBox1.Enabled = true;
            else
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = -1;
            }
        }
    }
}
