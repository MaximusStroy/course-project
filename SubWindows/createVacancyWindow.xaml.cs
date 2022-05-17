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
using System.Windows.Shapes;

namespace Vacancy.SubWindows
{
    /// <summary>
    /// Логика взаимодействия для createVacancyWindow.xaml
    /// </summary>
    public partial class createVacancyWindow : Window
    {
        public createVacancyWindow()
        {
            InitializeComponent();
            openList();
        }

        private void btnSaveVacancy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mess = MessageBox.Show("Сохранить изменения?", "Сохранение изменений", MessageBoxButton.YesNo);
                if (mess == MessageBoxResult.Yes)
                {
                    using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                    {
                        vacancy vacCreate = new vacancy();

                        vacCreate.ID_employee = Models.CInfo.id_app;
                        if (txt_nameVac.Text != "") vacCreate.post = txt_nameVac.Text.Trim().ToString();
                        if (txt_salaryVac.Text != "") vacCreate.salary = Convert.ToDecimal(String.Format("{0:C}", txt_salaryVac.Text));
                        if (txt_competencies.Text != "") vacCreate.competencies = txt_competencies.Text.Trim().ToString();

                        db.vacancy.Add(vacCreate);

                        db.SaveChanges();

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelVacancy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mess = MessageBox.Show("Вы хотите отменить все изменения?", "Отмена", MessageBoxButton.YesNo);
                if (mess == MessageBoxResult.Yes)
                {
                    clearPoly();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExitVacancy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mess = MessageBox.Show("Вы хотите выйти?", "Выход", MessageBoxButton.YesNo);
                if (mess == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openList()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var vac = (from e in db.employers
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

                        txt_contactFace.Text = vac.First().contactFace.ToString();
                        txt_companyVac.Text = vac.First().nameCompany.ToString();
                        txt_addressCompany.Text = vac.First().addressCompany.ToString();
                        txt_numberCompany.Text = vac.First().phoneCompany.ToString();
                        txt_numberFace.Text = vac.First().contactNumber.ToString();
                        txt_titleCompany.Text = vac.First().titleCompany.ToString();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearPoly()
        {
            txt_nameVac.Clear();
            txt_salaryVac.Clear();
            txt_competencies.Clear();
        }
    }
}
