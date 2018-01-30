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


            dynamic result = new FacebookClient("EAAE3jEAMPzkBABigoPndSjT8j4c1ctw0Tpve0tA6lIUztWZC5ZBboLUNx8ZCLUtl6haz4qDE9ZBpDoxhRKDkZAcDeT0ZCCuGP2S3EvXjHoLiZCtmRUQBLkKraFErIoHKLDedBKWgbWfguhf7rf2w2pAMJjvKjpfZAsnEkRQ2dTPp0QZDZD");
            return result;

        }

        /* Posistion_elements()
		 * 
		 * Sets the system parameters for buttons and and text buttons
		 * to ensure that across all systems the buttons are uniform
		 * regardless of screen size
		 * 
		 * Variables defined:
		 * window_width - the size of the system's width
		 * window_height - the size of the system's height
		 * Margin - placement on the screen
		 * 
		 * nothing is returned
		 *
		 **/


        private void position_elements()
        {
            var window_width = SystemParameters.PrimaryScreenWidth;
            var window_height = SystemParameters.PrimaryScreenHeight;
            fb_title.Margin = new Thickness(window_width * 0.35, window_height * .05, 0, 0);
            notifs.Margin = new Thickness(window_width * .10, window_height * .25, 0, 0); //.06, .33
            prof.Margin = new Thickness(window_width * .41, window_height * .25, 0, 0); //.41, .33
            messages.Margin = new Thickness(window_width * .72, window_height * .25, 0, 0); //.75, .33
            friends.Margin = new Thickness(window_width * .10, window_height * .66, 0, 0); //.06, .75
            newsFeed.Margin = new Thickness(window_width * .41, window_height * .66, 0, 0); //.41, .75
            photos.Margin = new Thickness(window_width * .72, window_height * .66, 0, 0); //.75, .75
            exit_button.Margin = new Thickness(window_width * .85, window_height * .03, 0, 0);

            notifs.Height = window_height * .30;
            notifs.Width = window_width * .18;

            prof.Height = window_height * .30;  //.24
            prof.Width = window_width * .18;     //.2

            messages.Height = window_height * .30;
            messages.Width = window_width * .18;

            friends.Height = window_height * .30;
            friends.Width = window_width * .18;

            newsFeed.Height = window_height * .30;
            newsFeed.Width = window_width * .18;

            photos.Height = window_height * .30;
            photos.Width = window_width * .18;



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
            Messaging_New messaging = new Messaging_New();
            this.Content = messaging;
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Friends friends = new Friends();
            this.Content = friends;
        }
    }
}
