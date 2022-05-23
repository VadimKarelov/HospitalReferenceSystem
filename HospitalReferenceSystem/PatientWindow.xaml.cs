using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace HospitalReferenceSystem
{
    /// <summary>
    /// Логика взаимодействия для PatienceWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=HospitalReferenceSystemDataBase;Integrated Security=True;User ID=patient;Password='1'";

        public PatientWindow(int id)
        {
            InitializeComponent();

            GetUserInformation(id);

            UpdateHistory(id);
        }

        private void UpdateHistory(int id)
        {
            listBox_Journal.Items.Clear();

            List<string> his = Query.GetSickHistory(id);

            for (int i = 0; i < his.Count; i++)
            {
                listBox_Journal.Items.Add(his[i]);
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void GetUserInformation (int sickId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT FIO, DiagnosisID FROM Sick WHERE ID = {sickId}", connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                textBlock_FIO.Text = $"ФИО: {reader.GetString(0)}, {Query.GetDiagnoseNameByID(reader.GetInt32(1))}";

                reader.Close();
            }
        }
    }
}
