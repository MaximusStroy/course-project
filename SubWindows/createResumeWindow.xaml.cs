using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// Логика взаимодействия для createResumeWindow.xaml
    /// </summary>
    /// 

    class CSkills
    {
        public string skill { get; }
        public string language { get; }

        public CSkills(string _skill, string _lang)
        {
            skill = _skill;
            language = _lang;
        }
    }
    public partial class createResumeWindow : Window
    {
        public createResumeWindow()
        {
            InitializeComponent();
            loadContactInfo();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var mess = MessageBox.Show("Вы хотите выйти?", "Выход", MessageBoxButton.YesNo);
            if (mess == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var mess = MessageBox.Show("Вы уверены что хотите отменить создание вакансии?", "Отменя создания вакансии", MessageBoxButton.YesNo);
            if (mess == MessageBoxResult.Yes)
            {
                txtExperDate_w.Clear();
                txtExperPost_w.Clear();
                txtExperTitle_w.Clear();
                txtLanguages_w.Clear();
                txtNameEducation_w.Clear();
                txtSkills_w.Clear();
                txtSpecialEducation_w.Clear();
                txtSummary_w.Clear();
                txtYearEndEducation_w.Clear();
                txt_birth_w.Clear();
                txt_city_w.Clear();
                txt_lastname_w.Clear();
                txt_mail_w.Clear();
                txt_middlename_w.Clear();
                txt_name_w.Clear();
                txt_numResume.Text = "";
                txt_phone_w.Clear();
            }
        }
        private void loadContactInfo()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var m = new Models.CInfo();

                    var prof = (from a in db.applicants
                                where a.ID_applicant == Models.CInfo.id_app
                                select a).FirstOrDefault();

                    txt_lastname_w.Text = prof.lastname_applicant.ToString();
                    txt_name_w.Text = prof.name_applicant.ToString();
                    txt_middlename_w.Text = prof.middlename_applicant.ToString();
                    txt_birth_w.Text = m.datatimeConvert(prof.birthday_app.ToString());
                    txt_city_w.Text = prof.address_app.ToString();
                    txt_mail_w.Text = prof.email_app.ToString();
                    txt_phone_w.Text = prof.number_phone_app.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void isEnabledTextbox(bool _bool)
        {
            txt_lastname_w.IsEnabled = _bool;
            txt_mail_w.IsEnabled = _bool;
            txt_name_w.IsEnabled = _bool;
            txt_middlename_w.IsEnabled = _bool;
            txt_phone_w.IsEnabled = _bool;
            txt_city_w.IsEnabled = _bool;
            txt_birth_w.IsEnabled = _bool;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения?", "Сохранение изменений", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();

                resume_tab app = new resume_tab();
                if (txtSummary_w.Text != "")
                {
                    app.summary = txtSummary_w.Text.Trim().ToString();
                };
                if (txtExperPost_w.Text != "" && txtExperDate_w.Text != "" && txtExperTitle_w.Text != "")
                {
                    string[] words = txtExperDate_w.Text.Split('-');
                    var workExperience = new
                    {
                        post = txtExperPost_w.Text.Trim().ToString(),
                        working = txtExperTitle_w.Text.Trim().ToString(),
                        yearStart = words[0].Trim().ToString(),
                        yearEnd = words[1].Trim().ToString()
                    };
                    app.workExperience = System.Web.Helpers.Json.Encode(workExperience);
                } else app.workExperience = null;
                if (txtNameEducation_w.Text != "" && txtSpecialEducation_w.Text != "" && txtYearEndEducation_w.Text != "")
                {
                    var jsonEducation = new
                    {
                        placeStudy = txtNameEducation_w.Text.Trim().ToString(),
                        special = txtSpecialEducation_w.Text.Trim().ToString(),
                        yearEnd = txtYearEndEducation_w.Text.Trim().ToString()
                    };
                    app.education = System.Web.Helpers.Json.Encode(jsonEducation);
                };
                if (txtLanguages_w.Text != "" && txtSkills_w.Text != "")
                {
                    object oSkills = new
                    {
                        skill = txtSkills_w.Text.ToString(),
                        language = txtLanguages_w.Text.ToString()
                    };
                    CSkills jsonSkill = new CSkills(txtSkills_w.Text.Trim().ToString(), txtLanguages_w.Text.Trim().ToString());
                    
                    app.skills = System.Web.Helpers.Json.Encode(oSkills);
                     
                }
                app.ID_applicant = Models.CInfo.id_app;
                if (txtSummaryPost.Text != "") {
                    app.req_position = txtSummaryPost.Text.ToString();
                };
                if (txtSummarySalary.Text != "")
                {
                    app.req_salary = Convert.ToDecimal(String.Format("{0:C}", txtSummarySalary.Text));
                };
                db.resume_tab.Add(app);
                db.SaveChanges();

                this.Close();
            }
        }
    }
}