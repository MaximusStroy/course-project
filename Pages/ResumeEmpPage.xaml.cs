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
    /// Логика взаимодействия для ResumeEmpPage.xaml
    /// </summary>
    public partial class ResumeEmpPage : Page
    {
        List<resume_tab> resumes = new List<resume_tab>();
        List<resume_tab> filteredList = new List<resume_tab>();

        public List<vacancy> comboResumes { get; set; }
        public ResumeEmpPage()
        {
            InitializeComponent();
            openList();
            comboboxUpdate();
        }

        private void openList()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    resumes = (from r in db.resume_tab
                              select r).ToList();
                }
                listRes_e.ItemsSource = resumes;
                listRes_e.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listRes_e_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.CInfo cInfo = new Models.CInfo();
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var res = (resume_tab)listRes_e.SelectedItem;
                try
                {
                    clearScroll();
                    if (res != null)
                    {
                        var infoRes = db.selectResume((int)res.ID_resume);

                        var m = new Models.CInfo();

                        if (null != infoRes.First().lastname)
                            txt_lastname.Text = infoRes.First().lastname.ToString();
                        if (null != infoRes.First().name)
                            txt_name.Text = infoRes.First().name.ToString();
                        if (null != infoRes.First().middlename)
                            txt_middlename.Text = infoRes.First().middlename.ToString();
                        if (null != infoRes.First().birthday)
                            txt_birth.Text = m.datatimeConvert(infoRes.First().birthday.ToString());
                        if (null != infoRes.First().address)
                            txt_city.Text = infoRes.First().address.ToString();
                        if (null != infoRes.First().mail)
                            txt_mail.Text = infoRes.First().mail.ToString();
                        if (null != infoRes.First().phone)
                            txt_phone.Text = infoRes.First().phone.ToString();
                        if (null != infoRes.First().summary)
                            txtSummary.Text = infoRes.First().summary.ToString();
                        if (null != infoRes.First().postWorkEx)
                            txtExperPost.Text = infoRes.First().postWorkEx.ToString();
                        if (null != infoRes.First().working)
                            txtExperTitle.Text = infoRes.First().working.ToString();
                        if (null != infoRes.First().reqSalary)
                            txtSummarySalary.Text = String.Format("{0:C}", infoRes.First().reqSalary);
                        if (null != infoRes.First().reqPost)
                            txtSummaryPost.Text = infoRes.First().reqPost.ToString();
                        if ((null != infoRes.First().yearStartWork) && (null != infoRes.First().yearEndWork))
                        {
                            string yearsWorking = infoRes.First().yearStartWork.ToString() + " - " + infoRes.First().yearEndWork.ToString();
                            txtExperDate.Text = yearsWorking.ToString();
                        }
                        if (null != infoRes.First().education)
                            txtNameEducation.Text = infoRes.First().education.ToString();

                        if (null != infoRes.First().specialEducation)
                            txtSpecialEducation.Text = infoRes.First().specialEducation.ToString();

                        if (null != infoRes.First().yearEndEducation)
                            txtYearEndEducation.Text = infoRes.First().yearEndEducation.ToString();

                        if (null != infoRes.First().lang)
                            txtLanguages.Text = infoRes.First().lang.ToString();

                        if (null != infoRes.First().skill)
                            txtSkills.Text = infoRes.First().skill.ToString();

                        if (null != infoRes.First().resumeID)
                            txt_numResume.Text = infoRes.First().resumeID.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        public void clearScroll()
        {
            txtExperDate.Clear();
            txtExperPost.Clear();
            txtExperTitle.Clear();
            txtLanguages.Clear();
            txtNameEducation.Clear();
            txtSkills.Clear();
            txtSpecialEducation.Clear();
            txtSummary.Clear();
            txtYearEndEducation.Clear();
            txt_birth.Clear();
            txt_city.Clear();
            txt_lastname.Clear();
            txt_mail.Clear();
            txt_middlename.Clear();
            txt_name.Clear();
            txt_numResume.Text = "";
            txt_phone.Clear();
        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboType.SelectedIndex)
            {
                case 0:
                    openList();
                    break;
                case 1:
                    using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                    {
                        filteredList = (from x in db.resume_tab
                                        orderby x.req_position
                                        select x).ToList();
                    }
                    listRes_e.ItemsSource = filteredList.ToList();
                    listRes_e.SelectedIndex = 0;
                    break;
                case 2:
                    using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                    {
                        filteredList = (from x in db.resume_tab
                                        orderby x.req_salary
                                        select x).ToList();
                    }
                    listRes_e.ItemsSource = filteredList.ToList();
                    listRes_e.SelectedIndex = 0;
                    break;
            }
        }
        private void comboboxUpdate()
        {
            RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();
            var item = db.vacancy.Where(x => x.ID_employee == Models.CInfo.id_app).ToList();
            comboResumes = item;
            DataContext = comboResumes;
            ComboResume.ItemsSource = comboResumes.ToList();
            ComboResume.SelectedItem = 2;
        }
        private void txt_find_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_find.Text))
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    filteredList = (from x in db.resume_tab
                                    where x.req_position.ToLower().Contains(txt_find.Text.ToLower())
                                    select x).ToList();
                }
                listRes_e.ItemsSource = filteredList.ToList();
            }
            else openList();
        }
    }

}
