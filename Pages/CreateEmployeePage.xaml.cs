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
    /// Логика взаимодействия для CreateEmployeePage.xaml
    /// </summary>
    public partial class CreateEmployeePage : Page
    {
        private bool flag = false;
        private bool flagBuff = false;
        public List<companyMany> comboComp { get; set; }
        public CreateEmployeePage()
        {
            InitializeComponent();
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var notBeetwen = (from c in db.companyMany
                                  select c).ToList();

                notBeetwen.Insert(0, new companyMany
                {
                    name_company = "Нет в списке"
                });
                txtNameCompany.ItemsSource = notBeetwen;
                txtNameCompany.SelectedIndex = 0;
            }
            comboboxUpdate();
        }
        private void comboboxUpdate()
        {
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var currentInfo = (from x in db.companyMany
                                   select x).ToList();

                if (txtNameCompany.SelectedIndex > 0)
                {
                    currentInfo = (from c in db.companyMany
                                   where c.ID_company == txtNameCompany.SelectedIndex
                                   select c).ToList();
                    if (currentInfo != null && currentInfo.Count > 0)
                    {
                        txtCompany.Text = currentInfo.First().name_company.ToString();
                        txtCompany.IsEnabled = false;

                        txtAddressCompany.Text = currentInfo.First().address_company.ToString();
                        txtAddressCompany.IsEnabled = false;

                        txtCompanyTitle.Text = currentInfo.First().title_company.ToString();
                        txtCompanyTitle.IsEnabled = false;

                        txtNumberCompany.Text = currentInfo.First().phone_company.ToString();
                        txtNumberCompany.IsEnabled = false;

                        flag = false;
                    }
                } else if (txtNameCompany.SelectedIndex == 0)
                {
                    txtCompany.Clear();
                    txtCompany.IsEnabled = true;
                    txtAddressCompany.Clear();
                    txtAddressCompany.IsEnabled = true;
                    txtCompanyTitle.Clear();
                    txtCompanyTitle.IsEnabled = true;
                    txtNumberCompany.Clear();
                    txtNumberCompany.IsEnabled = true;

                    flag = true;
                }
            }
        }

        private void txtNameCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboboxUpdate();
        }

        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var chack = (from us in db.users
                                 where us.login_user == Models.CInfo.login
                                 select us).FirstOrDefault();

                    employers emp = new employers();

                    emp.name_employee = txtContactTitle.Text;
                    emp.number_phone_emp = txtNumberContactTitle.Text;
                    emp.ID_user = chack.ID_user;
                    if (flag == false)
                    {
                        emp.ID_company = txtNameCompany.SelectedIndex;
                    } else if (flag == true)
                    {
                        var cmp = new companyMany();
                        cmp.name_company = txtCompany.Text;
                        cmp.phone_company = txtNumberCompany.Text;
                        cmp.title_company = txtCompanyTitle.Text;
                        cmp.address_company = txtAddressCompany.Text;

                        db.companyMany.Add(cmp);

                        db.SaveChanges();

                    }

                    var addComp = (from c in db.companyMany
                                   where c.name_company == txtCompany.Text
                                   select c).FirstOrDefault();

                    emp.ID_company = addComp.ID_company;
                    emp.address_employee = txtAddressContactTitle.Text;

                    db.employers.Add(emp);

                    db.SaveChanges();

                    var lst = (from a in db.employers
                               from us in db.users
                               where a.ID_user == us.ID_user
                               where us.login_user == Models.CInfo.login
                               select a).FirstOrDefault();

                    Models.CInfo.id_app = (int)lst.ID_employee;
                    MessageBox.Show(Models.CInfo.id_app.ToString());

                    NavigationService.Navigate(new Uri("Pages/EmployerPage.xaml", UriKind.Relative));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelProfile_Click(object sender, RoutedEventArgs e)
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
    }
}
