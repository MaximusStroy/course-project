using Newtonsoft.Json;
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
    /// Логика взаимодействия для ResumeAppPage.xaml
    /// </summary>
    public partial class ResumeAppPage : Page
    {
        List<resume_tab> resumes = new List<resume_tab>();

        public static SubWindows.createResumeWindow createResume;
        
        public ResumeAppPage()
        {
            InitializeComponent();
            openList();
            isEnabledTextbox(true);
        }
        private void isEnabledTextbox(bool _bool)
        {
            txtExperDate.IsReadOnly = _bool;
            txtExperPost.IsReadOnly = _bool;
            txtExperTitle.IsReadOnly = _bool;
            txtLanguages.IsReadOnly = _bool;
            txtNameEducation.IsReadOnly = _bool;
            txtSkills.IsReadOnly = _bool;
            txtSpecialEducation.IsReadOnly = _bool;
            txtSummary.IsReadOnly = _bool;
            txtSummaryPost.IsReadOnly = _bool;
            txtSummarySalary.IsReadOnly = _bool;
            txtYearEndEducation.IsReadOnly = _bool;

            btnChangeCancel.IsEnabled = !_bool;
            btnChangeSave.IsEnabled = !_bool;
            btnChangeResume.IsEnabled = _bool;
        }
        public void openList()
        {
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                resumes = (from x in db.resume_tab
                           join ap in db.applicants on x.ID_applicant equals ap.ID_applicant
                           join us in db.users on ap.ID_user equals us.ID_user
                           where us.ID_user == Models.CInfo.status
                           select x).ToList() ;
            }
            listRes.ItemsSource = resumes;
            listRes.SelectedIndex = 0;
        }
        private void listRes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.CInfo cInfo = new Models.CInfo();
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var res = (resume_tab)listRes.SelectedItem;
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
        

        private void btnDeleteResume_Click(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("Вы уверены что хотите удаить резюме?", "Удаление резюме", MessageBoxButton.OKCancel);
            if (a == MessageBoxResult.OK)
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var resDel = (resume_tab)listRes.SelectedItem;

                    db.dropResume((int)resDel.ID_resume);

                    clearScroll();
                    openList();
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

        private void btnCreateResume_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in App.Current.Windows)
            {
                if (w is SubWindows.createResumeWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                createResume = new SubWindows.createResumeWindow();
                createResume.Show();
            }
            else openList();
        }

        private void btnChangeResume_Click(object sender, RoutedEventArgs e)
        {
            isEnabledTextbox(false);
        }

        private string convertMoneyToStr(string s)
        {
            string result = "";
            bool flag = true;
            for (int i = 0; i < s.Length; i++)
            {

                if (s[i] != ' ')
                {
                    result += s[i];
                    if (s[i] == ',')
                    {
                        break;
                        flag = false;
                    }
                }
                if (false == flag) break;
            }

            return result;
        }

        private void btnChangeSave_Click(object sender, RoutedEventArgs e)
        {
            Models.CInfo cInfo = new Models.CInfo();
            try
            {
                if (MessageBox.Show("Сохранить изменения?", "Сохранение изменений", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    isEnabledTextbox(true);

                    var resInfo = (resume_tab)listRes.SelectedItem;

                    RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();
                    var app = db.resume_tab
                        .Where(x => x.ID_resume == resInfo.ID_resume)
                        .FirstOrDefault();

                    if (txtSummary.Text != "")
                    {
                        app.summary = txtSummary.Text.Trim().ToString();
                    };
                    if (txtExperPost.Text != "" && txtExperDate.Text != "" && txtExperTitle.Text != "")
                    {
                        string[] words = txtExperDate.Text.Split('-');
                        var workExperience = new
                        {
                            post = txtExperPost.Text.Trim().ToString(),
                            working = txtExperTitle.Text.Trim().ToString(),
                            yearStart = words[0].Trim().ToString(),
                            yearEnd = words[1].Trim().ToString()
                        };
                        app.workExperience = System.Web.Helpers.Json.Encode(workExperience);
                    }
                    else app.workExperience = null;
                    if (txtNameEducation.Text != "" && txtSpecialEducation.Text != "" && txtYearEndEducation.Text != "")
                    {
                        var jsonEducation = new
                        {
                            placeStudy = txtNameEducation.Text.Trim().ToString(),
                            special = txtSpecialEducation.Text.Trim().ToString(),
                            yearEnd = txtYearEndEducation.Text.Trim().ToString()
                        };
                        app.education = System.Web.Helpers.Json.Encode(jsonEducation);
                    };
                    if (txtLanguages.Text != "" && txtSkills.Text != "")
                    {/*
                        dynamic langDyn = System.Web.Helpers.Json.Encode();
                        dynamic skillDyn = System.Web.Helpers.Json.Encode();
                        */
                        var jsonSkills = new
                        {
                            language = txtLanguages.Text.Trim().ToString(),
                            skill = txtSkills.Text.Trim().ToString()
                        };
                        app.skills = System.Web.Helpers.Json.Encode(jsonSkills);
                    }
                    if (txtSummaryPost.Text != "")
                    {
                        app.req_position = txtSummaryPost.Text.ToString();
                    };
                    if (txtSummarySalary.Text != "")
                    {
                        string str = convertMoneyToStr(String.Format("{0:C}", txtSummarySalary.Text));
                        app.req_salary = Convert.ToDecimal(str);
                    };

                    db.SaveChanges();
                    openList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { openList(); }
        }
    }
}
