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

namespace Vacancy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
            {
                var r = (from e in db.applicants
                         select e).ToList();
            }

            frame.Navigate(new Pages.SignUpAndLogInPage());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                item.Close();
            }
        }
    }

}
