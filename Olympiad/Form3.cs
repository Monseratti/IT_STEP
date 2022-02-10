using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Olympiad.Classes;

namespace Olympiad
{
    public partial class Form3 : Form
    {
        OlympaidContext db = new OlympaidContext();
        int index = -1; int changeID = -1;
        int countryID = -1; int spmenID = -1;
        int OlSpID = -1; int olID = -1; int sportId = -1;
        byte[] bytes = null;
        public Form3(int index_, int changeID_ = -1)
        {
            index = index_; changeID = changeID_;
            switch (index)
            {
                case 0:
                    Label label = new Label() { Text = "Country name: ", Location = new Point(50, 25) };
                    TextBox textBox = new TextBox() { Location = new Point(150, 25), Name = "textBox" };
                    if (changeID != -1) textBox.Text = db.Countries.Where(x => x.Id.Equals(changeID)).Select(x => x.Name).FirstOrDefault();
                    this.Controls.Add(label);
                    this.Controls.Add(textBox);
                    break;
                case 1:
                    Label label1 = new Label() { Text = "Sport name: ", Location = new Point(50, 25) };
                    TextBox textBox1 = new TextBox() { Location = new Point(150, 25), Name = "textBox1" };
                    if (changeID != -1) textBox1.Text = db.Sports.Where(x => x.Id.Equals(changeID)).Select(x => x.Name).FirstOrDefault();
                    this.Controls.Add(label1);
                    this.Controls.Add(textBox1);
                    break;
                case 2:
                    Label label2 = new Label() { Text = "Sportsmen name: ", Location = new Point(50, 25) };
                    TextBox textBox2 = new TextBox() { Location = new Point(150, 25), Name = "textBox2" };
                    Label label3 = new Label() { Text = "Country", Location = new Point(50, 50) };
                    ComboBox cb1 = new ComboBox() { Name = "cb1", Location = new Point(150, 50), DataSource = db.Countries.Select(x => x.Name).ToList() };
                    Label label4 = new Label() { Text = "Birthday: ", Location = new Point(50, 75) };
                    DateTimePicker dtp = new DateTimePicker() { Name = "dtp", Location = new Point(150, 75) };
                    Label label5 = new Label() { Text = "Photo ", Location = new Point(50, 100) };
                    TextBox textBox3 = new TextBox() { Location = new Point(150, 100), Name = "textBox3", Width = 200 };
                    Button btn1 = new Button() { Name = "btn1", Location = new Point(350, 100), Width = 35, Height = 20, Text = "..." };
                    PictureBox pictureBox1 = new PictureBox() { Name = "pbx", Location = new Point(50, 150), Width = 200, Height = 100 };
                    btn1.Click += btn1_Click;
                    if (changeID != -1)
                    {
                        textBox2.Text = db.Sportsmens.Where(x => x.Id.Equals(changeID)).Select(x => x.Name).FirstOrDefault();
                        countryID = (int)db.Sportsmens.Where(o => o.Id.Equals(changeID)).Select(o => o.CountryId).FirstOrDefault() - 1;
                        dtp.Value = db.Sportsmens.Where(o => o.Id.Equals(changeID)).Select(o => o.BirthDay).FirstOrDefault();
                        var buff = db.Sportsmens.Where(o => o.Id.Equals(changeID)).Select(o => o.Photo).FirstOrDefault();
                        if (buff != null)
                        {
                            MemoryStream ms = new MemoryStream(buff);
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    this.Controls.Add(label2);
                    this.Controls.Add(label3);
                    this.Controls.Add(label4);
                    this.Controls.Add(label5);
                    this.Controls.Add(textBox2);
                    this.Controls.Add(textBox3);
                    this.Controls.Add(cb1);
                    this.Controls.Add(dtp);
                    this.Controls.Add(btn1);
                    this.Controls.Add(pictureBox1);
                    break;
                case 3:
                    Label label6 = new Label() { Text = "Year: ", Location = new Point(50, 25) };
                    NumericUpDown nud = new NumericUpDown() { Name = "nud", Location = new Point(150, 25), Minimum = 1900, Maximum = 9999 };
                    Label label7 = new Label() { Text = "Country: ", Location = new Point(50, 50) };
                    ComboBox cb2 = new ComboBox() { Name = "cb2", Location = new Point(150, 50), DataSource = db.Countries.Select(x => x.Name).ToList() };
                    Label label8 = new Label() { Text = "Parent city: ", Location = new Point(50, 75) };
                    TextBox textBox4 = new TextBox() { Name = "textBox4", Location = new Point(150, 75) };
                    CheckBox cbx = new CheckBox() { Name = "cbx", Text = "Is winter Game", Location = new Point(50, 100), Checked = false };
                    if (changeID != -1)
                    {
                        nud.Value = db.Olympaids.Where(x => x.Id.Equals(changeID)).Select(x => x.Year).FirstOrDefault();
                        textBox4.Text = db.Olympaids.Where(x => x.Id.Equals(changeID)).Select(x => x.CityName).FirstOrDefault();
                        countryID = (int)db.Olympaids.Where(o => o.Id.Equals(changeID)).Select(o => o.ParentCountryId).FirstOrDefault() - 1;
                        cbx.Checked = db.Olympaids.Where(o => o.Id.Equals(changeID)).Select(o => o.IsWinter).FirstOrDefault();
                    }
                    this.Controls.Add(label6);
                    this.Controls.Add(label7);
                    this.Controls.Add(label8);
                    this.Controls.Add(textBox4);
                    this.Controls.Add(nud);
                    this.Controls.Add(cb2);
                    this.Controls.Add(cbx);
                    break;
                case 4:
                    Label label9 = new Label() { Text = "Olympaid-Sport: ", Location = new Point(50, 25) };
                    List<string> map = new List<string>();
                    foreach (var item in db.OlympaidSports.Select(o => o.Id).ToList())
                    {
                        string tmp = "";
                        int OlID = db.OlympaidSports.Where(o => o.Id.Equals(item)).Select(o => o.OlympaidId).FirstOrDefault();
                        int SpID = db.OlympaidSports.Where(o => o.Id.Equals(item)).Select(o => o.SportsId).FirstOrDefault();
                        tmp += db.Olympaids.Where(o => o.Id.Equals(OlID)).Select(o => o.Year).FirstOrDefault().ToString();
                        tmp += "-";
                        tmp += db.Sports.Where(o => o.Id.Equals(SpID)).Select(o => o.Name).FirstOrDefault().ToString();
                        map.Add(tmp);
                    }
                    ComboBox cb3 = new ComboBox() { Name = "cb3", Location = new Point(150, 25), DataSource = map };
                    Label label10 = new Label() { Text = "Sportsmen: ", Location = new Point(50, 50) };
                    ComboBox cb4 = new ComboBox() { Name = "cb4", Location = new Point(150, 50), DataSource = db.Sportsmens.Select(o => o.Name).ToList() };
                    Label label11 = new Label() { Text = "Medals: Gold, Silver, Bronze ", Location = new Point(50, 75), Width = 200 };
                    NumericUpDown nud1 = new NumericUpDown() { Name = "nud1", Location = new Point(50, 100), Width = 50 };
                    NumericUpDown nud2 = new NumericUpDown() { Name = "nud2", Location = new Point(150, 100), Width = 50 };
                    NumericUpDown nud3 = new NumericUpDown() { Name = "nud3", Location = new Point(250, 100), Width = 50 };
                    if (changeID != -1)
                    {
                        nud1.Value = db.OlympaidResults.Where(x => x.Id.Equals(changeID)).Select(x => x.GoldenMedal).FirstOrDefault();
                        nud2.Value = db.OlympaidResults.Where(x => x.Id.Equals(changeID)).Select(x => x.SilverMedal).FirstOrDefault();
                        nud3.Value = db.OlympaidResults.Where(x => x.Id.Equals(changeID)).Select(x => x.BronzeMedal).FirstOrDefault();
                        spmenID = db.OlympaidResults.Where(x => x.Id.Equals(changeID)).Select(x => x.SportsmenId).FirstOrDefault()-1;
                        OlSpID = db.OlympaidResults.Where(o => o.Id.Equals(changeID)).Select(o => o.OlSpId).FirstOrDefault() - 1;
                    }
                    this.Controls.Add(label9);
                    this.Controls.Add(label10);
                    this.Controls.Add(label11);
                    this.Controls.Add(cb3);
                    this.Controls.Add(cb4);
                    this.Controls.Add(nud1);
                    this.Controls.Add(nud2);
                    this.Controls.Add(nud3);
                    break;
                case 5:
                    Label label12 = new Label() { Text = "Olympiad: ", Location = new Point(50, 25) };
                    ComboBox cb5 = new ComboBox() { Name = "cb5", Location = new Point(150, 25), DataSource = db.Olympaids.Select(x => x.Year).ToList() };
                    Label label13 = new Label() { Text = "Sport: ", Location = new Point(50, 50) };
                    ComboBox cb6 = new ComboBox() { Name = "cb6", Location = new Point(150, 50), DataSource = db.Sports.Select(x => x.Name).ToList() };
                    if (changeID != -1)
                    {
                        olID = db.OlympaidSports.Where(x => x.Id.Equals(changeID)).Select(x => x.OlympaidId).FirstOrDefault()-1;
                        sportId = db.OlympaidSports.Where(x => x.Id.Equals(changeID)).Select(x => x.SportsId).FirstOrDefault()-1;
                    }
                    this.Controls.Add(label12);
                    this.Controls.Add(label13);
                    this.Controls.Add(cb5);
                    this.Controls.Add(cb6);
                    break;
            }
            InitializeComponent();
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Controls["textBox3"].Text = dlg.FileName;
                bytes = CreateCopy();
            }
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
                    db.Countries.Add(new Country() { Name = Controls["textBox"].Text });
                    break;
                case 1:
                    db.Sports.Add(new Sports() { Name = Controls["textBox1"].Text });
                    break;
                case 2:
                    string countryname = Controls["cb1"].Text;
                    db.Sportsmens.Add(new Sportsmen()
                    {
                        Name = Controls["textBox2"].Text,
                        CountryId = db.Countries.Where(o => o.Name.Equals(countryname)).Select(o => o.Id).FirstOrDefault(),
                        BirthDay = (Controls["dtp"] as DateTimePicker).Value.Date,
                        Photo = bytes
                    });
                    break;
                case 3:
                    string countryname1 = Controls["cb2"].Text;
                    db.Olympaids.Add(new Olympaid()
                    {
                        Year = (short)(Controls["nud"] as NumericUpDown).Value,
                        ParentCountryId = db.Countries.Where(o => o.Name.Equals(countryname1)).Select(o => o.Id).FirstOrDefault(),
                        CityName = Controls["textBox4"].Text,
                        IsWinter = (Controls["cbx"] as CheckBox).Checked
                    });
                    break;
                case 4:
                    string spmenname = Controls["cb4"].Text;
                    string olspname = Controls["cb3"].Text;
                    string[] ol = olspname.Split('-');
                    short year = short.Parse(ol[0]);
                    string sp = ol[1];
                    int OlId = db.Olympaids.Where(o => o.Year.Equals(year)).Select(o => o.Id).FirstOrDefault();
                    int spId = db.Sports.Where(o => o.Name.Equals(sp)).Select(o => o.Id).FirstOrDefault();
                    db.OlympaidResults.Add(new OlympaidResults()
                    {
                        SportsmenId = db.Sportsmens.Where(o => o.Name.Equals(spmenname)).Select(o => o.Id).FirstOrDefault(),
                        GoldenMedal = (int)(Controls["nud1"] as NumericUpDown).Value,
                        SilverMedal = (int)(Controls["nud2"] as NumericUpDown).Value,
                        BronzeMedal = (int)(Controls["nud3"] as NumericUpDown).Value,
                        OlSpId = db.OlympaidSports.Where(o => o.OlympaidId.Equals(OlId) && o.SportsId.Equals(spId)).Select(o => o.Id).FirstOrDefault()
                    });
                    break;
                case 5:
                    short year1 = short.Parse(Controls["cb5"].Text);
                    string spname = Controls["cb6"].Text;
                    db.OlympaidSports.Add(new OlympaidSports()
                    {
                        OlympaidId = db.Olympaids.Where(o => o.Year.Equals(year1)).Select(o => o.Id).FirstOrDefault(),
                        SportsId = db.Sports.Where(o => o.Name.Equals(spname)).Select(o => o.Id).FirstOrDefault()
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
                    db.Countries.Find(changeID).Name = Controls["textBox"].Text;
                    break;
                case 1:
                    db.Sports.Find(changeID).Name = Controls["textBox1"].Text;
                    break;
                case 2:
                    Sportsmen sportsmen = db.Sportsmens.Find(changeID);
                    string countryname = Controls["cb1"].Text;
                    sportsmen.Name = Controls["textBox2"].Text;
                    sportsmen.CountryId = db.Countries.Where(o => o.Name.Equals(countryname)).Select(o => o.Id).FirstOrDefault();
                    sportsmen.BirthDay = (Controls["dtp"] as DateTimePicker).Value.Date;
                    sportsmen.Photo = bytes;
                    break;
                case 3:
                    Olympaid olympaid = db.Olympaids.Find(changeID);
                    string countryname1 = Controls["cb2"].Text;
                    olympaid.Year = (short)(Controls["nud"] as NumericUpDown).Value;
                    olympaid.ParentCountryId = db.Countries.Where(o => o.Name.Equals(countryname1)).Select(o => o.Id).FirstOrDefault();
                    olympaid.CityName = Controls["textBox4"].Text;
                    olympaid.IsWinter = (Controls["cbx"] as CheckBox).Checked;
                    break;
                case 4:
                    OlympaidResults results = db.OlympaidResults.Find(changeID);
                    string spmenname = Controls["cb4"].Text;
                    string olspname = Controls["cb3"].Text;
                    string[] ol = olspname.Split('-');
                    short year = short.Parse(ol[0]);
                    string sp = ol[1];
                    int OlId = db.Olympaids.Where(o => o.Year.Equals(year)).Select(o => o.Id).FirstOrDefault();
                    int spId = db.Sports.Where(o => o.Name.Equals(sp)).Select(o => o.Id).FirstOrDefault();
                    results.SportsmenId = db.Sportsmens.Where(o => o.Name.Equals(spmenname)).Select(o => o.Id).FirstOrDefault();
                    results.GoldenMedal = (int)(Controls["nud1"] as NumericUpDown).Value;
                    results.SilverMedal = (int)(Controls["nud2"] as NumericUpDown).Value;
                    results.BronzeMedal = (int)(Controls["nud3"] as NumericUpDown).Value;
                    results.OlSpId = db.OlympaidSports.Where(o => o.OlympaidId.Equals(OlId) && o.SportsId.Equals(spId)).Select(o => o.Id).FirstOrDefault();
                    break;
                case 5:
                    short year1 = short.Parse(Controls["cb5"].Text);
                    string spname = Controls["cb6"].Text;
                    db.OlympaidSports.Add(new OlympaidSports()
                    {
                        OlympaidId = db.Olympaids.Where(o => o.Year.Equals(year1)).Select(o => o.Id).FirstOrDefault(),
                        SportsId = db.Sports.Where(o => o.Name.Equals(spname)).Select(o => o.Id).FirstOrDefault()
                    });
                    break;
            }
            db.SaveChanges();
        }
        private byte[] CreateCopy()
        {
            Image img = Image.FromFile(Controls["textBox3"].Text);
            int maxWidth = 200, maxHeight = 100;
            //размеры выбраны произвольно
            double ratioX = (double)maxWidth / img.Width;
            double ratioY = (double)maxHeight / img.Height;
            double ratio = Math.Min(ratioX, ratioY);
            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);
            Image mi = new Bitmap(newWidth, newHeight);
            (Controls["pbx"] as PictureBox).Image = mi;
            //рисунок в памяти
            Graphics g = Graphics.FromImage(mi);
            g.DrawImage(img, 0, 0, newWidth, newHeight);
            MemoryStream ms = new MemoryStream();
            //поток для ввода|вывода байт из памяти
            mi.Save(ms, ImageFormat.Jpeg);
            ms.Flush();//выносим в поток все данные
                       //из буфера
            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            byte[] buf = br.ReadBytes((int)ms.Length);
            return buf;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if ((Controls["cb1"] as ComboBox) != null) (Controls["cb1"] as ComboBox).SelectedIndex = countryID;
            if ((Controls["cb2"] as ComboBox) != null) (Controls["cb2"] as ComboBox).SelectedIndex = countryID;
            if ((Controls["cb3"] as ComboBox) != null) (Controls["cb3"] as ComboBox).SelectedIndex = OlSpID;
            if ((Controls["cb4"] as ComboBox) != null) (Controls["cb4"] as ComboBox).SelectedIndex = spmenID;
            if ((Controls["cb5"] as ComboBox) != null) (Controls["cb5"] as ComboBox).SelectedIndex = olID;
            if ((Controls["cb6"] as ComboBox) != null) (Controls["cb6"] as ComboBox).SelectedIndex = sportId;
        }
    }
}
