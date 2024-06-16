using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace Final_Project
{
    public static class DatabaseHelper
    {
        private static string DatabaseFileName = "PhoneBookDB.mdb";
        private static string ConnectionString
        {
            get
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string dbFilePath = Path.Combine(baseDirectory, DatabaseFileName);
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFilePath;
            }
        }
        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(ConnectionString);
        }

        public static int ExecuteNonQuery(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection connection = GetConnection())
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection connection = GetConnection())
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        public static OleDbDataReader ExecuteReader(string query, params OleDbParameter[] parameters)
        {
            OleDbConnection connection = GetConnection();
            OleDbCommand command = new OleDbCommand(query, connection);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            connection.Open();
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
    }
}