using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Final_Project
{
    public static class ContactManager
    {
        public static OleDbDataReader SearchContacts(string fName, string lName, string phoneNumber)
        {
            // Construct the base SQL query
            string query = "SELECT * FROM PhoneBook WHERE 1 = 1"; // 1 = 1 is a trick to simplify dynamic query building

            // Initialize parameters list
            var parameters = new List<OleDbParameter>();

            // Check each parameter and add to the query if provided
            if (!string.IsNullOrEmpty(fName))
            {
                query += " AND FName LIKE ?";
                parameters.Add(new OleDbParameter("@FName", "%" + fName + "%")); // Use % for wildcard search
            }
            if (!string.IsNullOrEmpty(lName))
            {
                query += " AND LName LIKE ?";
                parameters.Add(new OleDbParameter("@LName", "%" + lName + "%"));
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query += " AND PhoneNumber LIKE ?";
                parameters.Add(new OleDbParameter("@PhoneNumber", "%" + phoneNumber + "%"));
            }

            // Create OleDbCommand and execute reader
            OleDbConnection connection = DatabaseHelper.GetConnection();
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddRange(parameters.ToArray());

            connection.Open();
            OleDbDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }
    }
}
