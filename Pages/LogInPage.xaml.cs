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
    /// 

    

    public partial class LogInPage : Page
    {
        bool isLogin = false;
        bool isPass = false;

        public LogInPage()
        {
            InitializeComponent();
        }
        public void LogIn()
        {
            using(RecruitmentAgencyEntities bd = new RecruitmentAgencyEntities())
            {
                var findingUser = bd.users 
                                   .Where (u => u.login_user == txt_logIn.Text && u.password_user == txt_pass.Password )
                                   .Select (u => new { u.login_user, u.password_user, u.types_users, u.ID_user}).ToList();
                if (findingUser.Count > 0 && findingUser.First().types_users.ID_type_user == 3)
                {
                    NavigationService.Navigate(new Uri("Pages/ApplicantPage.xaml", UriKind.Relative));
                    Models.CInfo.status = (int)findingUser.First().ID_user;
                } else
                {
                    MessageBox.Show("Вы ввели неправильный пароль или логин");
                }
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/ChangeStatusPage.xaml", UriKind.Relative));
        }

        private void btn_logIn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_logIn.Text))
            {
                if (!string.IsNullOrEmpty(txt_pass.Password))
                {
                    LogIn();
                }
                else MessageBox.Show("Вы не ввели пароль");
            }
            else MessageBox.Show("Вы не ввели логин");
        }
    }
}
