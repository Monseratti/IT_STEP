using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB12
{
    public partial class Form1 : Form
    {
        Academy_EntityEntities db = new Academy_EntityEntities();
        DataGridViewRow selectedrow = null;
        public Form1()
        {
            InitializeComponent();
            tabPage1.Text = "Forms";
            tabPage2.Text = "Departments";
            tabPage3.Text = "Groups";
            tabPage4.Text = "Students";
            UpdateData();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                Form2 form = new Form2(tabControl1.SelectedIndex, db);
                form.ShowDialog();
                UpdateData();
            }
        }
        private void UpdateData()
        {
            dataGridView1.DataSource = db.Forms.Select(x => new {x.Id, x.Name }).ToList();
            dataGridView2.DataSource = db.Departments.Select(x => new {x.Id, x.Name }).ToList();
            dataGridView3.DataSource = db.Groups.Select(x => new {x.Id, x.Name, x.DepartmentsID, x.FormsID }).ToList();
            dataGridView4.DataSource = db.Students.Select(x => new {x.Id, x.Name, x.Surname, x.GroupsID }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(tabControl1.SelectedIndex, db, SelectedID());
            form.ShowDialog();
            UpdateData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).SelectedRows.Count !=0)
            {
                selectedrow = (sender as DataGridView).SelectedRows[0];
            }
            else if ((sender as DataGridView).SelectedCells.Count !=0)
            {
                selectedrow = (sender as DataGridView).SelectedCells[0].OwningRow;
            }
            else
            {
                selectedrow = null;
            }
        }

        private int SelectedID()
        {
            return (int)selectedrow.Cells[0].Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = SelectedID();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (db.Groups.Where(o => o.FormsID.Equals(id)).Count() != 0)
                    {
                        MessageBox.Show("This form use. Did not deleted"); return;
                    }
                    else
                    {
                        Forms del = db.Forms.Find(id);
                        db.Forms.Remove(del);
                    }
                    break;
                case 1:
                    if (db.Groups.Where(o => o.DepartmentsID.Equals(id)).Count() != 0)
                    {
                        MessageBox.Show("This department use. Did not deleted"); return;
                    }
                    else
                    {
                        Departments del1 = db.Departments.Find(id);
                        db.Departments.Remove(del1);
                    }
                    break;
                case 2:
                    if (db.Students.Where(o=>o.GroupsID.Equals(id)).Count()!=0)
                    {
                        MessageBox.Show("This group use. Did not deleted"); return;
                    }
                    else
                    {
                        Groups del2 = db.Groups.Find(id);
                        db.Groups.Remove(del2);
                    }
                    break;
                case 3:
                    Students del3 = db.Students.Find(id);
                    db.Students.Remove(del3);
                    break;
            }
            db.SaveChanges();
            UpdateData();
        }
    }
}
