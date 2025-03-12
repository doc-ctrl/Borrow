using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Borrow.Models;
using Borrow.Repo;

namespace Borrow
{
    public partial class ModifyTable : Form
    {
        public ModifyTable()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;
        }

        private int bookid = 0;
        public void EditRec(Rec rec)
        {
            this.Text = "Update";
            this.LbTitle.Text = "Update";

            this.lbID.Text = "" + rec.id;
            this.tbBook.Text =  rec.Book;
            this.tbGenre.Text =  rec.Genre;
            this.tbAuthor.Text =   rec.Author;
            this.tbMember.Text =  rec.Member;

            this.bookid = rec.id;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Rec rec = new Rec();
            rec.id = this.bookid;
            rec.Book = this.tbBook.Text;
            rec.Genre = this.tbGenre.Text;
            rec.Author = this.tbAuthor.Text;
            rec.Member = this.tbMember.Text;

            var repo = new RecRepo();
            
            if (rec.id == 0)
            {
                repo.CreateRec(rec);
            }
            else
            {
                repo.UpdateRec(rec);
            }
            

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
