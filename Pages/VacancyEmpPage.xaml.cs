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
    /// Логика взаимодействия для VacancyEmpPage.xaml
    /// </summary>
    public partial class VacancyEmpPage : Page
    {
        List<vacancy> vacancies = new List<vacancy>();
        public static SubWindows.createVacancyWindow createVacancy;

        public VacancyEmpPage()
        {
            InitializeComponent();
            loadVacancy();
        }

        private void loadVacancy()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    listVac.ItemsSource = (from v in db.vacancy
                                 where v.ID_employee == Models.CInfo.id_app
                                 select v).ToList();
                }
                listVac.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listVac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var vac = (listVac.SelectedItem as vacancy);

                if (vac != null)
                {
                    var companyInfo = db.selectCompany((int)vac.ID_vac);

                    if (!String.IsNullOrEmpty(companyInfo.First().company.ToString())) 
                    {
                        txt_companyVac.Text = companyInfo.First().company.ToString(); 
                    };
                    if (!String.IsNullOrEmpty(companyInfo.First().post.ToString()))
                    {
                        txt_nameVac.Text = companyInfo.First().post.ToString(); 
                    };
                    if (!String.IsNullOrEmpty(companyInfo.First().salary.ToString()))
                    {
                        txt_salaryVac.Text = String.Format("{0:C}", companyInfo.First().salary);
                    }
                    else
                    {
                        txt_salaryVac.Text = "Доход не указан";
                    }
                    if (!String.IsNullOrEmpty(companyInfo.First().name_competencies.ToString()))
                    {
                        txt_competencies.Text = companyInfo.First().name_competencies.ToString();
                    };
                    if (!String.IsNullOrEmpty(companyInfo.First().address_company.ToString()))
                    {
                        txt_addressCompany.Text = companyInfo.First().address_company.ToString();
                    };
                    if (!String.IsNullOrEmpty(companyInfo.First().phone_company.ToString()))
                    {
                        txt_numberCompany.Text = companyInfo.First().phone_company.ToString();
                    };
                    if (!String.IsNullOrEmpty(companyInfo.First().phone_emp.ToString()))
                    {
                        txt_numberFace.Text = companyInfo.First().phone_emp.ToString();
                    };
                    if (!String.IsNullOrEmpty(companyInfo.First().name_employee.ToString()))
                    {
                        txt_contactFace.Text = companyInfo.First().name_employee.ToString();
                    };
                    if (!String.IsNullOrEmpty(companyInfo.First().title.ToString()))
                    {
                        txt_titleCompany.Text = companyInfo.First().title.ToString();
                    };
                    txt_numVacancy.Text = companyInfo.First().vacID.ToString();

                    _enable(true);
                }
            }
        }

        private void _enable(bool _bool)
        {
            txt_nameVac.IsReadOnly = _bool;
            txt_salaryVac.IsReadOnly = _bool;
            txt_competencies.IsReadOnly = _bool;

            btnChangeVacanxy.IsEnabled = _bool;
            btnCancelVacancy.IsEnabled = !_bool;
            btnSaveVacancy.IsEnabled = !_bool;
        }

        private void btnChangeVacanxy_Click(object sender, RoutedEventArgs e)
        {
            _enable(false);
        }

        private void btnSaveVacancy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _enable(true);
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    Models.CInfo info = new Models.CInfo();

                    var vac = (listVac.SelectedItem as vacancy);

                    var save = (from v in db.vacancy
                                where v.ID_vac == vac.ID_vac
                                select v).FirstOrDefault();
                    save.ID_employee = Models.CInfo.id_app;

                    if (txt_nameVac.Text != "") save.post = txt_nameVac.Text.Trim();
                    if (txt_salaryVac.Text != "")
                    {
                        string str = info.convertMoneyToStr(String.Format("{0:C}", txt_salaryVac.Text));
                        save.salary = Convert.ToDecimal(str);
                    }
                    if (txt_competencies.Text != "") save.competencies = txt_competencies.Text.Trim();

                    db.SaveChanges();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelVacancy_Click(object sender, RoutedEventArgs e)
        {
            _enable(true);
        }

        private void btnCreateVacancy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isWindowOpen = false;

                foreach (Window w in App.Current.Windows)
                {
                    if (w is SubWindows.createVacancyWindow)
                    {
                        isWindowOpen = true;
                        w.Activate();
                    }
                }

                if (!isWindowOpen)
                {
                    createVacancy = new SubWindows.createVacancyWindow();
                    createVacancy.Show();
                }
                else loadVacancy();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { loadVacancy(); }
        }

        private void clearScreen()
        {
            txt_addressCompany.Clear();
            txt_companyVac.Clear();
            txt_competencies.Clear();
            txt_contactFace.Clear();
            txt_nameVac.Clear();
            txt_numberCompany.Clear();
            txt_numberFace.Clear();
            txt_numVacancy.Text = "";
            txt_salaryVac.Clear();
            txt_titleCompany.Clear();
        }

        private void btnDeleteVacancy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var a = MessageBox.Show("Вы уверены что хотите удалить вакансию?", "Удаление вакансии", MessageBoxButton.OKCancel);
                if (a == MessageBoxResult.OK)
                {
                    using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                    {
                        var dropVac = (vacancy)listVac.SelectedItem;

                        db.dropVacancy((int)dropVac.ID_vac);

                        clearScreen();
                        loadVacancy();
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
