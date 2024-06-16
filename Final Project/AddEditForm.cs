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
    public partial class AddEditForm : Form
    {
        private int recordId;
        private MainForm mainForm;

        public AddEditForm(MainForm mainForm, int recordId = -1)
        {
            InitializeComponent();
            this.recordId = recordId;
            this.mainForm = mainForm;

            if (recordId != -1)
            {
                LoadRecord();
            }
        }

        private void LoadRecord()
        {
            string query = "SELECT FName, LName, PhoneNumber FROM PhoneBook WHERE ID = @ID";
            using (var connection = DatabaseHelper.GetConnection())
            {
                using (var command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", recordId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtFName.Text = reader["FName"].ToString();
                            txtLName.Text = reader["LName"].ToString();
                            txtPhoneNumber.Text = reader["PhoneNumber"].ToString();
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsDuplicate())
            {
                MessageBox.Show("این مخاطب با همین نام، نام خانوادگی و شماره تلفن قبلاً ثبت شده است.", "تکراری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveRecord();
        }

        private bool IsDuplicate()
        {
            string query = "SELECT COUNT(*) FROM PhoneBook WHERE FName = @FName AND LName = @LName AND PhoneNumber = @PhoneNumber";
            OleDbParameter[] parameters =
            {
                new OleDbParameter("@FName", txtFName.Text),
                new OleDbParameter("@LName", txtLName.Text),
                new OleDbParameter("@PhoneNumber", txtPhoneNumber.Text)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            int count = Convert.ToInt32(result);

            return count > 0;
        }

        private void SaveRecord()
        {
            string query;
            OleDbParameter[] parameters;
            if (recordId == -1)
            {
                query = "INSERT INTO PhoneBook (FName, LName, PhoneNumber) VALUES (@FName, @LName, @PhoneNumber)";
                parameters = new OleDbParameter[]
                {
                    new OleDbParameter("@FName", txtFName.Text),
                    new OleDbParameter("@LName", txtLName.Text),
                    new OleDbParameter("@PhoneNumber", txtPhoneNumber.Text)
                };
            }
            else
            {
                query = "UPDATE PhoneBook SET FName = @FName, LName = @LName, PhoneNumber = @PhoneNumber WHERE ID = @ID";
                parameters = new OleDbParameter[]
                {
                    new OleDbParameter("@FName", txtFName.Text),
                    new OleDbParameter("@LName", txtLName.Text),
                    new OleDbParameter("@PhoneNumber", txtPhoneNumber.Text),
                    new OleDbParameter("@ID", recordId)
                };
            }

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            if (rowsAffected > 0)
            {
                MessageBox.Show("اطلاعات با موفقیت ذخیره شد.", "ذخیره موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                mainForm.LoadData();
            }
            else
            {
                MessageBox.Show("ذخیره اطلاعات با شکست مواجه شد.", "خطا در ذخیره", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}