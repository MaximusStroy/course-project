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
    /// Логика взаимодействия для EmployerPage.xaml
    /// </summary>
    public partial class EmployerPage : Page
    {
        public EmployerPage()
        {
            InitializeComponent();
            loadPage();
        }
        private bool _profile = true;
        private bool _vacancy = false;
        private bool _resume = false;
        private bool _response = false;
        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Вы уверены что хотите выйти из учетной записи?", "Выход из учетной записи", MessageBoxButton.YesNo)) == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new Uri("Pages/ChangeStatusPage.xaml", UriKind.Relative));
                    Models.CInfo.id_app = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_myProfile_Click(object sender, RoutedEventArgs e)
        {
            loadPage();
        }

       private void btn_vacancy_Click(object sender, RoutedEventArgs e)
        {
            if (_vacancy == true)
            {
                frameApp.Navigate(new VacancyEmpPage());
                _profile = true;
                _resume = true;
                _vacancy = false;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btnResume.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btnResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btn_vacancy.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btn_vacancy.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                _response = true;

                btn_responses.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_responses.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
            }
        }

        private void loadPage()
        {
            if (_profile == true)
            {
                frameApp.Navigate(new ProfileEmployerPage());
                _profile = false;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                _resume = true;
                btnResume.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btnResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                _vacancy = true;
                btn_vacancy.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_vacancy.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                _response = true;

                btn_responses.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_responses.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_response == true)
            {
                frameApp.Navigate(new ResponsesEmpPage());
                _profile = true;
                _resume = true;
                _vacancy = true;
                _response = false;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btnResume.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btnResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btn_responses.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btn_responses.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                btn_vacancy.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_vacancy.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");

            }
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            if (_resume == true)
            {
                frameApp.Navigate(new ResumeEmpPage());
                _profile = true;
                _resume = false;
                _vacancy = true;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btnResume.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btnResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                btn_vacancy.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_vacancy.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                _response = true;
                btn_responses.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_responses.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
            }
        }

    }
}
