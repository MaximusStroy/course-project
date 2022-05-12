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
        public ProfileEmployerPage()
        {
            InitializeComponent();
            loadProfile();
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
    }
}
