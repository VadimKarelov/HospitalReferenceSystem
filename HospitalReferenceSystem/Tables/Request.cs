using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReferenceSystem.Tables
{
    internal class Request
    {
        public static string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static async Task<DataSet> RequestAsync(string sqlExpression)
        {
            return await new Task<DataSet>(() => DoRequest(sqlExpression));
        }

        private static DataSet DoRequest(string sqlExpression)
        {
            DataSet ds = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                
                adapter.Fill(ds);
            }

            return ds;
        }
    }
}
