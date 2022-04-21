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
    /// Логика взаимодействия для ApplicantPage.xaml
    /// </summary>
    public partial class ApplicantPage : Page
    {
        public ApplicantPage()
        {
            InitializeComponent();
            frameApp.Navigate(new VacanciesPage());
        }


        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LogInPage.xaml", UriKind.Relative));
        }

        private void btn_myProfile_Click(object sender, RoutedEventArgs e)
        {
            frameApp.Navigate(new ProfilePage());
        }

        private void btn_vacancy_Click(object sender, RoutedEventArgs e)
        {
            frameApp.Navigate(new VacanciesPage());
        }
    }
}
