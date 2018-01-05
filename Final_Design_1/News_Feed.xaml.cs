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
    /// Interaction logic for News_Feed.xaml
    /// </summary>
    public partial class News_Feed : UserControl
    {
        static int post_cntr = 0;
        int post1_cntr = 0;
        int post2_cntr = 0;
        static bool got_posts = false;
        static dynamic result;


        public News_Feed()
        {
            InitializeComponent();
            post_cntr = 0;
            set_posts(0);
        }

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

        private void set_textbox(dynamic text_object, string text)
        {
            text_object.Text = text;
        }

        private void hide_object(dynamic visual_object)
        {
            visual_object.Visibility = System.Windows.Visibility.Hidden;
        }

        private void show_object(dynamic visual_object)
        {
            visual_object.Visibility = System.Windows.Visibility.Visible;
        }

        private void get_userPosts()
        {
            var fb = fb_client();
            result = fb.Get("/me/posts?fields=message,from,attachments");

            got_posts = true;
        }

        private void set_posts(int opt)
        {
            bool post1_set = false;
            bool post2_set = false;

            clear_post1();
            clear_post2();

            if (!got_posts) //If we haven't requested the user's posts, then we do that here.
                get_userPosts();

            if (opt == 1 && !(post_cntr > result.data.Count))
                post_cntr = post2_cntr + 1;

            if(post_cntr >= result.data.Count-1)
            {
                post1_cntr = result.data.Count;
                post_cntr = result.data.Count;
                scroll_backward();
            }

            while (post_cntr < result.data.Count && (!post1_set || !post2_set))
            {
                set_post1(result.data[post_cntr]);
                post1_cntr = post_cntr++;
                bound_postCntr();
                post1_set = true;

                if (post_cntr >= result.data.Count)
                    continue;

                set_post2(result.data[post_cntr]);
                post2_cntr = post_cntr++;
                bound_postCntr();
                post2_set = true;
            }
        }

        private void set_post1(dynamic post)
        {
            if (post.ContainsKey("from"))
                set_textbox(post1_name, post.from.name);
            if (post.ContainsKey("message"))
                set_textbox(post1_status, post.message);
            if (post.ContainsKey("attachments"))
            {
                if (post.attachments.data[0].ContainsKey("subattachments"))
                {
                    set_image(post1_image1, get_multiImageSrc(post, 0));
                    set_image(post1_image2, get_multiImageSrc(post, 1));
                }
                else
                {
                    set_image(post1_image1, get_singleImageSrc(post));
                }
            }
        }

        private void set_post2(dynamic post)
        {
            if (post.ContainsKey("from"))
                set_textbox(post2_name, post.from.name);
            if (post.ContainsKey("message"))
                set_textbox(post2_status, post.message);
            if (post.ContainsKey("attachments"))
            {
                if (post.attachments.data[0].ContainsKey("subattachments"))
                {
                    set_image(post2_image1, get_multiImageSrc(post, 0));
                    set_image(post2_image2, get_multiImageSrc(post, 1));
                }
                else
                {
                    set_image(post2_image1, get_singleImageSrc(post));
                }
            }
        }

        private void scroll_backward()
        {

            bool post1_set = false;
            bool post2_set = false;

            post_cntr = post1_cntr - 1;
            bound_postCntr();
            
            clear_post1();
            clear_post2();

            if(post_cntr <= 0)
            {
                post_cntr = 0;
                set_posts(0);
                return;
            }

            while(post_cntr > 0 && (!post1_set || !post2_set))
            {
                set_post2(result.data[post_cntr]);
                post2_cntr = post_cntr--;
                bound_postCntr();
                post2_set = true;

                set_post1(result.data[post_cntr]);
                post1_cntr = post_cntr;
                bound_postCntr();
                post1_set = true;
            }
        }

        private void clear_post1()
        {
            post1_profPic.Source = null;
            set_textbox(post1_name, "");
            set_textbox(post1_status, "");
            post1_image1.Source = null;
            post1_image2.Source = null;
        }

        private void clear_post2()
        {
            post2_profPic.Source = null;
            set_textbox(post2_name, "");
            set_textbox(post2_status, "");
            post2_image1.Source = null;
            post2_image2.Source = null;
        }

        private string get_multiImageSrc(dynamic post, int img_num)
        {
            string img_src = post.attachments.data[0].subattachments.data[img_num].media.image.src;
            return img_src;
        }

        private string get_singleImageSrc(dynamic post)
        {
            string img_src = post.attachments.data[0].media.image.src;
            return img_src;
        }

        private void bound_postCntr()
        {
            if (post_cntr < 0)
            {
                post_cntr = 0;
            }
            if (post_cntr >= result.data.Count)
            {
                post_cntr = result.data.Count + 1;
            }
        }

        private void expand_post1()
        {
            clear_ex();

            ex_Name.Content = post1_name.Text;
            ex_status.Text = post1_status.Text;
            if (post1_image1.Source != null)
                set_image(ex_image, post1_image1.Source.ToString());
            show_object(ex_grid);
        }

        private void expand_post2()
        {
            clear_ex();

            ex_Name.Content = post2_name.Text;
            ex_status.Text = post2_status.Text;
            if (post2_image1.Source != null)
                set_image(ex_image, post2_image1.Source.ToString());
            show_object(ex_grid);
        }

        private void clear_ex()
        {
            ex_Name.Content = "";
            ex_status.Text = "";
            ex_image.Source = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            base.Content = main;
        }

        private void expand1_Click(object sender, RoutedEventArgs e)
        {
            hide_object(post2_block);
            hide_object(expand2);
            expand1.Click -= expand1_Click;
            expand1.Click += minimize1_Click;
            expand1.Content = "Minimize";
            hide_object(post1_block);

            expand_post1();

        }

        private void minimize1_Click(object sender, RoutedEventArgs e)
        {
            show_object(post2_block);
            show_object(expand2);
            show_object(post1_block);
            expand1.Click -= minimize1_Click;
            expand1.Click += expand1_Click;
            expand1.Content = "Expand";

            hide_object(ex_grid);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            scroll_backward();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            set_posts(1);
        }

        private void expand2_Click(object sender, RoutedEventArgs e)
        {
            hide_object(post1_block);
            hide_object(post2_block);
            hide_object(expand1);
            expand2.Click -= expand2_Click;
            expand2.Click += minimize2_Click;
            expand2.Content = "Minimize";

            expand_post2();
        }

        private void minimize2_Click(object sender, RoutedEventArgs e)
        {
            show_object(post1_block);
            show_object(post2_block);
            show_object(expand1);
            expand2.Click -= minimize2_Click;
            expand2.Click += expand2_Click;
            expand2.Content = "Expand";

            hide_object(ex_grid);
        }
    }
}
