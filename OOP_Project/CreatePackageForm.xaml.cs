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

namespace OOP_Project
{
    /// <summary>
    /// Interaction logic for CreatePackageForm.xaml
    /// </summary>
    public partial class CreatePackageForm : Page
    {
        private Frame _mainFrame;
        private UserRepository _userRepository = new UserRepository();

        public CreatePackageForm(Frame mainFrame, bool isMoneyOrDoc)
        {
            InitializeComponent();

            if (isMoneyOrDoc)
            {
                TittleCreatePackage.Content = "Створити документ/гроші";
                IsMoneyOrDocCheckBox.IsChecked = true;
                IsMoneyOrDocCheckBox.IsEnabled = false;
                WeightTextBox.IsEnabled = false;
                WeightTextBox.BorderBrush = new SolidColorBrush(Colors.White);
            }
            else
            {
                TittleCreatePackage.Content = "Створити посилку";
                IsMoneyOrDocCheckBox.IsChecked = false;
                IsMoneyOrDocCheckBox.IsEnabled = false;
            }

            _mainFrame = mainFrame;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var userDashboard = new UserDashboard(_mainFrame);
            _mainFrame.Navigate(userDashboard);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReceiverTextBox.Text) || string.IsNullOrEmpty(SenderCityTextBox.Text) ||
                string.IsNullOrWhiteSpace(ReceiverCityTextBox.Text) || string.IsNullOrWhiteSpace(WeightTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) || string.IsNullOrWhiteSpace(PostOfficeTextBox.Text))
            {
                MessageBox.Show("Заповніть всі поля!");
                return;
            }

            if (!double.TryParse(WeightTextBox.Text, out double weight) || weight <= 0)
            {
                MessageBox.Show("Введіть коректну вагу!");
                return;
            }

            if (!int.TryParse(PriceTextBox.Text, out int price) || price <= 0)
            {
                MessageBox.Show("Введіть коректну ціну!");
                return;
            }

            User? user = _userRepository.GetUserByLogin(ReceiverTextBox.Text);

            if (user == null)
            {
                MessageBox.Show("Отримувача не знайдено!");
                return;
            }

            if (!int.TryParse(PostOfficeTextBox.Text, out int postOffice) || postOffice <= 0)
            {
                MessageBox.Show("Введіть коректне відділення!");
                return;
            }

            if (UserRepository.GetSeance() == ReceiverTextBox.Text)
            {
                MessageBox.Show("Ви не можете відправити посилку самі собі!");
                return;
            }

            Package package = new Package(
                Guid.NewGuid(),
                UserRepository.GetSeance(),
                ReceiverTextBox.Text,
                PackageStatus.InTransit,
                (float)weight,
                price,
                (bool)IsMoneyOrDocCheckBox.IsChecked,
                SenderCityTextBox.Text,
                ReceiverCityTextBox.Text,
                postOffice
                );

            PackageRepository.AddPackage(package);
            _mainFrame.Navigate(new UserDashboard(UserRepository.GetSeance(), _mainFrame));
        }
    }
}
