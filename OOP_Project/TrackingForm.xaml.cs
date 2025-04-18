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
    }
}
