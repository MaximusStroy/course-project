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
            NavigationService.Navigate(new Uri("Pages/LogInPage.xaml", UriKind.Relative));
        }

        private void btn_myProfile_Click(object sender, RoutedEventArgs e)
        {
            loadPage();
        }

      /*  private void btn_vacancy_Click(object sender, RoutedEventArgs e)
        {
            if (_vacancy == true)
            {
                frameApp.Navigate(new VacanciesPage());
                _profile = true;
                _resume = true;
                _vacancy = false;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btn_myResume.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btn_vacancy.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btn_vacancy.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                _response = true;

                btn_responses.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_responses.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
            }
        }
*/
        /*private void btn_myResume_Click(object sender, RoutedEventArgs e)
        {
            if (_resume == true)
            {
                frameApp.Navigate(new ResumeAppPage());
                _profile = true;
                _resume = false;
                _vacancy = true;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btn_myResume.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btn_myResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                btn_vacancy.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_vacancy.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                _response = true;
                btn_responses.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_responses.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
            }
        }
*/
        private void loadPage()
        {
            if (_profile == true)
            {
                frameApp.Navigate(new ProfileEmployerPage());
                _profile = false;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                _resume = true;
                btn_myResume.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
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
                frameApp.Navigate(new ResponsesPage());
                _profile = true;
                _resume = true;
                _vacancy = true;
                _response = false;
                btn_myProfile.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myProfile.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btn_myResume.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_myResume.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");
                btn_responses.Background = (Brush)Application.Current.MainWindow.FindResource("dark");
                btn_responses.Foreground = (Brush)Application.Current.MainWindow.FindResource("theLightest");
                btn_vacancy.Background = (Brush)Application.Current.MainWindow.FindResource("light");
                btn_vacancy.Foreground = (Brush)Application.Current.MainWindow.FindResource("theDarkest");

            }
        }
    }
}
