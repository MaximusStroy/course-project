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
    /// Логика взаимодействия для CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        List<users> usersList = new List<users>();

        bool checkPass = false;
        bool checkLog = false;
        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private void createUser (string _str)
        {
            Models.CInfo.login = txt_logIn.Text;
            Models.CInfo.password = txt_pass.Password.ToString();
            NavigationService.Navigate(new Uri(_str, UriKind.Relative));
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                users us = new users();

                us.login_user = Models.CInfo.login;
                us.password_user = Models.CInfo.password;
                us.ID_type_user = Models.CInfo.status;

                if (!String.IsNullOrEmpty(us.login_user) && !String.IsNullOrEmpty(us.password_user))
                {
                    db.users.Add(us);
                    db.SaveChanges();
                }
                else MessageBox.Show("Логин или пароль пуст");
            }
        }
        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (true == checkPass && true == checkLog)
                {
                    if (Models.CInfo.status == 3)
                    {
                        createUser("Pages/RegisterApplicantPage.xaml");
                    } else if (Models.CInfo.status == 2)
                    {
                        createUser("Pages/CreateEmployeePage.xaml");
                    }
                
                }
                else if (false == checkPass && false == checkLog)
                {
                    MessageBox.Show("Такой логин уже существует и ваши пароли не совпадают");
                }
                else if (false == checkLog)
                {
                    MessageBox.Show("Такой логин уже существует. Придумайте другой логин");
                } else if (false == checkPass)
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_logIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_logIn.Text))
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    usersList = (from u in db.users
                                 where u.login_user == txt_logIn.Text
                                 select u).ToList();
                }
                if (usersList.Count > 0)
                {
                    checkLog = false;
                    txtCheckLog.Text = "Логин занят";
                    txtCheckLog.Foreground = (Brush)Application.Current.MainWindow.FindResource("error");
                } else
                {
                    checkLog = true;
                    txtCheckLog.Text = "Логин свободен";
                    txtCheckLog.Foreground = (Brush)Application.Current.MainWindow.FindResource("yes");
                }
            }
        }

        private void txt_passCopy_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_pass.Password) && !String.IsNullOrEmpty(txt_passCopy.Password))
            {
                if (txt_passCopy.Password != txt_pass.Password)
                {
                    checkPass = false;
                    txtCheckPass.Text = "Пароли не совпадают";
                    txtCheckPass.Foreground = (Brush)Application.Current.MainWindow.FindResource("error");
                } else
                {
                    checkPass = true;
                    txtCheckPass.Text = "Пароли совпадают";
                    txtCheckPass.Foreground = (Brush)Application.Current.MainWindow.FindResource("yes");
                }
            }
        }
    }
}
