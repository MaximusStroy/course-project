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
                    var vac = (from vv in db.vacancyView
                               where vv.ID_employee == Models.CInfo.id_app
                               select vv).FirstOrDefault();

                    txt_contactFace.Text = vac.name_employee.ToString();
                    txt_companyVac.Text = vac.name_company.ToString();
                    txt_addressCompany.Text = vac.address_company.ToString();
                    txt_numberCompany.Text = vac.phone_company.ToString();
                    txt_numberFace.Text = vac.number_phone_emp.ToString();
                    txt_titleCompany.Text = vac.title_company.ToString();
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
