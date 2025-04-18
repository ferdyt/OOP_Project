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

        private string seancePath = "C:/Users/Csgo2/Source/Repos/OOP_Project/OOP_Project/Databases/Current.json";
        private string seance;

        public UserDashboard(string userLogin, Frame mainFrame)
        {
            InitializeComponent();

            seance = JsonSerializer.Serialize(userLogin);
            File.WriteAllText(seancePath, seance);

            var list = DatabaseManager.GetPackagesByUser(userLogin);
            Packages = new ObservableCollection<Package>(list);

            this.DataContext = this;
            _mainFrame = mainFrame;
        }

        public UserDashboard(Frame mainFrame)
        {
            InitializeComponent();

            string userLogin = File.ReadAllText(seancePath);
            userLogin = JsonSerializer.Deserialize<string>(userLogin);

            var list = DatabaseManager.GetPackagesByUser(userLogin);
            Packages = new ObservableCollection<Package>(list);

            this.DataContext = this;
            _mainFrame = mainFrame;
        }

        private void CreatePackageButton_Click(object sender, RoutedEventArgs e)
        {
            var createPackagePage = new CreatePackageForm(_mainFrame);
            _mainFrame.Navigate(createPackagePage);
        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            var authPage = new AuthForm(_mainFrame);
            _mainFrame.Navigate(authPage);
        }
    }

    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count = (int)value;
            string mode = parameter as string;

            if (mode == "HasItems")
                return count > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (mode == "NoItems")
                return count == 0 ? Visibility.Visible : Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
