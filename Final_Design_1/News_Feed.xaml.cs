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
            dynamic result = new FacebookClient("EAAE3jEAMPzkBABigoPndSjT8j4c1ctw0Tpve0tA6lIUztWZC5ZBboLUNx8ZCLUtl6haz4qDE9ZBpDoxhRKDkZAcDeT0ZCCuGP2S3EvXjHoLiZCtmRUQBLkKraFErIoHKLDedBKWgbWfguhf7rf2w2pAMJjvKjpfZAsnEkRQ2dTPp0QZDZD");
            return result;
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

            if (!got_posts)
            {
                get_userPosts();
            }

            if(opt == 1)
            {
                post_cntr = post2_cntr + 1;
            }

            bound_postCntr();

            while(post_cntr < result.data.Count && (!post1_set || !post2_set))
            {


                if (!post1_set)
                {
                    set_textbox(post1_status, "");
                    set_textbox(post1_name, "");
                    post1_image1.Source = null;
                    post1_image2.Source = null;
                    if (result.data[post_cntr].ContainsKey("from"))
                        set_textbox(post1_name, result.data[post_cntr].from.name);
                    if (result.data[post_cntr].ContainsKey("message"))
                        set_textbox(post1_status, result.data[post_cntr].message);
                    if (result.data[post_cntr].ContainsKey("attachments"))
                    {
                        if (!result.data[post_cntr].attachments.ContainsKey("subattachments"))
                            set_image(post1_image1, get_singleImageSrc(result.data[post_cntr]));
                    }
                    if (result.data[post_cntr].ContainsKey("attachments"))
                    {
                        if (result.data[post_cntr].ContainsKey("subattachments"))
                        {
                            get_multiImageSrc(result.data[post_cntr], 0);
                            get_multiImageSrc(result.data[post_cntr], 1);
                        }
                    }

                    post1_cntr = post_cntr++;
                    post1_set = true;

                    clear_post2();

                    continue;
                }
                bound_postCntr();
                if (!post2_set)
                {
                    set_textbox(post2_status, "");
                    set_textbox(post2_name, "");
                    post2_image1.Source = null;
                    post2_image2.Source = null;
                    if (result.data[post_cntr].ContainsKey("from"))
                        set_textbox(post2_name, result.data[post_cntr].from.name);
                    if (result.data[post_cntr].ContainsKey("message"))
                        set_textbox(post2_status, result.data[post_cntr].message);
                    if (result.data[post_cntr].ContainsKey("attachments"))
                        if (!result.data[post_cntr].attachments.data[0].ContainsKey("subattachments"))
                            set_image(post2_image1, get_singleImageSrc(result.data[post_cntr]));
                    if (result.data[post_cntr].ContainsKey("attachments"))
                    {
                        if (result.data[post_cntr].attachments.data[0].ContainsKey("subattachments"))
                        {
                            //set_textbox(debug, result.data[post_cntr].attachments.ToString());
                            set_image(post2_image1, get_multiImageSrc(result.data[post_cntr], 0));
                            set_image(post2_image2, get_multiImageSrc(result.data[post_cntr], 1));
                        }
                    }

                    post2_cntr = post_cntr++;
                    post2_set = true;
                }


            }
            bound_postCntr();
        }

        

        private void scroll_backward()
        {
            bool post1_set = false;
            bool post2_set = false;
            post_cntr = post1_cntr - 1;
            bound_postCntr();

            while(post_cntr >= 0 && (!post1_set || !post2_set))
            {
                if (!post2_set)
                {
                    if (result.data[post_cntr].ContainsKey("message"))
                        if (result.data[post_cntr].message != post1_status.Text)
                        {
                            set_textbox(post2_status, result.data[post_cntr].message);
                            if (result.data[post_cntr].ContainsKey("from"))
                                set_textbox(post2_name, result.data[post_cntr].from.name);
                            if (result.data[post_cntr].ContainsKey("attachments"))
                                if (!result.data[post_cntr].attachments.data[0].ContainsKey("subattachments"))
                                    set_image(post2_image1, get_singleImageSrc(result.data[post_cntr]));
                            if (result.data[post_cntr].ContainsKey("attachments"))
                            {
                                if (result.data[post_cntr].attachments.data[0].ContainsKey("subattachments"))
                                {
                                    set_image(post2_image1, get_multiImageSrc(result.data[post_cntr], 0));
                                    set_image(post2_image2, get_multiImageSrc(result.data[post_cntr], 1));
                                }
                            }

                            post2_cntr = post_cntr--;
                            post2_set = true;
                        }
                }
                if (!post1_set)
                {
                    if (result.data[post_cntr].ContainsKey("message"))
                        set_textbox(post1_status, result.data[post_cntr].message);
                    if (result.data[post_cntr].ContainsKey("from"))
                        set_textbox(post1_name, result.data[post_cntr].from.name);
                    if (result.data[post_cntr].ContainsKey("attachments"))
                        if (!result.data[post_cntr].attachments.data[0].ContainsKey("subattachments"))
                            set_image(post1_image1, get_singleImageSrc(result.data[post_cntr]));
                    if (result.data[post_cntr].ContainsKey("attachments"))
                    {
                        if (result.data[post_cntr].attachments.data[0].ContainsKey("subattachments"))
                        {
                            set_image(post1_image1, get_multiImageSrc(result.data[post_cntr], 0));
                            set_image(post1_image2, get_multiImageSrc(result.data[post_cntr], 1));
                        }
                    }
                    post1_cntr = post_cntr--;
                    post1_set = true;
                }
            }
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
                post_cntr = 1;
            }
            if (post_cntr > result.data.Count)
            {
                post_cntr = result.data.Count + 1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Content = main;
        }

        private void expand1_Click(object sender, RoutedEventArgs e)
        {

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

        }
    }
}
