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
    /// Логика взаимодействия для ProfileEmployerPage.xaml
    /// </summary>
    public partial class ProfileEmployerPage : Page
    {
        private Dictionary<string, string> cmp = new Dictionary<string, string>();

        public ProfileEmployerPage()
        {
            InitializeComponent();
            loadProfile();
            cmp.Add("contactFace", txtContactTitle.Text);
            cmp.Add("contactNumber", txtNumberContactTitle.Text);
            cmp.Add("nameCompany", txtNameCompany.Text);
            cmp.Add("addressCompany", txtAddressCompany.Text);
            cmp.Add("phoneCompany", txtNumberCompany.Text);
            cmp.Add("titleCompany", txtCompanyTitle.Text);
            _enabled(true);
        }

        private void loadProfile()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var companyProfile = (from e in db.employers
                                          from comp in db.companyMany
                                          where e.ID_company == comp.ID_company
                                          where e.ID_employee == Models.CInfo.id_app
                                          select new
                                          {
                                              contactFace = e.name_employee,
                                              contactNumber = e.number_phone_emp,
                                              nameCompany = comp.name_company,
                                              addressCompany = comp.address_company,
                                              phoneCompany = comp.phone_company,
                                              titleCompany = comp.title_company
                                          }).ToList();

                    txtContactTitle.Text = companyProfile.First().contactFace.ToString();
                    txtNumberContactTitle.Text = companyProfile.First().contactNumber.ToString();
                    txtNameCompany.Text = companyProfile.First().nameCompany.ToString();
                    txtAddressCompany.Text = companyProfile.First().addressCompany.ToString();
                    txtNumberCompany.Text = companyProfile.First().phoneCompany.ToString();
                    txtCompanyTitle.Text = companyProfile.First().titleCompany.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _enabled(bool _bool)
        {
            txtAddressCompany.IsReadOnly = _bool;
            txtCompanyTitle.IsReadOnly = _bool;
            txtContactTitle.IsReadOnly = _bool;
            txtNameCompany.IsReadOnly = _bool;
            txtNumberCompany.IsReadOnly = _bool;
            txtNumberContactTitle.IsReadOnly = _bool;

            btnCancelProfile.IsEnabled = !_bool;
            btnChangeEmployeeProfile.IsEnabled = _bool;
            btnSaveProfile.IsEnabled = !_bool;
        }

        private void btnChangeEmployeeProfile_Click(object sender, RoutedEventArgs e)
        {
            _enabled(false);
        }

        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Сохранить изменения?", "Сохранение изменений", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _enabled(true);

                    using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                    {
                        var saveProfileEmp = (from emp in db.employers
                                              join comp in db.companyMany
                                              on emp.ID_company equals comp.ID_company
                                              where emp.ID_employee == Models.CInfo.id_app
                                              select emp
                                           ).FirstOrDefault();
                        var saveProfileComp = (from emp in db.employers
                                               join comp in db.companyMany
                                               on emp.ID_company equals comp.ID_company
                                               where emp.ID_employee == Models.CInfo.id_app
                                               select comp
                                           ).FirstOrDefault();
                        if (txtContactTitle.Text != cmp["contactFace"]) saveProfileEmp.name_employee = txtContactTitle.Text;
                        if (txtNumberContactTitle.Text != cmp["contactNumber"]) saveProfileEmp.number_phone_emp = txtNumberContactTitle.Text;
                        if (txtNameCompany.Text != cmp["nameCompany"]) saveProfileComp.name_company = txtNameCompany.Text;
                        if (txtAddressCompany.Text != cmp["addressCompany"]) saveProfileComp.address_company = txtAddressCompany.Text;
                        if (txtNumberCompany.Text != cmp["phoneCompany"]) saveProfileComp.phone_company = txtNumberCompany.Text;
                        if (txtCompanyTitle.Text != cmp["titleCompany"]) saveProfileComp.title_company = txtCompanyTitle.Text;
                        
                        db.SaveChanges();
                    }
                }
                else { loadProfile(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelProfile_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите отменить изменения?", "Отмена изменений", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _enabled(true);
                loadProfile();
            }
        }
    }
}
