using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace OOP_Project
{
    /// <summary>
    /// Interaction logic for TrackingForm.xaml
    /// </summary>
    public partial class TrackingForm : Page
    {
        private Frame _mainFrame;
        private string seancePath = "C:/Users/Csgo2/Source/Repos/OOP_Project/OOP_Project/Databases/Current.json";
        public TrackingForm(Frame frame)
        {
            InitializeComponent();
            _mainFrame = frame;
        }

        private void PersonalAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(seancePath))
            {
                MessageBox.Show("Ви не авторизовані");
                var authPage = new AuthForm(_mainFrame);
                _mainFrame.Navigate(authPage);
                return;
            }

            string userLogin = File.ReadAllText(seancePath);
            userLogin = JsonSerializer.Deserialize<string>(userLogin);

            if (userLogin == null)
            {
                MessageBox.Show("Ви не авторизовані");
                var authPage = new AuthForm(_mainFrame);
                _mainFrame.Navigate(authPage);
            }
            else
            {
                var userDashBoard = new UserDashboard(userLogin, _mainFrame);
                _mainFrame.Navigate(userDashBoard);
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Package package = null; // Initialize the variable to avoid CS0165

            if (Guid.TryParse(InvoiceTextBox.Text, out Guid packageId))
            {
                package = PackageRepository.GetPackageById(packageId);
            }
            else
            {
                MessageBox.Show("Некоректний айді посилки");
            }

            if (package != null)
            {
                if (package.status == PackageStatus.Canceled)
                {
                    OutputLabel.Foreground = System.Windows.Media.Brushes.Red;
                    OutputLabel.Content = "Посилка скасована";
                }
                else if (package.status == PackageStatus.Delivered)
                {
                    OutputLabel.Foreground = System.Windows.Media.Brushes.Green;
                    OutputLabel.Content = "Посилка доставлена";
                }
                else if (package.status == PackageStatus.InTransit)
                {
                    OutputLabel.Foreground = System.Windows.Media.Brushes.DarkOrange;
                    OutputLabel.Content = "Посилка в дорозі";
                }
            }
            else
            {
                OutputLabel.Foreground = System.Windows.Media.Brushes.Black;
                OutputLabel.Content = "Посилка не знайдена";
            }
        }
    }
}
