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
        static dynamic albums;
        public static List<dynamic> album_photos = new List<dynamic>();
        public static List<dynamic> photo_srcs = new List<dynamic>();
        static bool got_photos = false;
        static int photo_cntr = 0;
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
            photo_cntr = 0;
            if (!got_photos)
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

            clear_album1();
            clear_album2();

            if (album_cntr >= albums.data.Count - 1)
            {
                album1_cntr = albums.data.Count - 2;
                album_cntr = albums.data.Count - 2;
                preview_albums();
            }

            bound_albumCntr();

            while (album_cntr < albums.data.Count && (!album1_set || !album2_set))
            {
                if (!album1_set)
                {
                    set_album1(albums.data[album_cntr], album_photos[album_cntr]);
                    album1_cntr = album_cntr++;
                    album1_set = true;
                }

                if (album_cntr >= albums.data.Count)
                    continue;

                if (!album2_set)
                {
                    set_album2(albums.data[album_cntr], album_photos[album_cntr]);
                    album2_cntr = album_cntr++;
                    album2_set = true;
                }
            }

        }

        

        static dynamic active_album = null;

        private void display_photos(dynamic album)
        {
            active_album = album;
            pic1.Source = null;
            pic2.Source = null;

            if (photo_cntr < 0)
                photo_cntr = 0;
            if (photo_cntr >= album.data.Count - 1)
                photo_cntr = album.data.Count - 2;
            if (album.data.Count == 1)
            {
                set_image(pic1, album.data[0].source);
                if (album.data[0].ContainsKey("name"))
                {
                    set_text(pic1_caption, album.data[0].name);
                }

            }
            else
            {
                while (photo_cntr <= album.data.Count && !(pic1.Source != null && pic2.Source != null))
                {
                    if (pic1.Source == null)
                    {
                        set_image(pic1, album.data[photo_cntr].source);
                        if (album.data[photo_cntr].ContainsKey("name"))
                        {
                            set_text(pic1_caption, album.data[photo_cntr].name);
                        }
                        else
                        {
                            set_text(pic1_caption, null);
                        }
                        photo_cntr++;
                    }
                    if (pic2.Source == null)
                    {
                        set_image(pic2, album.data[photo_cntr].source);
                        if (album.data[photo_cntr].ContainsKey("name"))
                        {
                            set_text(pic2_caption, album.data[photo_cntr].name);
                        }
                        else
                        {
                            set_text(pic2_caption, null);
                        }
                        photo_cntr++;
                    }

                }
            }

        }

        private void display_photos2()
        {
            pic1.Source = null;
            pic2.Source = null;

            if (photo_cntr < 0)
                photo_cntr = 0;
            if (photo_cntr >= album_photos[album1_cntr].data.Count - 1)
                photo_cntr = album_photos[album1_cntr].data.Count - 2;
            while (photo_cntr <= album_photos[album1_cntr].data.Count && !(pic1.Source != null && pic2.Source != null))
            {
                if (pic1.Source == null)
                {
                    set_image(pic1, album_photos[album1_cntr].data[photo_cntr++].source);
                }
                if (pic2.Source == null)
                {
                    set_image(pic2, album_photos[album1_cntr].data[photo_cntr++].source);
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

        private void clear_album1()
        {
            album1_cover.Source = null;
            album1_title.Text = "";
        }

        private void clear_album2()
        {
            album2_cover.Source = null;
            album2_title.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            base.Content = home;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            album_cntr = album1_cntr - 2;
            bound_albumCntr();
            preview_albums();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            preview_albums();
        }

        private void scroll_back()
        {
            bool album1_set = false;
            bool album2_set = false;

            album_cntr = album1_cntr - 1;
            bound_albumCntr();

            while (album_cntr > 0 && (!album1_set || !album2_set))
            {
                bound_albumCntr();
                if (!album2_set)
                {
                    set_album2(albums.data[album_cntr], album_photos[album_cntr]);
                    album2_cntr = album_cntr--;
                    album2_set = true;
                }

                if (album_cntr < 0)
                    continue;
                if (!album1_set)
                {
                    set_album1(albums.data[album_cntr], album_photos[album_cntr]);
                    album1_cntr = album_cntr--;
                    album1_set = true;
                }

            }
        }

        private void bound_albumCntr()
        {
            if (album_cntr < 0)
                album_cntr = 0;
            if (album_cntr > albums.data.Count)
                album_cntr = albums.data.Count + 1;
        }

        private void expand1_Click(object sender, RoutedEventArgs e)
        {
            hide(next);
            hide(prev);
            hide(album1);
            hide(expand2);
            hide(album2);
            expand1.Content = "Minimize Album";

            show(pic1);
            show(pic2);
            show(expand_pic1);
            show(expand_pic2);
            show(next_images);
            show(prev_images);

            expand1.Margin = new Thickness(expand1.Margin.Left + 245, expand1.Margin.Top, expand1.Margin.Right, expand1.Margin.Bottom);

            expand1.Click -= expand1_Click;
            expand1.Click += minimize_Click_1;
            display_photos(album_photos[album1_cntr]);
        }

        private void expand2_Click(object sender, RoutedEventArgs e)
        {
            hide(next);
            hide(prev);
            hide(expand1);
            hide(album1);
            hide(album2);
            expand2.Content = "Minimize";

            show(pic1);
            show(pic2);
            show(expand_pic1);
            show(expand_pic2);
            show(next_images);
            show(prev_images);

            expand2.Margin = new Thickness(expand2.Margin.Left - 245, expand2.Margin.Top, expand2.Margin.Right, expand2.Margin.Bottom);

            expand2.Click -= expand2_Click;
            expand2.Click += minimize_Click_2;
            display_photos(album_photos[album2_cntr]);
        }

        private void minimize_Click_1(object sender, RoutedEventArgs e)
        {

            set_text(pic1_caption, null);

            show(next);
            show(prev);
            show(expand2);
            show(album1);
            show(album2);
            expand1.Content = "Expand";

            hide(pic1);
            hide(pic2);
            hide(expand_pic1);
            hide(expand_pic2);
            hide(next_images);
            hide(prev_images);

            photo_cntr = 0;

            expand1.Margin = new Thickness(expand1.Margin.Left - 245, expand1.Margin.Top, expand1.Margin.Right, expand1.Margin.Bottom);

            expand1.Click -= minimize_Click_1;
            expand1.Click += expand1_Click;
        }

        private void minimize_Click_2(object sender, RoutedEventArgs e)
        {
            set_text(pic2_caption, null);

            show(next);
            show(prev);
            show(expand1);
            show(album1);
            show(album2);
            expand2.Content = "Expand";

            hide(pic1);
            hide(pic2);
            hide(expand_pic1);
            hide(expand_pic2);
            hide(next_images);
            hide(prev_images);

            photo_cntr = 0;

            expand2.Margin = new Thickness(expand2.Margin.Left + 245, expand2.Margin.Top, expand2.Margin.Right, expand2.Margin.Bottom);

            expand2.Click -= minimize_Click_2;
            expand2.Click += expand2_Click;
        }

        private void prev_images_Click(object sender, RoutedEventArgs e)
        {
            photo_cntr = photo_cntr - 4;
            display_photos(active_album);
        }

        private void next_images_Click(object sender, RoutedEventArgs e)
        {
            display_photos(active_album);
        }
    }
}
