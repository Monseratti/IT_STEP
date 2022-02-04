using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                using(LibraryContext db = new LibraryContext())
                {
                    List<string> list = db.Users.Where(x=>x.isDebtor == true).Select(o=>o.Name).ToList();
                    listBox1.DataSource = list;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                using (LibraryContext db = new LibraryContext())
                {
                    List<string> authorName = new List<string>();
                    List<int> authorId = db.AuthorsBooks.Where(x => x.BookId.Equals(3)).Select(o => o.AuthorId).ToList();
                    foreach (var item in authorId)
                    {
                        authorName.Add(db.Author.Where(x => x.Id.Equals(item)).Select(o => o.Name).FirstOrDefault());
                    }
                    listBox1.DataSource = authorName;
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                using (LibraryContext db = new LibraryContext())
                {
                    List<string> BookName = new List<string>();
                    List<int> BookId = db.UsersBooks.Select(o => o.BookId).Distinct().ToList();
                    List<string> BookAll = db.Books.Select(o => o.Title).ToList();
                    foreach (var item in BookId)
                    {
                        BookName.Add(db.Books.Where(x => x.Id.Equals(item)).Select(o => o.Title).FirstOrDefault());
                    } 
                    listBox1.DataSource = BookAll.Except(BookName).ToList(); 
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            if ((sender as RadioButton).Checked)
            {
                using (LibraryContext db = new LibraryContext())
                {
                    List<string> BookName = new List<string>();
                    List<int> BookId = db.UsersBooks.Where(x=>x.UserId.Equals(2)).Select(o => o.BookId).Distinct().ToList();
                    foreach (var item in BookId)
                    {
                        BookName.Add(db.Books.Where(x => x.Id.Equals(item)).Select(o => o.Title).FirstOrDefault());
                    }
                    listBox1.DataSource = BookName;
                }
            }
        }
    }
}
