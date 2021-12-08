using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            private set => instance = value; 
        }

        private DataProvider() { }

        private string connectionSTR = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Application.StartupPath + @"\Data.mdf;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    AddParametersToCommand(command, query, parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameters = null)
        {
            int row = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    AddParametersToCommand(command, query, parameters);
                }

                row = command.ExecuteNonQuery();

                connection.Close();
            }

            return row;
        }

        public object ExecuteScalar(string query, object[] parameters = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    AddParametersToCommand(command, query, parameters);
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }

        public void AddParametersToCommand(SqlCommand command, string query, object[] parameters)
        {
            string[] listParams = query.Split(' ');

            int i = 0;
            foreach (string item in listParams)
            {
                if (item.Contains('@'))
                {
                    string para = item.Replace(",", "");
                    command.Parameters.AddWithValue(para, parameters[i]);
                    ++i;
                }
            }
        }
        
    }
}
