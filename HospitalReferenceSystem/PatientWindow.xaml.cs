using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalReferenceSystem
{
    /// <summary>
    /// Логика взаимодействия для PatienceWindow.xaml
    /// </summary>
    public partial class PatienceWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=HospitalReferenceSystemDataBase;Integrated Security=True";

        public PatienceWindow(int id)
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
