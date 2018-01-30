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
using System.Dynamic;

using Facebook;

namespace Final_Design_1
{
    /// <summary>
    /// Interaction logic for Messaging_New.xaml
    /// </summary>
    public partial class Messaging_New : UserControl
    {
        //private static int msg_count;
        private int t1_cntr = 0;
        private int t2_cntr = 0;
        private int exp_index = 0;
        static dynamic conversations;
        static List<dynamic> messages = new List<dynamic>();
        static List<dynamic> convoIDs = new List<dynamic>();
        static List<dynamic> messageIDs = new List<dynamic>();
        private dynamic page_token;


        public Messaging_New()
        {
            InitializeComponent();
            get_page_token();
            get_conversations(page_token);
            display_names();
        }

        private dynamic fb_client()
        {
            MainWindow mw = new MainWindow();
            return mw.fb_client();
        }

        private void get_page_token()
        {
            var fb = fb_client();
            var result = fb.Get("/me/accounts");

            page_token = result.data[0].access_token.ToString();
        }

        private void get_conversations(string access_token)
        {
            var fb = new FacebookClient(access_token);
            conversations = fb.Get("/me/conversations?fields=participants");

            get_convoIDs();
        }

        private void get_convoIDs()
        {
            for(int i = 0; i < conversations.Count; i++)
            {
                convoIDs.Add(conversations.data[i].id.ToString());
            }

            get_messages();
        }

        private void get_messages()
        {
            var fb = new FacebookClient(page_token);

            for (int i = 0; i < convoIDs.Count; i++)
            {
                messages.Add(fb.Get("/" + convoIDs[i] + "/messages?fields=message,from"));
            }
        }

        private void display_names()
        {
            for(int i = 0; i < conversations.Count; i++)
            {
                if (t1_name.Text == "")
                {
                    t1_name.Text = conversations.data[i].participants.data[0].name;
                    display_recentMessage("t1", i);
                    t1_cntr = i;
                }
                else
                {
                    if (t2_name.Text == "")
                    {
                        t2_name.Text = conversations.data[i].participants.data[0].name;
                        display_recentMessage("t2", i);
                        t2_cntr = i;
                    }
                }
            }
        }

        private void display_recentMessage(string thread, int t_cntr)
        {
            if(thread == "t1")
            {
                t1_recentMessage.Text = messages[t_cntr].data[0].message;
            }
            if(thread == "t2")
            {
                t2_recentMessage.Text = messages[t_cntr].data[0].message;
            }
        }

        private void show_expand(int thread)
        {
            if(thread == 1)
            {
                expand_name1.Text = t1_name.Text;
                for(int i = messages[t1_cntr].data.Count-1; i >= 0; i--)
                {
                    msgs.Text += messages[t1_cntr].data[i].from.name + ": " + messages[t1_cntr].data[i].message + "\n";
                }
                exp_index = thread;
            }
            if(thread == 2)
            {
                expand_name1.Text = t2_name.Text;
                for(int i = messages[t2_cntr].data.Count-1; i >= 0; i--)
                {
                    msgs.Text += messages[t2_cntr].data[i].from.name + ": " + messages[t2_cntr].data[i].message + "\n";
                }
                exp_index = thread;
            }
        }

        private void clear_expand()
        {
            expand_name1.Text = String.Empty;
            msgs.Text = String.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            base.Content = home;
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            expandView.Visibility = System.Windows.Visibility.Hidden;

            clear_expand();

            threads.Visibility = System.Windows.Visibility.Visible;
        }

        private void t1_expand_Click(object sender, RoutedEventArgs e)
        {
            threads.Visibility = System.Windows.Visibility.Hidden;

            show_expand(1);

            expandView.Visibility = System.Windows.Visibility.Visible;
        }

        private void t2_expand_Click(object sender, RoutedEventArgs e)
        {
            threads.Visibility = System.Windows.Visibility.Hidden;

            show_expand(2);

            expandView.Visibility = System.Windows.Visibility.Visible;
        }

        private void reply_Click(object sender, RoutedEventArgs e)
        {
            var fb = fb_client();

            dynamic message = new ExpandoObject();
            message.message = response.Text;

            fb.Post(conversations.data[exp_index].id, message);
            response.Text = String.Empty;
        }
    }
}
