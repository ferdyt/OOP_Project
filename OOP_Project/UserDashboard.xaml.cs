using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OOP_Project
{
    public partial class UserDashboard : Page
    {
        public ObservableCollection<Package> Packages { get; set; }

        public UserDashboard(string userLogin)
        {
            InitializeComponent();

            var list = DatabaseManager.GetPackagesByUser(userLogin);
            Packages = new ObservableCollection<Package>(list);

            this.DataContext = this;
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
