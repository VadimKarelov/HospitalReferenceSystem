using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalReferenceSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            PatienceWindow patienceWindow = new PatienceWindow();
            patienceWindow.Show();

            DoctorWindow doctorWindow = new DoctorWindow();
            doctorWindow.Show();

            this.Close();
        }
    }
}
