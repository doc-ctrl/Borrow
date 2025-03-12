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
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Tls;

namespace Borrow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ReadRec();
        }

        private void ReadRec()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("BOOK");
            dataTable.Columns.Add("GENRE");
            dataTable.Columns.Add("AUTHOR");
            dataTable.Columns.Add("MEMBER");
           

            var repo = new RecRepo();
            var recs = repo.GetRecs();

            foreach (var rec in recs)
            {

                var row = dataTable.NewRow();

                row["ID"] = rec.id;
                row["BOOK"] = rec.Book;
                row["GENRE"] = rec.Genre;
                row["AUTHOR"] = rec.Author;
                row["MEMBER"] = rec.Member;
                

                dataTable.Rows.Add(row);
            }

            this.RecTable.DataSource = dataTable;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ModifyTable form = new ModifyTable();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadRec();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var val = this.RecTable.SelectedRows[0].Cells[0].Value.ToString();
            if (val == null || val.Length == 0) 
            return;

            int bookid = int.Parse(val);

            var repo = new RecRepo();
            var rec = repo.GetRec(bookid);
            
            if(rec == null)
            return;

            ModifyTable Form = new ModifyTable();
            Form.EditRec(rec);
            if (Form.ShowDialog() == DialogResult.OK)
            {
                ReadRec();               
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
{
    if (this.RecTable.SelectedRows.Count == 0) // ✅ Check if a row is selected
    {
        MessageBox.Show("Please select a record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    var val = this.RecTable.SelectedRows[0].Cells[0].Value?.ToString(); // ✅ Use safe null handling

    if (string.IsNullOrEmpty(val)) return; // ✅ Prevent null exceptions

    if (!int.TryParse(val, out int bookid)) return; // ✅ Use TryParse to prevent crashes

    DialogResult dialogResult = MessageBox.Show("Want to delete this Record?", "Delete Record", MessageBoxButtons.YesNo);

    if (dialogResult == DialogResult.No)
    { 
        return; 
    }

    var repo = new RecRepo();
    repo.DeleteRec(bookid); // ✅ Ensure DeleteRec(bookid) is properly implemented

    ReadRec(); // ✅ Refresh the table after deletion


        }
    }
}
