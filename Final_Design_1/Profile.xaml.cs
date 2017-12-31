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

using Facebook;

namespace Final_Design_1
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {

        public string name;
        public string prof_pic_url;

        public dynamic fb_client()
        {
            dynamic fb = new FacebookClient("EAAE3jEAMPzkBAPfxIGNPuVBKOQ4OwyNsKc7uw5Hn2LQQ0punnTByxqRXwdTvueA0oXW5rV5KkzSZA152HZATdC7F7ZCTKMSdM2PXqshUGd0YTlbFVKjQCQaFCVHidf0YSIz5T8ZBYntABBxnvwIrj0ZAfrSgdivcZD");
            return fb;
        }

        public Profile()
        {
            InitializeComponent();

            get_profile_info();
        }

        private void get_profile_info()
        {
            var fb = fb_client();

            dynamic result = fb.Get("/1560664070659765?fields=picture.height(800).width(800),birthday,about,hometown,location");

            //text1.Text = result.ToString();

            get_profile_pic(result.picture);
            get_hometown(result.hometown);
            get_about_text(result.about);
            get_birthday(result.birthday);
        }

        private void get_birthday(dynamic birthday)
        {
            birthday_label.Content = birthday.ToString();
        }

        private void get_about_text(dynamic about_text)
        {
            about_label.Content = about_text.ToString();
        }

        private void get_hometown(dynamic hometown)
        {
            hometown_label.Content = hometown.name.ToString();
        }

        private void get_profile_pic(dynamic x)
        {
            var image = new Image();
            var fullFilePath = x.data.url/*x.data.url.ToString()*/;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            prof_pic.Source = bitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            base.Content = home;
        }
    }
}
