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
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Net;
using System.IO;
using Microsoft.Win32;

namespace Vacancy.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private Dictionary<string, string> person = new Dictionary<string, string>(); 

        public ProfilePage()
        {
            InitializeComponent();
            loadInfo(); 
            person.Add("lastname", txt_lastname.Text.ToString());
            person.Add("name", txt_name.Text.ToString());
            person.Add("middlename", txt_middlename.Text.ToString());
            person.Add("age", txt_age.Text.ToString());
            person.Add("phone", txt_phone.Text.ToString());
            person.Add("mail", txt_mail.Text.ToString());
            person.Add("country", txt_country.Text.ToString());
            _enable(true);
        }

        private void loadInfo()
        {
            try
            {
                using (RecruitmentAgencyEntities db = new RecruitmentAgencyEntities())
                {
                    var m = new Models.CInfo();

                    var profileApp = (from app in db.applicants
                                      where app.ID_applicant == Models.CInfo.id_app
                                      select app).ToList();

                    txt_lastname.Text = profileApp.First().lastname_applicant.ToString();
                    txt_name.Text = profileApp.First().name_applicant.ToString();
                    txt_middlename.Text = profileApp.First().middlename_applicant.ToString();
                    txt_age.Text = m.datatimeConvert(profileApp.First().birthday_app.ToString());
                    txt_country.Text = profileApp.First().address_app.ToString();
                    txt_mail.Text = profileApp.First().email_app.ToString();
                    txt_phone.Text = profileApp.First().number_phone_app.ToString();

                    if (profileApp.First().imageData != null)
                    {
                        byte[] arrayByte = profileApp.First().imageData;

                        MemoryStream memoryStream = new MemoryStream();
                        memoryStream.Write(arrayByte, 0, (int)arrayByte.Length);

                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = memoryStream;
                        image.EndInit();
                        image.Freeze();

                        imgProfile.Source = image;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _enable(bool _bool)
        {
            txt_lastname.IsReadOnly = _bool;
            txt_name.IsReadOnly = _bool;
            txt_middlename.IsReadOnly = _bool;
            txt_phone.IsReadOnly = _bool;
            txt_mail.IsReadOnly = _bool;
            txt_country.IsReadOnly = _bool;
            txt_age.IsReadOnly = _bool;

            btn_change.IsEnabled = _bool;
            btn_saveChanges.IsEnabled = !_bool;
            btn_cancelChanges.IsEnabled = !_bool;
            changeImage.IsEnabled = !_bool;
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            _enable(false);
        }

        private void btn_saveChanges_Click(object sender, RoutedEventArgs e)
        {
            RecruitmentAgencyEntities db = new RecruitmentAgencyEntities();
            try
            {
                if (MessageBox.Show("Сохранить изменения?", "Сохранение изменений", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _enable(true);

                    var app = db.applicants
                        .Where(x => x.ID_applicant == Models.CInfo.id_app)
                        .FirstOrDefault();

                    
                    if (txt_lastname.Text != person["lastname"]) app.lastname_applicant = txt_lastname.Text;
                    if (txt_name.Text != person["name"]) app.name_applicant = txt_name.Text;
                    if (txt_middlename.Text != person["middlename"]) app.middlename_applicant = txt_middlename.Text;
                    if (txt_age.Text != person["age"]) app.birthday_app = DateTime.Parse(txt_age.Text);
                    if (txt_phone.Text != person["phone"]) app.number_phone_app = txt_phone.Text;
                    if (txt_mail.Text != person["mail"]) app.email_app = txt_mail.Text;
                    if (txt_country.Text != person["country"]) app.address_app = txt_country.Text;

                    if (imageLocation != null || imageLocation != "")
                    {
                        byte[] img = null;
                        FileStream fileStream = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                        BinaryReader binaryReader = new BinaryReader(fileStream);
                        img = binaryReader.ReadBytes((int)fileStream.Length);

                        if (app.imageData != img)
                        {
                            app.imageData = img;
                        }
                    }
                }
                else loadInfo();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                db.SaveChanges();
            }
        }

        private void btn_cancelChanges_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите отменить изменения?", "Отмена изменений", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _enable(true);
                loadInfo();
            }
        }

        string imageLocation = "";
        private void changeImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files (*.PNG)|*.PNG|All files(*.*)|(*.*)";
                ofd.InitialDirectory = @"C:\Users\khame\OneDrive\Рабочий стол\Курсовой проект\Картинки";
                ofd.Title = "Выберите фотографию";

                if (ofd.ShowDialog() == true) {
                    imageLocation = ofd.FileName.ToString();
                    imgProfile.Source = new BitmapImage(new Uri(imageLocation));
                }
            }
            catch { MessageBox.Show("Error"); }
        }

        private void txt_age_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}