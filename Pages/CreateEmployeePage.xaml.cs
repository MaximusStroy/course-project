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
        public List<companyMany> comboComp { get; set; }
        public CreateEmployeePage()
        {
            InitializeComponent();
            comboboxUpdate();
        }
        private void comboboxUpdate()
        {
            RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();
            var item = db.companyMany.Select(x => x).ToList();
            comboComp = item;
            DataContext = txtNameCompany;
            txtNameCompany.ItemsSource = comboComp.ToList();
        }

        private void txtNameCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities()) {

                var id_com = txtNameCompany.SelectedItem;

                var firstComp = (from c in db.companyMany
                                 where c.name_company == id_com.ToString()
                                 select c).FirstOrDefault();
                if (firstComp != null)
                {
                    txtAddressCompany.Text = firstComp.address_company.ToString();
                    txtAddressCompany.IsEnabled = true;

                    txtCompanyTitle.Text = firstComp.title_company.ToString();
                    txtCompanyTitle.IsEnabled = true;

                    txtNumberCompany.Text = firstComp.phone_company.ToString();
                    txtNumberCompany.IsEnabled = true;
                }
                else if (firstComp == null) MessageBox.Show("null");
            }
        }
    }
}
