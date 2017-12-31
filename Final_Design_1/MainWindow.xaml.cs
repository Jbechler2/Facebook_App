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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public MainWindow()
        {
            InitializeComponent();
            position_elements();
        }

        public dynamic fb_client()
        {
            dynamic result = new FacebookClient("");
            return result;
        }

        private void position_elements()
        {
            var window_width = SystemParameters.PrimaryScreenWidth;
            var window_height = SystemParameters.PrimaryScreenHeight;
            fb_title.Margin = new Thickness(window_width*0.35, window_height*.05, 0, 0);
            notifs.Margin = new Thickness(window_width * .06, window_height * .33, 0, 0);
            prof.Margin = new Thickness(window_width * .41, window_height * .33, 0, 0);
            messages.Margin = new Thickness(window_width * .75, window_height * .33, 0, 0);
            friends.Margin = new Thickness(window_width * .06, window_height * .75, 0, 0);
            newsFeed.Margin = new Thickness(window_width * .41, window_height * .75, 0, 0);
            photos.Margin = new Thickness(window_width * .75, window_height * .75, 0, 0);
            exit_button.Margin = new Thickness(window_width * .85, window_height * .03, 0, 0);

            notifs.Height = window_height * .24;
            notifs.Width = window_width * .2;

            prof.Height = window_height * .24;
            prof.Width = window_width * .2;

            messages.Height = window_height * .24;
            messages.Width = window_width * .2;

            friends.Height = window_height * .24;
            friends.Width = window_width * .2;

            newsFeed.Height = window_height * .24;
            newsFeed.Width = window_width * .2;

            photos.Height = window_height * .24;
            photos.Width = window_width * .2;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            this.Content = profile;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Notifications notifs = new Notifications();
            this.Content = notifs;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            News_Feed news = new News_Feed();
            this.Content = news;
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Photos photos = new Photos();
            this.Content = photos;
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Messaging messaging = new Messaging();
            this.Content = messaging;
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Friends friends = new Friends();
            this.Content = friends;
        }
    }
}
