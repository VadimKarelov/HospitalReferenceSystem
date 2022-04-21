using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

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

            DoctorWindow f = new DoctorWindow(GetDoctorID("d1", "d1"));
            f.ShowDialog();
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
                    DoctorWindow f = new DoctorWindow(GetDoctorID(textBox_Login.Text, passwordBox_Password.Password));
                    f.ShowDialog();
                }
                else if (GetSickID(textBox_Login.Text, passwordBox_Password.Password) > -1)
                {
                    PatientWindow f = new PatientWindow(GetSickID(textBox_Login.Text, passwordBox_Password.Password));
                    f.ShowDialog();
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
                SqlCommand command = new SqlCommand("SELECT ID, Login, Password FROM Doctors", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(1) == login && reader.GetString(2) == password)
                        {
                            res = reader.GetInt32(0);
                            break;
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
                SqlCommand command = new SqlCommand("SELECT ID, Login, Password FROM Sick", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(1) == login && reader.GetString(2) == password)
                        {
                            res = reader.GetInt32(0);
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
