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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=HospitalReferenceSystemDataBase;Integrated Security=True";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            try
            {
                if (GetDoctorID(textBox_Login.Text, passwordBox_Password.Password) > -1)
                {
                    DoctorWindow f = new DoctorWindow();
                    f.Show();
                    this.Close();
                }
                else if (GetSickID(textBox_Login.Text, passwordBox_Password.Password) > -1)
                {
                    PatienceWindow f = new PatienceWindow();
                    f.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetDoctorID(string login, string password)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Doctors", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(3) == login && reader.GetString(4) == password)
                        {
                            res = reader.GetInt32(0);
                        }
                    }
                }

                reader.Close();
            }
            return res;
        }

        private int GetSickID(string login, string password)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Sick", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(6) == login && reader.GetString(7) == password)
                        {
                            res = reader.GetInt32(0);
                        }
                    }
                }

                reader.Close();
            }
            return res;
        }
    }
}
