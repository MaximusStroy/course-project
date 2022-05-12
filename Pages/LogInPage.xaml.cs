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
        public LogInPage()
        {
            InitializeComponent();
        }
        public void LogIn()
        {
            using(RecruitmentAgencyEntities bd = new RecruitmentAgencyEntities())
            {

                switch (Models.CInfo.status)
                {
                    case 2:
                        var findingUser = (from us in bd.users
                                           from e in bd.employers
                                           where us.ID_user == e.ID_user
                                           where us.login_user == txt_logIn.Text
                                           where us.password_user == txt_pass.Password
                                           select new
                                           {
                                               idUser = us.ID_user,
                                               type = us.ID_type_user,
                                               login = us.login_user,
                                               pass = us.password_user,
                                               idApp = e.ID_employee
                                           }).ToList();
                        if (findingUser.Count > 0)
                        {
                            NavigationService.Navigate(new Uri("Pages/EmployerPage.xaml", UriKind.Relative));
                            Models.CInfo.id_app = (int)findingUser.First().idApp;
                        }
                        else MessageBox.Show("Пользователь не найден");
                        break;

                    case 3:
                        findingUser = (from us in bd.users
                                           from app in bd.applicants
                                           where us.ID_user == app.ID_user
                                           where us.login_user == txt_logIn.Text
                                           where us.password_user == txt_pass.Password
                                           select new
                                           {
                                               idUser = us.ID_user,
                                               type = us.ID_type_user,
                                               login = us.login_user,
                                               pass = us.password_user,
                                               idApp = app.ID_applicant
                                           }).ToList();
                        if (findingUser.Count > 0)
                        {
                            NavigationService.Navigate(new Uri("Pages/ApplicantPage.xaml", UriKind.Relative));
                            Models.CInfo.id_app = (int)findingUser.First().idApp;
                        }
                        break;
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
