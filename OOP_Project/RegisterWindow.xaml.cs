using System;
using System.Collections.Generic;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Page
    {
        private Frame _mainFrame;
        SolidColorBrush gray = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D9D9D9"));
        private UserRepository _userRepository = new UserRepository();

        public RegisterWindow(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var authPage = new AuthForm(_mainFrame);
            _mainFrame.Navigate(authPage);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser;

            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password) ||
                string.IsNullOrEmpty(RepeatPasswordTextBox.Password) || string.IsNullOrEmpty(NameTextBox.Text) ||
                string.IsNullOrEmpty(LastNameTextBox.Text) || string.IsNullOrEmpty(MiddleNameTextBox.Text))
            {
                MessageBox.Show("Не усі поля заповнені");
                return;
            }

            if (PasswordTextBox.Password != RepeatPasswordTextBox.Password)
            {
                PasswordTextBox.BorderBrush = Brushes.Red;
                RepeatPasswordTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Паролі не співпадають");
                PasswordTextBox.BorderBrush = gray;
                RepeatPasswordTextBox.BorderBrush = gray;
                return;
            }

            if (PasswordTextBox.Password.Length < 4)
            {
                if (PasswordTextBox.Password.Length < 4)
                    PasswordTextBox.BorderBrush = Brushes.Red;
                if (RepeatPasswordTextBox.Password.Length < 4)
                    RepeatPasswordTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Пароль повинен містити не меньше 4 символів");
                PasswordTextBox.BorderBrush = gray;
                RepeatPasswordTextBox.BorderBrush = gray;
                return;
            }

            User? user = _userRepository.GetUserByLogin(LoginTextBox.Text);

            if (user != null)
            {
                LoginTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Користувач з таким логіном вже існує");
                LoginTextBox.BorderBrush = gray;
                return;
            }

            if (NameTextBox.Text.Length < 2 || LastNameTextBox.Text.Length < 2 ||
                MiddleNameTextBox.Text.Length < 2)
            {
                if (NameTextBox.Text.Length < 2)
                    NameTextBox.BorderBrush = Brushes.Red;
                if (LastNameTextBox.Text.Length < 2)
                    LastNameTextBox.BorderBrush = Brushes.Red;
                if (MiddleNameTextBox.Text.Length < 2)
                    MiddleNameTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Користувач повинен містити не меньше 2 символи у полях ім'я, прізвище, по-батькові");
                NameTextBox.BorderBrush = gray;
                LastNameTextBox.BorderBrush = gray;
                MiddleNameTextBox.BorderBrush = gray;
                return;
            }

            if (LoginTextBox.Text == "admin")
            {
                MessageBox.Show("Логін 'admin' зарезервовано для адміністратора");
                LoginTextBox.BorderBrush = Brushes.Red;
                return;
            }

            try
            {
                PackageRepository.GetPackagesByUser(LoginTextBox.Text);
            }
            catch (JsonException)
            {
                MessageBox.Show("Помилка при створенні користувача. Можливо він був видалений");
                return;
            }

            newUser = new User(LoginTextBox.Text, PasswordTextBox.Password, NameTextBox.Text, LastNameTextBox.Text, MiddleNameTextBox.Text);

            _userRepository.AddUser(newUser);

            var userDashboardPage = new UserDashboard(newUser.login, _mainFrame);
            _mainFrame.Navigate(userDashboardPage);
        }
    }
}
