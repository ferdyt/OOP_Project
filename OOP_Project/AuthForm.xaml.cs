using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP_Project
{
    /// <summary>
    /// Interaction logic for AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Page
    {
        private Frame _mainFrame;

        private string seancePath = "C:/Users/Csgo2/Source/Repos/OOP_Project/OOP_Project/Databases/Current.json";
        private string seance;
        public AuthForm(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerPage = new RegisterWindow(_mainFrame);
            _mainFrame.Navigate(registerPage);
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            var trackingPage = new TrackingForm(_mainFrame);
            _mainFrame.Navigate(trackingPage);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                MessageBox.Show("Введіть логін та пароль");
                return;
            }

            string userLogin = LoginTextBox.Text;
            string userPassword = PasswordTextBox.Password;

            bool isLogin = DatabaseManager.Login(userLogin, userPassword);

            if (isLogin)
            {
                seance = JsonSerializer.Serialize(userLogin);
                File.WriteAllText(seancePath, seance);

                var userDashboard = new UserDashboard(userLogin, _mainFrame);
                _mainFrame.Navigate(userDashboard);
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль");
                return;
            }
        }
    }
}
