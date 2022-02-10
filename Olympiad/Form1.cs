using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
            List<string> list = new List<string>();
            for (int i = 0; i < 11; i++)
            {
                list.Add($"Query {i + 1}");
            }
            list.Add("Reset");
            comboBox1.DataSource = list;
            using (OlympaidContext context = new OlympaidContext())
            {
                listBox1.DataSource = context.Database.SqlQuery<string>("SELECT name FROM sys.tables ORDER BY name").ToList().Where(o => o != "__MigrationHistory").ToList();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        label2.Visible = false;
                        comboBox2.Visible = false;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            dataGridView1.DataSource = db.Query1().ToList();
                        }
                        break;
                    case 1:
                        label2.Visible = true;
                        comboBox2.Visible = true;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            comboBox2.DataSource = db.Olympaids.Select(x => x.Year).ToList();
                        }
                        break;
                    case 2:
                        label2.Visible = false;
                        comboBox2.Visible = false;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            dataGridView1.DataSource = db.Query3().ToList();
                        }
                        break;
                    case 3:
                        label2.Visible = true;
                        comboBox2.Visible = true;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            comboBox2.DataSource = db.Olympaids.Select(x => x.Year).ToList();
                        }
                        break;
                    case 4:
                        label2.Visible = false;
                        comboBox2.Visible = false;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            dataGridView1.DataSource = db.Query5().ToList();
                        }
                        break;
                    case 5:
                        label2.Visible = true;
                        comboBox2.Visible = true;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            comboBox2.DataSource = db.Olympaids.Select(x => x.Year).ToList();
                        }
                        break;
                    case 6:
                        label2.Visible = true;
                        comboBox2.Visible = true;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            comboBox2.DataSource = db.Sports.Select(x => x.Name).ToList();
                        }
                        break;
                    case 7:
                        label2.Visible = false;
                        comboBox2.Visible = false;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            dataGridView1.DataSource = db.Query8().ToList();
                        }
                        break;
                    case 8:
                        label2.Visible = true;
                        comboBox2.Visible = true;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            comboBox2.DataSource = db.Countries.Select(x => x.Name).ToList();
                        }
                        break;
                    case 9:
                        label2.Visible = true;
                        comboBox2.Visible = true;
                        label3.Visible = false;
                        comboBox3.Visible = false;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            comboBox2.DataSource = db.Countries.Select(x => x.Name).ToList();
                        }
                        break;
                    case 10:
                        label2.Visible = true;
                        comboBox2.Visible = true;
                        label3.Visible = true;
                        comboBox3.Visible = true;
                        using (OlympaidContext db = new OlympaidContext())
                        {
                            comboBox2.DataSource = db.Countries.Select(x => x.Name).ToList();
                            comboBox3.DataSource = db.Olympaids.Select(x => x.Year).ToList();
                        }
                        break;
                    default:
                        dataGridView1.DataSource = null;
                        break;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                using (OlympaidContext db = new OlympaidContext())
                {
                    switch (comboBox1.SelectedIndex)
                    {
                        case 1:
                            dataGridView1.DataSource =
                                db.Query2(new SqlParameter() { ParameterName = "@year", Value = int.Parse(comboBox2.Text) }).ToList();
                            break;
                        case 3:
                            dataGridView1.DataSource =
                                db.Query4(new SqlParameter() { ParameterName = "@year", Value = int.Parse(comboBox2.Text) }).ToList();
                            break;
                        case 5:
                            dataGridView1.DataSource =
                                db.Query6(new SqlParameter() { ParameterName = "@year", Value = int.Parse(comboBox2.Text) }).ToList();
                            break;
                        case 6:
                            dataGridView1.DataSource =
                                db.Query7(new SqlParameter() { ParameterName = "@SportName", Value = comboBox2.Text }).ToList();
                            break;
                        case 8:
                            dataGridView1.DataSource =
                                db.Query9(new SqlParameter() { ParameterName = "@CountryName", Value = comboBox2.Text }).ToList();
                            break;
                        case 9:
                            dataGridView1.DataSource =
                                db.Query10(new SqlParameter() { ParameterName = "@CountryName", Value = comboBox2.Text }).ToList();
                            break;
                        case 10:
                            if (comboBox3.SelectedIndex != -1)
                            {
                                dataGridView1.DataSource =
                                db.Query11(
                                    new SqlParameter() { ParameterName = "@CountryName", Value = comboBox2.Text },
                                    new SqlParameter() { ParameterName = "@year", Value = int.Parse(comboBox3.Text) }
                                    ).ToList();
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {
                using (OlympaidContext db = new OlympaidContext())
                {
                    if (comboBox2.SelectedIndex != -1)
                    {
                        dataGridView1.DataSource =
                           db.Query11(
                               new SqlParameter() { ParameterName = "@CountryName", Value = comboBox2.Text },
                               new SqlParameter() { ParameterName = "@year", Value = int.Parse(comboBox3.Text) }
                               ).ToList();
                    }
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                using (OlympaidContext db = new OlympaidContext())
                {
                    Form2 form2 = new Form2((string)listBox1.SelectedItem);
                    form2.Show();
                }
            }
        }
    }
}

