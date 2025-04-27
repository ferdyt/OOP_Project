using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OOP_Project
{
    public partial class UserDashboard : Page
    {
        public ObservableCollection<Package> Packages { get; set; }
        private Frame _mainFrame;

        private string seance;

        public UserDashboard(string userLogin, Frame mainFrame)
        {
            InitializeComponent();

            UserRepository.WriteSeance(userLogin);

            var list = PackageRepository.GetPackagesByUser(userLogin);
            Packages = new ObservableCollection<Package>(list);

            this.DataContext = this;
            _mainFrame = mainFrame;
        }

        public UserDashboard(Frame mainFrame)
        {
            InitializeComponent();

            string? userLogin;

            userLogin = UserRepository.GetSeance();

            var list = PackageRepository.GetPackagesByUser(userLogin);
            Packages = new ObservableCollection<Package>(list);

            this.DataContext = this;
            _mainFrame = mainFrame;
        }

        private void CreatePackageButton_Click(object sender, RoutedEventArgs e)
        {
            var createPackagePage = new CreatePackageForm(_mainFrame, false);
            _mainFrame.Navigate(createPackagePage);
        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            var authPage = new AuthForm(_mainFrame);
            _mainFrame.Navigate(authPage);
        }

        private void CreatePackageWithMoneyOrDocButton_Click(object sender, RoutedEventArgs e)
        {
            var createPackagePage = new CreatePackageForm(_mainFrame, true);
            _mainFrame.Navigate(createPackagePage);
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            User? userSender;
            User? userReceiver;

            Button button = sender as Button;

            if (button != null && button.Tag is Package package)
            {
                userSender = UserRepository.GetUserByLogin(package.senderLogin);
                userReceiver = UserRepository.GetUserByLogin(package.receiverLogin);

                MessageBoxResult result = MessageBox.Show(
                    $"Відправник: {userSender.Name} {userSender.MiddleName} {userSender.LastName}\n" +
                    $"Місто відправлення: {package.senderCity}\n" +
                    $"Отримувач: {userReceiver.Name} {userReceiver.MiddleName} {userReceiver.LastName}\n" +
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

        private void StatusPackageButton_Click(object sender, RoutedEventArgs e)
        {
            var trackingPage = new TrackingForm(_mainFrame);
            _mainFrame.Navigate(trackingPage);
        }
    }
}
