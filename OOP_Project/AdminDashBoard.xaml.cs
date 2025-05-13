using System;
using System.Collections.Generic;
using System.IO.Packaging;
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
    /// Interaction logic for AdminDashBoard.xaml
    /// </summary>
    public partial class AdminDashBoard : Page
    {
        private Frame _mainFrame;
        private UserRepository _userRepository = new UserRepository();

        public AdminDashBoard(Frame mainFrame)
        {
            InitializeComponent();
            UsersList.ItemsSource = _userRepository.GetUsers();
            OrdersList.ItemsSource = PackageRepository.GetPackages();
            _mainFrame = mainFrame;
        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            var authPage = new AuthForm(_mainFrame);
            _mainFrame.Navigate(authPage);
        }

        private void UsersDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;

            if (button != null && button.Tag is User user)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Ім'я: {user.Name}\n" +
                    $"Прізвище: {user.LastName}\n" +
                    $"По батькові: {user.MiddleName}\n" +
                    $"Логін: {user.login}",
                    "Інформація про користувача",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void PackageDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            User? userSender;
            User? userReceiver;

            Button? button = sender as Button;

            if (button != null && button.Tag is Package package)
            {
                userSender = _userRepository.GetUserByLogin(package.senderLogin);
                userReceiver = _userRepository.GetUserByLogin(package.receiverLogin);

                if (userSender == null || userReceiver == null)
                {
                    MessageBox.Show("Користувач не знайдений", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBoxResult result = MessageBox.Show(
                    $"Відправник: {userSender.Name} {userSender.MiddleName} {userSender.LastName}\n" +
                    $"Логін відправника: {userSender.login}\n" +
                    $"Місто відправлення: {package.senderCity}\n" +
                    $"Отримувач: {userReceiver.Name} {userReceiver.MiddleName} {userReceiver.LastName}\n" +
                    $"Логін отримувача: {userReceiver.login}\n" +
                    $"Місто отримання: {package.receiverCity}\n" +
                    $"Статус: {package.status}\n" +
                    $"Вага: {package.weight} кг\n" +
                    $"Вартість: {package.cost} грн\n" +
                    $"Документ: {(package.isDockument ? "Так" : "Ні")}\n" +
                    $"Відділення: {package.postOffice}",
                    "Інформація про посилку",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;

            if (button != null && button.Tag is Package package)
            {
                if (package.status == PackageStatus.Canceled)
                {
                    MessageBox.Show("Посилка вже скасована", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (package.status == PackageStatus.Delivered)
                {
                    MessageBox.Show("Посилка вже доставлена", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                package.status = PackageStatus.Canceled;

                MessageBoxResult result = MessageBox.Show(
                    $"Ви впевнені, що хочете скасувати посилку з ID: {package.id}?",
                    "Підтвердження",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    PackageRepository.UpdatePackage(package, package.id);
                    OrdersList.ItemsSource = PackageRepository.GetPackages();
                }
            }
        }

        private void DeliverButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;

            if (button != null && button.Tag is Package package)
            {
                if (package.status == PackageStatus.Delivered)
                {
                    MessageBox.Show("Посилка вже доставлена", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                package.status = PackageStatus.Delivered;

                MessageBoxResult result = MessageBox.Show(
                    $"Ви впевнені, що хочете доставити посилку з ID: {package.id}?",
                    "Підтвердження",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    PackageRepository.UpdatePackage(package, package.id);
                    OrdersList.ItemsSource = PackageRepository.GetPackages();
                }
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;

            if (button != null && button.Tag is User user)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Ви впевнені, що хочете видалити користувача: {user.login}?",
                    "Підтвердження",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _userRepository.DelUser(user.login);
                    UsersList.ItemsSource = _userRepository.GetUsers();
                }
            }
        }
    }
}
