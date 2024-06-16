using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Final_Project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            string query = "SELECT ID AS شناسه, FName AS نام, LName AS نام_خانوادگی, PhoneNumber AS شماره_تلفن FROM PhoneBook";
            using (var connection = DatabaseHelper.GetConnection())
            {
                using (var adapter = new OleDbDataAdapter(query, connection))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenAddEditForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OpenSearchForm(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OpenSearchForm(false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OpenSearchForm(false);
        }

        private void OpenAddEditForm(int recordId = -1)
        {
            var addEditForm = new AddEditForm(this, recordId);
            addEditForm.ShowDialog();
            LoadData();
        }

        private void OpenSearchForm(bool isEditMode)
        {
            var searchForm = new SearchForm(this, isEditMode);
            searchForm.ShowDialog();
            LoadData();
        }
    }
}