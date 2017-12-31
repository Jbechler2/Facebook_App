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
    /// Interaction logic for NewsFeed.xaml
    /// </summary>
    public partial class NewsFeed : UserControl
    {
        static int x = 0;
        int text1_post = 0;
        int text2_post = 0;
        static bool got_posts = false;
        static dynamic result;
        public int curr_post = 0;

        public NewsFeed()
        {
            InitializeComponent();
            x = 0;
            set_posts(0);
        }

        private void set_image(string image_source, dynamic image_object)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(image_source, UriKind.Absolute);
            bitmap.EndInit();

            image_object.Source = bitmap;
        }

        private void set_text(string text, dynamic text_object)
        {
            text_object.Text = text;
        }

        public dynamic fb_client()
        {
            dynamic result = new FacebookClient("EAAE3jEAMPzkBABigoPndSjT8j4c1ctw0Tpve0tA6lIUztWZC5ZBboLUNx8ZCLUtl6haz4qDE9ZBpDoxhRKDkZAcDeT0ZCCuGP2S3EvXjHoLiZCtmRUQBLkKraFErIoHKLDedBKWgbWfguhf7rf2w2pAMJjvKjpfZAsnEkRQ2dTPp0QZDZD");
            return result;
        }

        private void get_user_posts()//Gets the users posts and stores them in the public variable 'result'
        {
            var fb = fb_client();
            result = fb.Get("/me/posts?fields=message,from,attachments"); //Probably going to need a try-catch to know if it is successful or if we need a new access token

            debug.Text = result.data[0].from.id.ToString();

            //var img_src = fb.Get("/" + result.data[0].from.id.ToString() + "/picture?type=large&redirect=false");

            debug.Text = result.ToString();
            if(result.ContainsKey("face"))
            {
                debug.Text = "HERE WE GOE";
            }
            else
            {
                debug.Text = "FUCK IT UP";
            }

            // set_debugImage(img_src.data.url);

            //set_post1(result.data[0]);

            got_posts = true; //Public variable so we know when posts have been requested and received
        }

        private void set_debugImage(string img_src)
        {
            var image = new Image();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(img_src, UriKind.Absolute);
            bitmap.EndInit();

            image1.Source = bitmap;
        }

        private void set_post1(dynamic post)
        {
            status_preview1.Text = post.message.ToString();
            profile_name1.Text = post.from.name.ToString();

            var fb = fb_client();
            //dynamic image = fb.Get("/" + post.object_id.ToString() + "?fields=source");
            //display_cover(image.source);
        }

        private void set_name1(dynamic post)
        {
            try
            {
                profile_name1.Text = post.from.name.ToString();
            }
            catch
            {

            }
        }

        private void set_message1(dynamic post)
        {
            try
            {
                status_preview1.Text = post.message.ToString();
            }
            catch
            {
                status_preview1.Text = "";
            }
        }

        private void set_name2(dynamic post)
        {
            try
            {
                profile_name2.Text = post.from.name.ToString();
            }
            catch
            {

            }
        }

        private void set_message2(dynamic post)
        {
            try
            {
                status_preview2.Text = post.message.ToString();
            }
            catch
            {
                status_preview2.Text = "";
            }
        }

        private void set_image1(dynamic post)
        {
            try
            {
                string img_src = post.attachments.data[0].media.image.src.ToString();
                //var image = new Image();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(img_src, UriKind.Absolute);
                bitmap.EndInit();

                post1_image1.Source = bitmap;
            }
            catch (Exception ex)
            {
                try
                {
                    string img1_src = post.attachments.data[0].subattachments.data[0].media.image.src.ToString();
                    string img2_src = post.attachments.data[0].subattachments.data[1].media.image.src.ToString();
                    //var iamge = new Image();

                    BitmapImage bitmap1 = new BitmapImage();
                    BitmapImage bitmap2 = new BitmapImage();
                    bitmap1.BeginInit();
                    bitmap2.BeginInit();
                    bitmap1.UriSource = new Uri(img1_src, UriKind.Absolute);
                    bitmap2.UriSource = new Uri(img2_src, UriKind.Absolute);
                    bitmap1.EndInit();
                    bitmap2.EndInit();

                    post1_image1.Source = bitmap1;
                    post1_image1.Source = bitmap2;
                }
                catch
                {
                    post1_image1.Source = null;
                    debug.Text = "set_image1: No images to display\n" + ex.ToString();
                }
                
            }
        }

        private void set_image2(dynamic post)
        {
            try
            {
                string img_src = post.attachments.data[0].media.image.src.ToString();
                //var image = new Image();

                debug.Text = img_src;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(img_src, UriKind.Absolute);
                bitmap.EndInit();

                post1_image1.Source = bitmap;
            }
            catch
            {
                try
                {
                    string img1_src = post.attachments.data[0].subattachments.data[0].media.image.src.ToString();
                    string img2_src = post.attachments.data[0].subattachments.data[1].media.image.src.ToString();
                    //var iamge = new Image();

                    BitmapImage bitmap1 = new BitmapImage();
                    BitmapImage bitmap2 = new BitmapImage();
                    bitmap1.BeginInit();
                    bitmap2.BeginInit();
                    bitmap1.UriSource = new Uri(img1_src, UriKind.Absolute);
                    bitmap2.UriSource = new Uri(img2_src, UriKind.Absolute);
                    bitmap1.EndInit();
                    bitmap2.EndInit();

                    post2_image1.Source = bitmap1;
                    post2_image2.Source = bitmap2;
                }
                catch (Exception ex1)
                {
                    post2_image1.Source = null;
                    post2_image2.Source = null;
                    debug.Text = "set_image2: No images to display\n" + ex1.ToString();
                }

            }
        }

        private void display_cover(string url)
        {
            var image = new Image();
            var fullFilePath = url;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            post1_image1.Source = bitmap;
        }

        private void scroll_backward()
        {
            //debug.Text = "Start: " + x.ToString();

            bool text1_set = false;
            bool text2_set = false;
            x = text1_post - 1;
            bound_x();

            while (x >= 0 && !text2_set)
            {
                try
                {
                    if(result.data[x].message != text1.Text)
                    {
                        set_message2(result.data[x]);
                        set_name2(result.data[x]);
                        set_image2(result.data[x]);
                        text2_post = x;
                        
                        text2_set = true;
                    }
                    x--;
                }
                catch
                {
                    x--;
                }
            }
            while(x >= 0 && !text1_set)
            {
                try
                {
                    set_message1(result.data[x]);
                    set_name1(result.data[x]);
                    set_image1(result.data[x]);
                    text1_post = x;
                    x--;
                    text1_set = true;
                }
                catch
                {
                    x--;
                }
            }
            bound_x();
            //debug.Text += "\nEnd: " + x.ToString();
        }

        private void set_posts(int opt)
        {
            bool text1_set = false;
            bool text2_set = false; //These are to know when the text boxes have text in them

            if (!got_posts)
            {
                get_user_posts();
            }

            if (opt == 1)
            {
                x = text2_post + 1;
            }

            bound_x();
            //debug.Text = "Start: " + x.ToString();

            while (x < result.data.Count && !text1_set){
                try
                {
                    set_message1(result.data[x]);
                    set_name1(result.data[x]);
                    set_image1(result.data[x]);
                    set_profPic1(result.data[x]);
                    text1_post = x;
                    x++;
                    text1_set = true;
                }
                catch
                {
                    x++;
                }
            }
            while(x < result.data.Count && !text2_set)
            {
                try //Try to set text1 to have the text from the first post. If the first post has no message, this will fail
                {
                    set_message2(result.data[x]);
                    set_name2(result.data[x]);
                    set_image2(result.data[x]);
                    set_profPic2(result.data[x]);
                    //string y = result.data[x].message.ToString();
                    text2_post = x;
                    x++;
                    text2_set = true;
                }
                catch //If the try fails, increment x and try with the next one, until either text2 is set or there are no more posts to try
                {
                    x++;
                }
            }
            bound_x();

            //debug.Text += "\nEnd: " + x.ToString();
        }

        private void set_profPic2(dynamic result)
        {
            string id = result.from.id;
            var fb = fb_client();

            var pic = fb.Get("/" + id + "/picture?type=large&redirect=false");
            var img_src = pic.data.url;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(img_src, UriKind.Absolute);
            bitmap.EndInit();

            profile_pic2.Source = bitmap;
        }

        private void set_profPic1(dynamic result)
        {
            string id = result.from.id;
            var fb = fb_client();

            var pic = fb.Get("/" + id + "/picture?type=large&redirect=false");
            var img_src = pic.data.url;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(img_src, UriKind.Absolute);
            bitmap.EndInit();

            profile_pic1.Source = bitmap;

        }

        private void bound_x()
        {
            if (x < 0)
            {
                x = 1;
            }
            if (x > result.data.Count)
            {
                x = result.data.Count + 1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            base.Content = home;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            set_posts(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            scroll_backward();
        }

        private void hide_post2()
        {
            post2_block.Visibility = System.Windows.Visibility.Hidden;
        }

        private void show_post2()
        {
            post2_block.Visibility = System.Windows.Visibility.Visible;
        }

        private void hide_post1()
        {
            post1_block.Visibility = System.Windows.Visibility.Hidden;
        }

        private void show_post1()
        {
            post1_block.Visibility = System.Windows.Visibility.Visible;
        }

        private void expand_text1()
        {
            //text1.Height = text1.Height * 2;
            expand2.Visibility = System.Windows.Visibility.Hidden;
            text2.Visibility = System.Windows.Visibility.Hidden;
            prev.Visibility = System.Windows.Visibility.Hidden;
            next.Visibility = System.Windows.Visibility.Hidden;

            post1_block.Visibility = System.Windows.Visibility.Hidden;


            var win_width = SystemParameters.PrimaryScreenWidth;
            var win_height = SystemParameters.PrimaryScreenHeight;

            text1.Margin = new Thickness(win_width * .8, win_height * .8, 0, 0);
            text1.Width = win_width * .19;
            text1.Height = win_height * .19;
            
            hide_post2();

            ex_Name.Content = profile_name1.Text;
            ex_status.Text = status_preview1.Text;
            ex_image.Source = post1_image1.Source;
            ex_profPic.Source = profile_pic1.Source;

            ex_border.Visibility = System.Windows.Visibility.Visible;

        }

        private void expand_text2()
        {
            text1.Visibility = System.Windows.Visibility.Hidden;
            expand1.Visibility = System.Windows.Visibility.Hidden;
            prev.Visibility = System.Windows.Visibility.Hidden;
            next.Visibility = System.Windows.Visibility.Hidden;

            post2_block.Visibility = System.Windows.Visibility.Hidden;

            var win_width = SystemParameters.PrimaryScreenWidth;
            var win_height = SystemParameters.PrimaryScreenHeight;

            text2.Margin = new Thickness(win_width * .8, win_height * .8, 0, 0);
            text2.Width = win_width * .19;
            text2.Height = win_height * .19;

            ex_Name.Content = profile_name2.Text;
            ex_status.Text = status_preview2.Text;
            ex_image.Source = post2_image1.Source;
            ex_profPic.Source = profile_pic2.Source;

            ex_border.Visibility = System.Windows.Visibility.Visible;

            hide_post1();
        }

        private void minimize_text1()
        {
            //text1.Height = text1.Height / 2;
            expand2.Visibility = System.Windows.Visibility.Visible;
            text2.Visibility = System.Windows.Visibility.Visible;
            prev.Visibility = System.Windows.Visibility.Visible;
            next.Visibility = System.Windows.Visibility.Visible;

            text1.Margin = new Thickness(44, 226, 0, 0);
            text1.Width = 131;
            text1.Height = 165;

            post1_block.Visibility = System.Windows.Visibility.Visible;

            ex_border.Visibility = System.Windows.Visibility.Hidden;

            show_post2();
        }

        private void minimize_text2()
        {
            text1.Visibility = System.Windows.Visibility.Visible;
            expand1.Visibility = System.Windows.Visibility.Visible;
            prev.Visibility = System.Windows.Visibility.Visible;
            next.Visibility = System.Windows.Visibility.Visible;

            text2.Margin = new Thickness(642, 94, 0, 0);

            text2.Width = 137;
            text2.Height = 268;
            
            post2_block.Visibility = System.Windows.Visibility.Visible;

            ex_border.Visibility = System.Windows.Visibility.Hidden;

            show_post1();
        }

        private void expand1_Click(object sender, RoutedEventArgs e)
        {
            expand_text1();
            expand1.Click -= expand1_Click;
            expand1.Click += minimize1_Click;
            expand1.Content = "Minimize";
        }

        private void expand2_Click(object sender, RoutedEventArgs e)
        {
            expand_text2();
            expand2.Click -= expand2_Click;
            expand2.Click += minimize2_Click;
            expand2.Content = "Minimize";
        }

        private void minimize1_Click(object sender, RoutedEventArgs e)
        {
            minimize_text1();
            expand1.Click -= minimize1_Click;
            expand1.Click += expand1_Click;
            expand1.Content = "Expand";
        }

        private void minimize2_Click(object sender, RoutedEventArgs e)
        {
            minimize_text2();
            expand2.Click -= minimize2_Click;
            expand2.Click += expand2_Click;
            expand2.Content = "Expand";
        }
    }
}
