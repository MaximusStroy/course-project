using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using Ubiety.Dns.Core;

namespace Vacancy.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterApplicantPage.xaml
    /// </summary>
    public partial class RegisterApplicantPage : Page
    {
        public RegisterApplicantPage()
        {
            InitializeComponent();
        }

        private void btnCancelReg_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите отменить создание аккауна?", "Отмена создание аккаунта", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var listUser = (from us in db.users
                                    where us.login_user == Models.CInfo.login && us.password_user == Models.CInfo.password
                                    select us).FirstOrDefault();

                    db.users.Remove(listUser);

                    db.SaveChanges();
                }
                NavigationService.Navigate(new Uri("Pages/SignUpAndLogInPage.xaml", UriKind.Relative));
            }
        }

        private void btn_saveChanges_Click(object sender, RoutedEventArgs e)
        {
            addApplicant();
            NavigationService.Navigate(new Uri("Pages/ApplicantPage.xaml", UriKind.Relative));
        }
        private void addApplicant()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var listUser = (from us in db.users
                                    where us.login_user == Models.CInfo.login
                                    select us).ToList();

                    applicants app = new applicants();

                    Models.CInfo m = new Models.CInfo();

                    app.name_applicant = txt_name.Text;
                    app.lastname_applicant = txt_lastname.Text;
                    app.middlename_applicant = txt_middlename.Text;
                    app.number_phone_app = txt_phone.Text;
                    if (comboSex.SelectedIndex == 0)
                    {
                        app.gender_app = "Муж";
                    } else if (comboSex.SelectedIndex == 1)
                    {
                        app.gender_app = "Жен";
                    }
                    app.email_app = txt_mail.Text;
                    app.birthday_app = DateTime.Parse(txt_age.Text);
                    app.address_app = txt_country.Text;
                    app.ID_user = (from us in db.users
                                   where us.login_user == Models.CInfo.login
                                   select us.ID_user).FirstOrDefault();


                    db.applicants.Add(app);

                    db.SaveChanges();

                    var lst = (from a in db.applicants
                               from us in db.users
                               where a.ID_user == us.ID_user
                               where us.login_user == Models.CInfo.login
                               select a).FirstOrDefault();

                    Models.CInfo.id_app = (int)lst.ID_applicant;
                    MessageBox.Show(Models.CInfo.id_app.ToString());
                }
            }
            catch (DbEntityValidationException ex)
            {
                string str = "";
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    str = "Object: " + validationResult.Entry.Entity.ToString() + " ;;; ";
                    Console.WriteLine();
                    foreach (DbValidationError err in validationResult.ValidationErrors)
                    {
                        str += " " + err.ErrorMessage + " /// ";
                    }
                }
                MessageBox.Show(str);
            }
        }
    }
}
