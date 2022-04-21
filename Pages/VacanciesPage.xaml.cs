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
    /// Логика взаимодействия для VacanciesPage.xaml
    /// </summary>
    public partial class VacanciesPage : Page
    {
        public VacanciesPage()
        {
            InitializeComponent();
            openList();
        }
        public void openList()
        {
            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                listVac.ItemsSource = (from x in db.vacancies
                                       select x).ToList();
            }
        }
    }
}
