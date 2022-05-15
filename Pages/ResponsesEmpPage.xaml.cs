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
    /// Логика взаимодействия для ResponsesEmpPage.xaml
    /// </summary>
    public partial class ResponsesEmpPage : Page
    {
        public int numId;
        public ResponsesEmpPage()
        {
            InitializeComponent();
            loadResponsesEmp();
        }
        private void loadResponsesEmp()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var list = db.selectResponsesEmp(Models.CInfo.id_app);
                    listResp.ItemsSource = list.ToList();
                }
                listResp.SelectedIndex = 0;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWellcome_Click(object sender, RoutedEventArgs e)
        {
            listenerButtonWell(true);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            listenerButtonWell(false);
        }

        private void listenerButtonWell(bool _bool)
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var rList = (from r in db.responses
                                 where r.ID_respone == numId
                                 select r).FirstOrDefault();

                    rList.flag = _bool;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                loadResponsesEmp();
            }
        }

        private void listResp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var resp = (selectResponsesEmp_Result)listResp.SelectedItem;
            if (resp != null)
            {
                numId = (int)resp.ID_respone;

                if (resp.flag != null)
                {
                    btnCancel.IsEnabled = false;
                    btnWellcome.IsEnabled = false;
                }
                else
                {

                    btnCancel.IsEnabled = true;
                    btnWellcome.IsEnabled = true;
                }
            }
        }
    }
}
