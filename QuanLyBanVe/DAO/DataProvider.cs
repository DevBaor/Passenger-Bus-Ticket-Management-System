using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DAO
{
    internal class DataProvider
    {
        private static DataProvider instance;
        internal static DataProvider Instance { get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; } 
            private set { DataProvider.instance = value; }
        } private string connectionstring = "Data Source=LAPTOP-RHPTALF1\\SQLEXPRESS;Initial Catalog=QLBV;Integrated Security=True;Encrypt=False";
       
        private DataProvider() { }
        public DataTable ExcuteQuery(string query, object[] parameter= null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionstring)) {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                { 
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string s in listpara)
                    {
                        if (s.Contains('@'))
                        {
                            command.Parameters.AddWithValue(s, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
           return data;
        }
        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string s in listpara)
                    {
                        if (s.Contains('@'))
                        {
                            command.Parameters.AddWithValue(s, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }
        public object ExcuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string s in listpara)
                    {
                        if (s.Contains('@'))
                        {
                            command.Parameters.AddWithValue(s, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
    }
}

