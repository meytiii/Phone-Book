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
    public partial class SearchForm : Form
    {
        private bool isEditMode;
        private int recordId;
        private MainForm mainForm;

        public SearchForm(MainForm mainForm, bool isEditMode = false)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.isEditMode = isEditMode;

            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ID", "شناسه");
            dataGridView1.Columns.Add("FName", "نام");
            dataGridView1.Columns.Add("LName", "نام خانوادگی");
            dataGridView1.Columns.Add("PhoneNumber", "شماره تلفن");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT ID, FName, LName, PhoneNumber FROM PhoneBook WHERE 1=1";

            if (!string.IsNullOrEmpty(txtFName.Text))
            {
                query += " AND FName LIKE @FName";
            }

            if (!string.IsNullOrEmpty(txtLName.Text))
            {
                query += " AND LName LIKE @LName";
            }

            if (!string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                query += " AND PhoneNumber LIKE @PhoneNumber";
            }

            using (var connection = DatabaseHelper.GetConnection())
            {
                using (var command = new OleDbCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(txtFName.Text))
                    {
                        command.Parameters.AddWithValue("@FName", "%" + txtFName.Text + "%");
                    }

                    if (!string.IsNullOrEmpty(txtLName.Text))
                    {
                        command.Parameters.AddWithValue("@LName", "%" + txtLName.Text + "%");
                    }

                    if (!string.IsNullOrEmpty(txtPhoneNumber.Text))
                    {
                        command.Parameters.AddWithValue("@PhoneNumber", "%" + txtPhoneNumber.Text + "%");
                    }

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string fName = reader.GetString(1);
                            string lName = reader.GetString(2);
                            string phoneNumber = reader.GetString(3);

                            dataGridView1.Rows.Add(id, fName, lName, phoneNumber);
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                recordId = Convert.ToInt32(row.Cells["ID"].Value);
                txtFName.Text = row.Cells["FName"].Value.ToString();
                txtLName.Text = row.Cells["LName"].Value.ToString();
                txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
            }
        }

        private void btnEditDelete_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                EditRecord();
            }
            else
            {
                DeleteRecord();
            }
        }

        private void EditRecord()
        {
            if (recordId != 0)
            {
                var addEditForm = new AddEditForm(mainForm, recordId);
                addEditForm.ShowDialog();
                btnSearch_Click(null, null);
            }
            else
            {
                MessageBox.Show("لطفاً یک رکورد را انتخاب کنید.", "ویرایش", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteRecord()
        {
            if (recordId != 0)
            {
                DialogResult result = MessageBox.Show("آیا مطمئن هستید که می‌خواهید این رکورد را حذف کنید؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM PhoneBook WHERE ID = @ID";
                    OleDbParameter[] parameters =
                    {
                        new OleDbParameter("@ID", recordId)
                    };

                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("رکورد با موفقیت حذف شد.", "حذف موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSearch_Click(null, null);
                        mainForm.LoadData();
                    }
                    else
                    {
                        MessageBox.Show("حذف رکورد با شکست مواجه شد.", "خطا در حذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("لطفاً یک رکورد را انتخاب کنید.", "حذف", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}