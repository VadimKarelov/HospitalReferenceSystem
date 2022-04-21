using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=HospitalReferenceSystemDataBase;Integrated Security=True";

        public DoctorWindow(int doctorId)
        {
            InitializeComponent();

            GetUserInformation(doctorId);

            listBox_Journal.Items.Clear();
            listBox_Procedures.Items.Clear();

            
        }

        private void GetUserInformation(int doctorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT FIO, Position FROM Doctors WHERE ID = {doctorId}", connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                textBlock_FIO.Text = $"ФИО: {reader.GetString(0)}, {reader.GetString(1)}";

                reader.Close();
            }
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
    }
}
