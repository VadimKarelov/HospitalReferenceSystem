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

        private List<int> patientsIndexes = new List<int>();

        private int currentPatient = -1;

        private int currentDoctorId = -1;

        public DoctorWindow(int doctorId)
        {
            InitializeComponent();

            GetUserInformation(doctorId);

            listBox_Journal.Items.Clear();
            listBox_Procedures.Items.Clear();

            LoadPatients(doctorId);

            currentDoctorId = doctorId;
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

        private void LoadPatients(int doctorId)
        {
            comboBox_PatientChoose.Items.Clear();
            patientsIndexes.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT FIO, DiagnosisID, ID FROM Sick WHERE DoctorID = {doctorId}", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patientsIndexes.Add(reader.GetInt32(2));
                        comboBox_PatientChoose.Items.Add($"{reader.GetString(0)}, {Query.GetDiagnoseNameByID(reader.GetInt32(1))}");
                    }
                }

                reader.Close();
            }
        }

        private void LoadPatientInfo(int patientID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT FIO, DiagnosisID, WardID, Status, Login, Password FROM Sick WHERE ID = {patientID}", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        textBox_FIO.Text = reader.GetString(0);
                        textBox_WardNumber.Text = reader.GetInt32(2).ToString();
                        textBlock_LoginPassword.Text = $"(Логин/Пароль)=({reader.GetString(4)}/{reader.GetString(5)})";

                        listBox_Journal.ItemsSource = Query.GetSickHistory(patientID);
                        listBox_Procedures.ItemsSource = Query.GetProcedures(patientID);
                    }
                }

                reader.Close();
            }
        }

        private void LoadPatientInfo_IndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb && cb.SelectedIndex > -1)
            {
                LoadPatientInfo(patientsIndexes[cb.SelectedIndex]);
                currentPatient = patientsIndexes[cb.SelectedIndex];
            }
            else
            {
                currentPatient = -1;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentPatient != -1)
            {
                bool isChanged = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT WardID FROM Sick WHERE ID = {currentPatient}", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    if (int.TryParse(textBox_WardNumber.Text, out int t))
                    {
                        isChanged = reader.GetInt32(0) != t;
                    }

                    reader.Close();
                }

                if (isChanged)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"UPDATE Sick\n" +
                            $"SET FIO = \'{textBox_FIO.Text}\', WardID = {textBox_WardNumber.Text}\n" +
                            $"WHERE ID = {currentPatient}";
                        SqlCommand command = new SqlCommand(commandText, connection);

                        command.ExecuteNonQuery();
                    }

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"INSERT WardJournal (DateTime, SickID, WardNumber, Event)\n" +
                            $"VALUES ('{DateTime.Now}', {currentPatient}, {textBox_WardNumber.Text}, 'Переведен в палату')";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.ExecuteNonQuery();
                    }

                    LoadPatientInfo(patientsIndexes[comboBox_PatientChoose.SelectedIndex]);
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"UPDATE Sick\n" +
                            $"SET FIO = \'{textBox_FIO.Text}'\n"+
                            $"WHERE ID = {currentPatient}";
                        SqlCommand command = new SqlCommand(commandText, connection);

                        command.ExecuteNonQuery();
                    }

                    LoadPatientInfo(patientsIndexes[comboBox_PatientChoose.SelectedIndex]);
                }
            }
        }

        private void AddProcedure_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_PatientChoose.SelectedIndex > -1)
            {
                AddProcedureWindow f = new AddProcedureWindow();
                f.ShowDialog();
                if (f.DialogResult == true)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"INSERT ProceduresJournal (DateTime, SickID, ProcedureID, Status)\n" +
                            $"VALUES ('{DateTime.Now}', {currentPatient}, {f.ProcedureID}, 'Назначена')";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.ExecuteNonQuery();
                    }
                    LoadPatientInfo(patientsIndexes[comboBox_PatientChoose.SelectedIndex]);
                }
            }
            else
            {
                MessageBox.Show("Не выбран пациент");
            }
        }

        private void MarkProcedure(object sender, RoutedEventArgs e)
        {
            if (listBox_Procedures.SelectedIndex > -1)
            {
                bool f = false;
                int index = -1;

                // get procedure id
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string commandText = $"SELECT ProcedureID, Status FROM ProceduresJournal\n" +
                        $"WHERE SickID = {currentPatient}";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string status = reader.GetString(1);
                            string name = Query.GetProcedureNameByID(id);

                            if (ClearProcedure(listBox_Procedures.Items[listBox_Procedures.SelectedIndex] as string) == name)
                            {
                                f = status == "Назначена";
                                index = id;
                            }
                        }
                    }
                }

                if (f)
                {
                    // add note
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"INSERT ProceduresJournal (DateTime, SickID, ProcedureID, Status)\n" +
                            $"VALUES ('{DateTime.Now}', {currentPatient}, {index}, 'Пройдена')";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.ExecuteNonQuery();
                    }
                    LoadPatientInfo(patientsIndexes[comboBox_PatientChoose.SelectedIndex]);
                }
                else
                {
                    MessageBox.Show("Процедура уже пройдена");
                }
            }
            else
            {
                MessageBox.Show("Не выбрана процедура");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string ClearProcedure(string str)
        {
            string res = "";

            int ind = str.IndexOf(" Пройдена");

            if (ind >= 0)
            {
                res = str.Substring(0, ind);
            }

            ind = str.IndexOf(" Назначена");

            if (ind >= 0)
            {
                res = str.Substring(0, ind);
            }

            return res;
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddNewPatientWindow f = new AddNewPatientWindow();
            f.ShowDialog();

            if (f.DialogResult == true)
            {
                string fio = f.FIO;
                int wN = f.WardNumber;
                int diag = f.DiagnosesID;

                string l = $"p{Query.GetLastLoginNumber()}";
                string p = l;

                // add patient
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string commandText = $"INSERT Sick (FIO, DiagnosisID, WardID, DoctorID, Status, Login, Password)\n" +
                        $"VALUES ('{fio}', {diag}, {wN}, {currentDoctorId}, 'На лечении', '{l}', '{p}')";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    
                    command.ExecuteNonQuery();
                }

                int id = (new LoginWindow()).GetSickID(l, p);

                // add note to journal
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string commandText = $"INSERT DiagnosesJournal (DateTime, SickID, DoctorID, DiagnoseID, Status)\n" +
                        $"VALUES ('{DateTime.Now}', {id}, {currentDoctorId}, {diag}, 'Поставлен')";
                    SqlCommand command = new SqlCommand(commandText, connection);

                    command.ExecuteNonQuery();
                }

                GetUserInformation(currentDoctorId);
                LoadPatients(currentDoctorId);
            }
        }

        private void WriteOut_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_PatientChoose.SelectedIndex > -1)
            {
                // check
                bool f = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string commandText = $"SELECT Status FROM Sick WHERE ID={currentPatient}";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    
                    reader.Read();
                    f = reader.GetString(0) == "На лечении";
                }

                if (f)
                {
                    // update info
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"UPDATE Sick\n" +
                            $"SET Status='Выписан'\n" +
                            $"WHERE ID={currentPatient}";
                        SqlCommand command = new SqlCommand(commandText, connection);

                        command.ExecuteNonQuery();
                    }

                    // get diagnose id
                    int dID = -1;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"SELECT DiagnosisID FROM Sick WHERE ID={currentPatient}";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        reader.Read();
                        dID = reader.GetInt32(0);
                    }

                    // write to journal
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string commandText = $"INSERT DiagnosesJournal (DateTime, SickID, DoctorID, DiagnoseID, Status)\n" +
                            $"VALUES ('{DateTime.Now}', {currentPatient}, {currentDoctorId}, {dID}, 'Снят')";
                        SqlCommand command = new SqlCommand(commandText, connection);

                        command.ExecuteNonQuery();
                    }

                    GetUserInformation(currentDoctorId);
                    //listBox_Journal.Items.Clear();
                    //listBox_Procedures.Items.Clear();
                    LoadPatients(currentDoctorId);
                }
                else
                {
                    MessageBox.Show("Пациент уже выписан");
                }
            }
        }
    }
}
