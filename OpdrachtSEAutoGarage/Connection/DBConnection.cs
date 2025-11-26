using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;

namespace OpdrachtSEAutoGarage.Connection
{
    public class DBConnection
    {
        private SqlConnection connection;
        private readonly string connectionString = "Data Source=KobersKirsch;Initial Catalog=AutoGarage;Integrated Security=True;Trust Server Certificate=True";

        public DBConnection()
        {
            connection = new SqlConnection(connectionString);
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }
        public void Open()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
        public void Close()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
