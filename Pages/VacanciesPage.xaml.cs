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
using System.ComponentModel;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Vacancy.Pages
{
    /// <summary>
    /// Логика взаимодействия для VacanciesPage.xaml
    /// </summary>
    public partial class VacanciesPage : Page
    {
        List<vacancy> vacancies = new List<vacancy>();
        List<vacancy> filteredList = new List<vacancy>();

        public List<resume_tab> comboResumes { get; set; }
        List<resume_tab> changeComboResume = new List<resume_tab>();
        public VacanciesPage()
        {
            InitializeComponent();
            ComboType.SelectedIndex = 0;
            Loaded += VacanciesPage_Loaded;
            listVac.SelectedIndex = 0;
            comboboxUpdate();
        }

        private void VacanciesPage_Loaded(object sender, RoutedEventArgs e)
        {

            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                if (ComboType.SelectedIndex == 0)
                {
                    vacancies = (from x in db.vacancy
                                 select x).ToList();
                }
            }
            listVac.ItemsSource = vacancies.ToList();

        }

        private void comboboxUpdate()
        {
            RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();
            var item = db.resume_tab.Where(x => x.ID_applicant == Models.CInfo.id_app).ToList();
            comboResumes = item;
            DataContext = comboResumes;
            ComboResume.ItemsSource = comboResumes.ToList();
            ComboResume.SelectedItem = 2;
        }


        private void listVac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var vac = (listVac.SelectedItem as vacancy);

                if (vac != null)
                {
                    var companyInfo = db.selectCompany((int)vac.ID_vac);

                    if (!String.IsNullOrEmpty(companyInfo.First().company.ToString())) txt_companyVac.Text = companyInfo.First().company.ToString();
                    if (!String.IsNullOrEmpty(companyInfo.First().post.ToString())) txt_nameVac.Text = companyInfo.First().post.ToString();
                    if (!String.IsNullOrEmpty(companyInfo.First().salary.ToString())) txt_salaryVac.Text = String.Format("{0:C0}", companyInfo.First().salary);
                    if (!String.IsNullOrEmpty(companyInfo.First().title.ToString())) txt_itemVac.Text = companyInfo.First().name_competencies.ToString();

                }
            }
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboType.SelectedIndex)
            {
                case 0:
                    listVac.ItemsSource = vacancies.ToList();
                    break;
                case 1:
                    using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                    {
                        filteredList = (from x in db.vacancy
                                     orderby x.post
                                     select x).ToList();
                    }
                    listVac.ItemsSource = filteredList.ToList();
                    break;
                case 2:
                    using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                    {
                        filteredList = (from x in db.vacancy
                                     orderby x.salary
                                     select x).ToList();
                    }
                    listVac.ItemsSource = filteredList.ToList();
                    break;
            }
        }

        private void txt_find_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_find.Text))
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    filteredList = (from x in db.vacancy
                                    where x.post.ToLower().Contains(txt_find.Text.ToLower())
                                    select x).ToList();
                }
                listVac.ItemsSource = filteredList.ToList();
            }
            else listVac.ItemsSource = vacancies.ToList();
        }

        private void btnRespons_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    responses _responses = new responses();
                    var vac = (listVac.SelectedItem as vacancy);
                    var sItem = (long)ComboResume.SelectedValue;

                    var companyInfo = db.selectCompany((int)vac.ID_vac);

                    _responses.ID_vacancy = companyInfo.First().vacID;
                    _responses.ID_resume = sItem;
                    var respChecking = (from x in db.responses
                                        where x.ID_resume == _responses.ID_resume && x.ID_vacancy == _responses.ID_vacancy
                                        select x).ToList();
                    if (respChecking.Count <= 0)
                    {
                        db.responses.Add(_responses);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Отлик на эту вакансию с этим резюме уже имеется. Чтобы откликнуться на эту вакансию можете выбрать другое резюме.");
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
