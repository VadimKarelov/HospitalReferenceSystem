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
    /// Логика взаимодействия для AddProcedureWindow.xaml
    /// </summary>
    public partial class AddProcedureWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=HospitalReferenceSystemDataBase;Integrated Security=True";

        [System.ComponentModel.TypeConverter(typeof(System.Windows.DialogResultConverter))]
        public bool? DialogResult { get; set; }

        public int ProcedureID { get; set; }

        private List<Procedure> procedures;

        public AddProcedureWindow()
        {
            InitializeComponent();
            LoadProcedures();
        }

        private void LoadProcedures()
        {
            cb_Procedures.Items.Clear();
            procedures = new List<Procedure>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM ProceduresAndExaminations", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    procedures.Add(new Procedure()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CabinetNumber = reader.GetInt32(2),
                    });
                }

                reader.Close();
            }
            cb_Procedures.ItemsSource = procedures.Select(x => x.ToString());
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Procedures.SelectedIndex > -1)
            {
                DialogResult = true;
                ProcedureID = procedures[cb_Procedures.SelectedIndex].ID;
                this.Close();
            }
            else
            {
                MessageBox.Show("Не выбрана процедура/обследование");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        class Procedure
        {
            public int ID;
            public string Name;
            public int CabinetNumber;

            public new string ToString()
            {
                return $"{Name}, кабинет {CabinetNumber}";
            }
        }
    }
}
