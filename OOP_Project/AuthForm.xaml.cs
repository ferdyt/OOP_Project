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
    }
}
