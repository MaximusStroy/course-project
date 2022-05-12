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
    /// Логика взаимодействия для ResponsesPage.xaml
    /// </summary>
    public partial class ResponsesPage : Page
    {
        List<responses> currentResp = new List<responses>();

        public ResponsesPage()
        {
            InitializeComponent();
            loadResponses();
        }

        private void loadResponses()
        {
            try
            {
                using(RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    listResp.ItemsSource = db.selectResponsesApp(Models.CInfo.id_app);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
