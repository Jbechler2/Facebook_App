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
    /// Interaction logic for Photos.xaml
    /// </summary>
    public partial class Photos : UserControl
    {

        public dynamic fb_client()
        {
            dynamic fb = new FacebookClient("EAAE3jEAMPzkBAPfxIGNPuVBKOQ4OwyNsKc7uw5Hn2LQQ0punnTByxqRXwdTvueA0oXW5rV5KkzSZA152HZATdC7F7ZCTKMSdM2PXqshUGd0YTlbFVKjQCQaFCVHidf0YSIz5T8ZBYntABBxnvwIrj0ZAfrSgdivcZD");
            return fb;
        }

        public Photos()
        {
            InitializeComponent();

            get_album_covers();
        }

        private void get_album_covers()
        {
            var fb = fb_client();

            dynamic result = fb.Get("/me/albums?fields=cover_photo.width(800).height(800),source");

            dynamic album = fb.Get("/" + result.data[0].id.ToString() + "?fields=name,picture,photos&fields=source");

            display_cover(album.picture.data.url.ToString());

            text1.Text = result.ToString();
        }

        private void display_cover(string url)
        {
            var image = new Image();
            var fullFilePath = url;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            album1.Source = bitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            base.Content = home;
        }
    }
}
