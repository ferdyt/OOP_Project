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
        public TrackingForm(Frame frame)
        {
            InitializeComponent();
            _mainFrame = frame;
        }

        private void PersonalAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var userDashBoard = new UserDashboard("Kein22");
            _mainFrame.Navigate(userDashBoard);
        }
    }
}
