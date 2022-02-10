using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Olympiad.Classes;

namespace Olympiad
{
    public partial class Form2 : Form
    {
        OlympaidContext db = new OlympaidContext();
        DataGridViewRow selectedrow = null;
        string table = "";
        int index = 0;
        public Form2(string _table)
        {
            table = _table;
            InitializeComponent();
            UpdateData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void UpdateData()
        {
            dataGridView1.DataSource = null;
            switch (table)
            {
                case "Countries":
                    dataGridView1.DataSource = db.Countries.Select(o => new { o.Id, o.Name }).ToList();
                    index = 0;
                    break;
                case "Sports":
                    dataGridView1.DataSource = db.Sports.Select(o => new { o.Id, o.Name }).ToList();
                    index = 1;
                    break;
                case "Sportsmen":
                    dataGridView1.DataSource = db.Sportsmens.Select(o => new { o.Id, o.Name, o.BirthDay, o.Photo, o.CountryId }).ToList();
                    index = 2;
                    break;
                case "Olympaids":
                    dataGridView1.DataSource = db.Olympaids.Select(o => 
                    new { o.Id, o.Year, o.ParentCountryId, o.CityName, o.IsWinter }).ToList();
                    index = 3;
                    break;
                case "OlympaidResults":
                    dataGridView1.DataSource = db.OlympaidResults.Select(o => 
                    new { o.Id, o.OlSpId, o.SportsmenId, o.GoldenMedal, o.SilverMedal, o.BronzeMedal }).ToList();
                    index = 4;
                    break;
                case "OlympaidSports":
                    dataGridView1.DataSource = db.OlympaidSports.Select(o => new { o.Id, o.OlympaidId, o.SportsId }).ToList();
                    index = 5;
                    break;
                default:
                    break;
            }
        }
        private int SelectedID()
        {
            return (int)selectedrow.Cells[0].Value;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(index);
            form3.ShowDialog();
            UpdateData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = SelectedID();
            switch (index)
            {
                case 0:
                    if (db.Sportsmens.Where(o => o.CountryId.Equals(id)).Count() != 0||
                        db.Olympaids.Where(o=>o.ParentCountryId.Equals(id)).Count()!=0)
                    {
                        MessageBox.Show("This country use. Did not deleted"); return;
                    }
                    else
                    {
                        Country del = db.Countries.Find(id);
                        db.Countries.Remove(del);
                    }
                    break;
                case 1:
                    if (db.OlympaidSports.Where(o => o.SportsId.Equals(id)).Count() != 0)
                    {
                        MessageBox.Show("This sport use. Did not deleted"); return;
                    }
                    else
                    {
                        Sports del1 = db.Sports.Find(id);
                        db.Sports.Remove(del1);
                    }
                    break;
                case 2:
                    if (db.OlympaidResults.Where(o => o.SportsmenId.Equals(id)).Count() != 0)
                    {
                        MessageBox.Show("This sportsmen's card use. Did not deleted"); return;
                    }
                    else
                    {
                        Sportsmen del2 = db.Sportsmens.Find(id);
                        db.Sportsmens.Remove(del2);
                    }
                    break;
                case 3:
                    if (db.OlympaidSports.Where(o => o.OlympaidId.Equals(id)).Count() != 0)
                    {
                        MessageBox.Show("This olympaid's card use. Did not deleted"); return;
                    }
                    else
                    {
                        Olympaid del3 = db.Olympaids.Find(id);
                        db.Olympaids.Remove(del3);
                    }
                    break;
                case 4:
                    
                        OlympaidResults del4 = db.OlympaidResults.Find(id);
                        db.OlympaidResults.Remove(del4);
                    break;
                case 5:
                    if (db.OlympaidResults.Where(o => o.OlSpId.Equals(id)).Count() != 0)
                    {
                        MessageBox.Show("This olympaid-sports' card use. Did not deleted"); return;
                    }
                    else
                    {
                        OlympaidSports del5 = db.OlympaidSports.Find(id);
                        db.OlympaidSports.Remove(del5);
                    }
                    break;
            }
            db.SaveChanges();
            UpdateData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if ((sender as DataGridView).SelectedRows.Count != 0)
            {
                selectedrow = (sender as DataGridView).SelectedRows[0];
            }
            else if ((sender as DataGridView).SelectedCells.Count != 0)
            {
                selectedrow = (sender as DataGridView).SelectedCells[0].OwningRow;
            }
            else
            {
                selectedrow = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(index, SelectedID());
            form3.ShowDialog();
            UpdateData();
        }
    }
}
