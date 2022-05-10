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
using System.Numerics;

namespace HospitalReferenceSystem
{
    /// <summary>
    /// Логика взаимодействия для AddNewPatientWindow.xaml
    /// </summary>
    public partial class AddNewPatientWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=HospitalReferenceSystemDataBase;Integrated Security=True";

        private List<Diagnose> diagnoses = new List<Diagnose>();

        [System.ComponentModel.TypeConverter(typeof(System.Windows.DialogResultConverter))]
        public bool? DialogResult { get; set; }

        public string FIO { get; set; }
        public int DiagnosesID { get; set; }
        public int WardNumber { get; set; }

        public AddNewPatientWindow()
        {
            InitializeComponent();
            LoadDiagnoses();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_FIO.Text.Replace(" ", "") != "" && int.TryParse(tb_WardN.Text, out int wN) && comboBox_Diagnoses.SelectedIndex != -1)
            {
                FIO = tb_FIO.Text;
                WardNumber = wN;
                DiagnosesID = diagnoses.First(x => x.Name == comboBox_Diagnoses.SelectedItem.ToString()).ID;
                DialogResult = true;
                this.Close();
            }
            else
            {
                if (tb_FIO.Text.Replace(" ", "") == "")
                    MessageBox.Show("Не введено ФИО!");

                else if (!int.TryParse(tb_WardN.Text, out int t))
                    MessageBox.Show("Не введен номер палаты или введен неправильно!");

                else if (comboBox_Diagnoses.SelectedIndex == -1)
                    MessageBox.Show("");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void LoadDiagnoses()
        {
            diagnoses.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Diagnoses", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        diagnoses.Add(new Diagnose() { ID = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }

            comboBox_Diagnoses.ItemsSource = diagnoses.Select(x => x.ToString());
        }

        class Diagnose
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public new string ToString()
            {
                return Name;
            }
        }
    }
}
