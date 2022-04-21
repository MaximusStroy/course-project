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
    /// Логика взаимодействия для RecPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }
        public void LogIn()
        {
            using(RecruitmentAgencyEntities bd = new RecruitmentAgencyEntities())
            {
                var findingUser = (from u in bd.users 
                                   where u.login_user == txt_logIn.Text && u.password_user == txt_pass.Password 
                                   select u).ToList();
                if (findingUser.Count > 0 && findingUser.First().types_users.ID_type_user == 3)
                {
                    MessageBox.Show(findingUser.First().types_users.name_type);
                    NavigationService.Navigate(new Uri("Pages/ApplicantPage.xaml", UriKind.Relative));
                }
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/ChangeStatusPage.xaml", UriKind.Relative));
        }

        private void btn_logIn_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
        }
    }
}
