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
                SqlCommand command = new SqlCommand($"SELECT ID FROM Doctors WHERE Login='{login}' AND Password='{password}'", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    res = reader.GetInt32(0);
                }

                reader.Close();
            }
            return res;
        }

        public int GetSickID(string login, string password)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT ID FROM Sick WHERE Login='{login}' AND Password='{password}'", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    res = reader.GetInt32(0);
                }

                reader.Close();
            }
            return res;
        }
    }
}
