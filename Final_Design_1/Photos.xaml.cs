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
        public dynamic albums;
        public List<dynamic> album_photos = new List<dynamic>();
        public List<dynamic> photo_srcs = new List<dynamic>();
        static bool got_photos = false;
        static public int album_cntr = 0;
        static public int album1_cntr = 0;
        static public int album2_cntr = 0;

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
        
        private void set_text(dynamic text_object, string text)
        {
            text_object.Text = text;
        }

        private void hide(dynamic visual_object)
        {
            visual_object.Visibility = System.Windows.Visibility.Hidden;
        }

        private void show(dynamic visual_object)
        {
            visual_object.Visibility = System.Windows.Visibility.Visible;
        }

        public Photos()
        {
            InitializeComponent();
            album_cntr = 0;
            get_albums();
            preview_albums();
        }

        private void get_albums()
        {
            if (!got_photos)
            {
                var fb = fb_client();
                albums = fb.Get("/me/albums?fields=name");

                get_photos(fb);
                got_photos = true;
            }
        }

        private void get_photos(dynamic fb)
        {

            for (int i = 0; i < albums.data.Count; i++)
            {
                album_photos.Add(fb.Get("/" + albums.data[i].id + "/photos?fields=source,name"));
            }
        }

        private void preview_albums()
        {
            bool album1_set = false;
            bool album2_set = false;

            while (album_cntr < albums.data.Count && (!album1_set || !album2_set))
            {
                if (!album1_set)
                {
                    set_album1(albums.data[album_cntr], album_photos[album_cntr]);
                    album1_cntr = album_cntr++;
                    album1_set = true;
                }

                if (album_cntr > albums.data.Count)
                    continue;

                if (!album2_set)
                {
                    set_album2(albums.data[album_cntr], album_photos[album_cntr]);
                    album2_cntr = album_cntr++;
                    album2_set = true;
                }
            }
            
        }

        private void set_album1(dynamic album, dynamic photos)
        {
            set_text(album1_title, album.name);
            set_image(album1_cover, photos.data[0].source);
        }

        private void set_album2(dynamic album, dynamic photos)
        {
            set_text(album2_title, album.name);
            set_image(album2_cover, photos.data[0].source);
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

        private void expand1_Click(object sender, RoutedEventArgs e)
        {
            hide(next);
            hide(prev);
            hide(expand2);
            hide(album2);
            expand1.Content = "Minimize";

            expand1.Click -= expand1_Click;
            expand1.Click += minimize_Click_1;
        }

        private void expand2_Click(object sender, RoutedEventArgs e)
        {
            hide(next);
            hide(prev);
            hide(expand1);
            hide(album1);
            expand2.Content = "Minimize";

            expand2.Click -= expand2_Click;
            expand2.Click += minimize_Click_2;
        }

        private void minimize_Click_1(object sender, RoutedEventArgs e)
        {
            show(next);
            show(prev);
            show(expand2);
            show(album2);
            expand1.Content = "Expand";

            expand1.Click -= minimize_Click_1;
            expand1.Click += expand1_Click;
        }

        private void minimize_Click_2(object sender, RoutedEventArgs e)
        {
            show(next);
            show(prev);
            show(expand1);
            show(album1);
            expand2.Content = "Expand";

            expand2.Click -= minimize_Click_2;
            expand2.Click += expand2_Click;
        }
    }
}
