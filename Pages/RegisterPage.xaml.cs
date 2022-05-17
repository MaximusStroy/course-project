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

namespace Vacancy.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void btnGoLogIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/ChangeStatusPage.xaml", UriKind.Relative));
        }

        private void ApplicantBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/CreateAccountPage.xaml", UriKind.Relative));
            Models.CInfo.status = 3;
        }

        private void LogInEmployerBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/CreateAccountPage.xaml", UriKind.Relative));
            Models.CInfo.status = 2;
        }
    }
}
