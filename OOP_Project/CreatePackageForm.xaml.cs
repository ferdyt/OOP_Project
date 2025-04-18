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

        public CreatePackageForm(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var userDashboard = new UserDashboard(_mainFrame);
            _mainFrame.Navigate(userDashboard);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
