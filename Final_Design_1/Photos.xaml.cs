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
            MainWindow mw = new MainWindow();
            return mw.fb_client();
        }

        private void set_image(dynamic image_object, string image_source)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(image_source, UriKind.Absolute);
            bitmap.EndInit();

            image_object.Source = bitmap;
        }

        public Photos()
        {
            InitializeComponent();

            get_album_covers();
        }

        private void get_album_covers()
        {
            var fb = fb_client();

            dynamic result = fb.Get("/me/albums?fields=cover_photo.width(1000).height(1000),source,name");

            dynamic album = fb.Get("/" + result.data[0].id.ToString() + "?fields=name,picture.type(album),photos&fields=source");

            set_image(album1_cover, album.picture.data.url);
            album1_title.Text = result.data[0].name;

            text1.Text = result.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            base.Content = home;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
