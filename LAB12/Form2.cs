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
    public partial class Form2 : Form
    {
        Academy_EntityEntities db = null;
        int index = -1;
        int changeID = -1;
        int deptID = -1;
        int formID = -1;
        int groupID = -1;
        public Form2(int index_, Academy_EntityEntities db_, int changeID_ = -1)
        {
            db = db_; index = index_; changeID = changeID_;
            switch (index)
            {
                case 0:
                    Label label = new Label() { Text = "Form name: ", Location = new Point(50, 50) };
                    TextBox textBox = new TextBox() { Location = new Point(150, 50), Name = "textBox" };
                    if (changeID != -1) textBox.Text = db.Forms.Where(x => x.Id.Equals(changeID)).Select(x => x.Name).FirstOrDefault();
                    this.Controls.Add(label);
                    this.Controls.Add(textBox);
                    break;
                case 1:
                    Label label1 = new Label() { Text = "Department name: ", Location = new Point(50, 50) };
                    TextBox textBox1 = new TextBox() { Location = new Point(150, 50), Name = "textBox1" };
                    this.Controls.Add(label1);
                    this.Controls.Add(textBox1);
                    break;
                case 2:
                    Label label2 = new Label() { Text = "Group name: ", Location = new Point(50, 50) };
                    TextBox textBox2 = new TextBox() { Location = new Point(150, 50), Name = "textBox2" };
                    Label label3 = new Label() { Text = "Department", Location = new Point(50, 100) };
                    ComboBox cb1 = new ComboBox() { Name = "cb1", Location = new Point(150, 100), DataSource = db.Departments.Select(x => x.Name).ToList() };
                    Label label4 = new Label() { Text = "Form ID: ", Location = new Point(50, 150) };
                    ComboBox cb2 = new ComboBox() { Name = "cb2", Location = new Point(150, 150), DataSource = db.Forms.Select(x => x.Name).ToList() };
                    if (changeID != -1)
                    {
                        textBox2.Text = db.Groups.Where(x => x.Id.Equals(changeID)).Select(x => x.Name).FirstOrDefault();
                        deptID = db.Groups.Where(o => o.Id.Equals(changeID)).Select(o => o.DepartmentsID).FirstOrDefault() - 1;
                        formID = db.Groups.Where(o => o.Id.Equals(changeID)).Select(o => o.FormsID).FirstOrDefault() - 1;
                    }
                    this.Controls.Add(label2);
                    this.Controls.Add(label3);
                    this.Controls.Add(label4);
                    this.Controls.Add(textBox2);
                    this.Controls.Add(cb1);
                    this.Controls.Add(cb2);
                    break;
                case 3:
                    Label label5 = new Label() { Text = "Student name: ", Location = new Point(50, 50) };
                    TextBox textBox3 = new TextBox() { Name = "textBox3", Location = new Point(150, 50) };
                    Label label6 = new Label() { Text = "Student surname: ", Location = new Point(50, 100) };
                    TextBox textBox4 = new TextBox() { Name = "textBox4", Location = new Point(150, 100) };
                    Label label7 = new Label() { Text = "Form ID: ", Location = new Point(50, 150) };
                    ComboBox cb3 = new ComboBox() { Name = "cb3", Location = new Point(150, 150), DataSource = db.Groups.Select(x => x.Name).ToList() };
                    if (changeID != -1)
                    {
                        textBox3.Text = db.Students.Where(x => x.Id.Equals(changeID)).Select(x => x.Name).FirstOrDefault();
                        textBox4.Text = db.Students.Where(x => x.Id.Equals(changeID)).Select(x => x.Surname).FirstOrDefault();
                        groupID = db.Students.Where(o => o.Id.Equals(changeID)).Select(o => o.GroupsID).FirstOrDefault() - 1;
                    }
                    this.Controls.Add(label5);
                    this.Controls.Add(label6);
                    this.Controls.Add(label7);
                    this.Controls.Add(textBox3);
                    this.Controls.Add(textBox4);
                    this.Controls.Add(cb3);
                    break;
            }

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (changeID == -1)
            {
                Add(index);
            }
            else
            {
                Edit(index);
            }
            DialogResult = DialogResult.OK;
        }
        private void Add(int index)
        {
            switch (index)
            {
                case 0:
                    db.Forms.Add(new Forms() { Name = Controls["textBox"].Text });
                    break;
                case 1:
                    db.Departments.Add(new Departments() { Name = Controls["textBox1"].Text });
                    break;
                case 2:
                    string depname = Controls["cb1"].Text;
                    string formname = Controls["cb2"].Text;
                    db.Groups.Add(new Groups()
                    {
                        Name = Controls["textBox2"].Text,
                        DepartmentsID = db.Departments.Where(o => o.Name.Equals(depname)).Select(o => o.Id).FirstOrDefault(),
                        FormsID = db.Forms.Where(o => o.Name.Equals(formname)).Select(o => o.Id).FirstOrDefault()
                    });
                    break;
                case 3:
                    string groupname = Controls["cb3"].Text;
                    db.Students.Add(new Students()
                    {
                        Name = Controls["textBox3"].Text,
                        Surname = Controls["textBox4"].Text,
                        GroupsID = db.Groups.Where(o => o.Name.Equals(groupname)).Select(o => o.Id).FirstOrDefault()
                    });
                    break;
            }
            db.SaveChanges();
        }

        private void Edit(int index)
        {
            switch (index)
            {
                case 0:
                    Forms tmp = db.Forms.Find(changeID);
                    tmp.Name = Controls["textBox"].Text;
                    break;
                case 1:
                    Departments tmp1 = db.Departments.Find(changeID);
                    tmp1.Name = Controls["textBox1"].Text;
                    break;
                case 2:
                    string depname = Controls["cb1"].Text;
                    string formname = Controls["cb2"].Text;
                    Groups tmp3 = db.Groups.Find(changeID);
                    tmp3.Name = Controls["textBox2"].Text;
                    tmp3.DepartmentsID = db.Departments.Where(o => o.Name.Equals(depname)).Select(o => o.Id).FirstOrDefault();
                    tmp3.FormsID = db.Forms.Where(o => o.Name.Equals(formname)).Select(o => o.Id).FirstOrDefault();
                    break;
                case 3:
                    string groupname = Controls["cb3"].Text;
                    Students tmp4 = db.Students.Find(changeID);
                    tmp4.Name = Controls["textBox3"].Text;
                    tmp4.Surname = Controls["textBox4"].Text;
                    tmp4.GroupsID = db.Groups.Where(o => o.Name.Equals(groupname)).Select(o => o.Id).FirstOrDefault();
                    break;
            }
            db.SaveChanges();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if ((Controls["cb1"] as ComboBox) != null) (Controls["cb1"] as ComboBox).SelectedIndex = deptID;
            if ((Controls["cb2"] as ComboBox) != null) (Controls["cb2"] as ComboBox).SelectedIndex = formID;
            if ((Controls["cb3"] as ComboBox) != null) (Controls["cb3"] as ComboBox).SelectedIndex = groupID;
        }
    }
}
