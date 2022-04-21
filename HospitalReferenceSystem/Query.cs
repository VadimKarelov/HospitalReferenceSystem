using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReferenceSystem
{
    static class Query
    {
        private static string connectionString = @"Data Source=localhost;Initial Catalog=HospitalReferenceSystemDataBase;Integrated Security=True";

        public static List<string> GetSickHistory(int sickID)
        {
            List<string> list = new List<string>();

            GetProceduresHistory(sickID, ref list);
            GetDiagnosesHistory(sickID, ref list);
            GetWardHistory(sickID, ref list);

            list.Sort();

            return list;
        }

        private static void GetProceduresHistory(int sickID, ref List<string> his)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string t = "SELECT DateTime, ProcedureID, Status FROM ProceduresJournal " +
                    $"WHERE SickID = {sickID}";
                SqlCommand command = new SqlCommand(t, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        his.Add($"{reader.GetDateTime(0)} - {reader.GetString(2)} {GetProcedureNameByID(reader.GetInt32(1))}");
                    }
                }

                reader.Close();
            }
        }

        private static void GetDiagnosesHistory(int sickID, ref List<string> his)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string t = "SELECT DateTime, DoctorID, DiagnoseID, Status FROM DiagnosesJournal " +
                    $"WHERE SickID = {sickID}";
                SqlCommand command = new SqlCommand(t, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        his.Add($"{reader.GetDateTime(0)} - {reader.GetString(3)} диагноз {GetDiagnoseNameByID(reader.GetInt32(2))}, врач {GetDoctorNameByID(reader.GetInt32(1))}");
                    }
                }

                reader.Close();
            }
        }

        private static void GetWardHistory(int sickID, ref List<string> his)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string t = "SELECT DateTime, WardNumber, Event FROM WardJournal " +
                    $"WHERE SickID = {sickID}";
                SqlCommand command = new SqlCommand(t, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        his.Add($"{reader.GetDateTime(0)} - {reader.GetString(2)} {reader.GetInt32(1)}");
                    }
                }

                reader.Close();
            }
        }

        public static string GetProcedureNameByID(int id)
        {
            string res = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string t = "SELECT ID, Name FROM ProceduresAndExaminations";
                SqlCommand command = new SqlCommand(t, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) == id)
                        {
                            res = reader.GetString(1);
                            break;
                        }
                    }
                }

                reader.Close();
            }
            return res;
        }

        public static string GetDiagnoseNameByID(int id)
        {
            string res = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string t = "SELECT ID, Name FROM Diagnoses";
                SqlCommand command = new SqlCommand(t, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) == id)
                        {
                            res = reader.GetString(1);
                            break;
                        }
                    }
                }

                reader.Close();
            }
            return res;
        }

        public static string GetDoctorNameByID(int id)
        {
            string res = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string t = "SELECT ID, FIO FROM Doctors";
                SqlCommand command = new SqlCommand(t, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) == id)
                        {
                            res = reader.GetString(1);
                            break;
                        }
                    }
                }

                reader.Close();
            }
            return res;
        }

        public static string GetSickNameByID(int id)
        {
            string res = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string t = "SELECT ID, FIO FROM Sick";
                SqlCommand command = new SqlCommand(t, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32(0) == id)
                        {
                            res = reader.GetString(1);
                            break;
                        }
                    }
                }

                reader.Close();
            }
            return res;
        }
    }
}
