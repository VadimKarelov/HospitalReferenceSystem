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
        private List<Ward> wards = new List<Ward>();

        [System.ComponentModel.TypeConverter(typeof(System.Windows.DialogResultConverter))]
        public bool? DialogResult { get; set; }

        public string FIO { get; set; }
        public int DiagnosesID { get; set; }
        public int WardNumber { get; set; }

        public AddNewPatientWindow()
        {
            InitializeComponent();
            LoadDiagnoses();
            LoadWards();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_FIO.Text.Replace(" ", "") != "" && cb_WardN.SelectedIndex != -1 && comboBox_Diagnoses.SelectedIndex != -1)
            {
                FIO = tb_FIO.Text;
                WardNumber = wards.First(x => x.Name == cb_WardN.SelectedItem.ToString()).ID;
                DiagnosesID = diagnoses.First(x => x.Name == comboBox_Diagnoses.SelectedItem.ToString()).ID;
                DialogResult = true;
                this.Close();
            }
            else
            {
                if (tb_FIO.Text.Replace(" ", "") == "")
                    MessageBox.Show("Не введено ФИО!");

                else if (cb_WardN.SelectedIndex == -1)
                    MessageBox.Show("Не выбрана палата!");

                else if (comboBox_Diagnoses.SelectedIndex == -1)
                    MessageBox.Show("Не выбран диагноз!");
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

        private void LoadWards()
        {
            wards.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Wards", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        wards.Add(new Ward() { ID = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }

            cb_WardN.ItemsSource = wards.Select(x => x.ToString());
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

        class Ward
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
