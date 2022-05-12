using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Vacancy.ViewModel
{
    class viewModelClass : DependencyObject
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(viewModelClass), new PropertyMetadata("", FilterText_Changed));
        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();
            var current = d as viewModelClass;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterPerson;
            }
        }
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(viewModelClass), new PropertyMetadata(null));
        public viewModelClass()
        {
            RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();
            Items = CollectionViewSource.GetDefaultView(db.selectVacancy());
            //Items.Filter = FilterPerson;
        }
        private bool FilterPerson(object obj)
        {
            bool res = true;
            Pages.VacanciesPage current = obj as Pages.VacanciesPage;
            if (current != null && !string.IsNullOrWhiteSpace(FilterText))
            {
                res = false;
            }
            return res;
        }
    }
}
