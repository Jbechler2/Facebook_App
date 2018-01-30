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
    /// Interaction logic for Messaging.xaml
    /// </summary>
    public partial class Messaging : UserControl
    {
        private static int count;
        static dynamic result;
        static dynamic conversations;
        static List<dynamic> messages = new List<dynamic>();
        int thread1_index;
        int thread2_index;
        int thread3_index; //These indexes are update whenever the buttons on the left are updated. The indexes allow for conversations to be pulled from the coversation data structure instantaneously.

        /*This is simply a shortcut for creating a Facebook Client connection. It opens a connection to a user's personal page by using a 
         user access token*/
        public dynamic fb_client()
        {
            MainWindow mw = new MainWindow();
            return mw.fb_client();
        }

        public Messaging()
        {
            InitializeComponent();
            get_conversations(get_page_token()); //Gets the conversation info from a page associated with a user's account. The argument is the page access token.
            display_threads();
        }

        private void get_messages()
        {

        }

        private void display_threads()
        {
            int j = 0;
            foreach(Control ctrl in threads.Children)
            {
                if(ctrl.GetType() == typeof(Button) && j < conversations.data.Count)
                {
                    if (conversations.data[j].ContainsKey("participants"))
                    {
                        ((Button)ctrl).Content = conversations.data[j].participants.data[0].name;
                        update_threadIndex((Button)ctrl, j++);
                    }
                }
            }
        }

        private void update_threadIndex(Control thread, int index)
        {
            if (thread.Name == "thread1")
                thread1_index = index;
            if (thread.Name == "thread2")
                thread2_index = index;
            if (thread.Name == "thread3")
                thread3_index = index;
        }

        /*Opens a connection to the user's profile and gets and returns the page access token in string form*/
        private string get_page_token()
        {
            string page_token = "";
            var fb = fb_client();

            result = fb.Get("/me/accounts");

            //text1.Text = result.data[0].access_token.ToString();

            page_token = result.data[0].access_token.ToString();

            return page_token;
        }


        /*Given the page access token, pulls the conversation info, including the participants in the conversation and the message IDs*/
        private void get_conversations(string access_token)
        {
            var fb = new FacebookClient(access_token);

            conversations = fb.Get("/me/conversations?fields=participants,messages");

            //text1.Text = result.ToString();

           //for (int i = 0; i < result.data.Count; i++)
           // {
           //     display_participants(result.data[i]);
           //    display_messages(result.data[i].id);
           //}
        }

        /*Given the id of a conversation, pulls the related information about messages including the messages themselves and who the message was sent by. It then displays the messages
         in their respective text boxes*/
        private void display_messages(dynamic conv_id)
        {
            //messages1.Text += conv_id.ToString() + "\n";

            var fb = new FacebookClient(get_page_token());


            if (count == 0)
            {
                dynamic result = fb.Get("/" + conv_id.ToString() + "/messages?fields=message,from");
                //text1.Text = result.ToString();
                for (int i = 0; i < result.data.Count; i++)
                {
                    //messages1.Text += result.data[i].from.name.ToString() + ": " + result.data[i].message.ToString() + "\n";
                }
                count++;
            }
            else if (count == 1)
            {
                dynamic result = fb.Get("/" + conv_id.ToString() + "/messages?fields=message,from");
                //messages1.Text = result.ToString();
                for (int i = 0; i < result.data.Count; i++)
                {
                    //messages2.Text += result.data[i].from.name.ToString() + ": " + result.data[i].message.ToString() + "\n";
                }
                count++;
            }
        }


        /*Given the data object associated with a conversation, determines who the conversation is with and displays their name above
         the respective text box*/
        private void display_participants(dynamic conv_data)
        {
            //text1.Text += conv_data.ToString() + "\n";

            if (count == 0)
            {
                //conv1.Content = conv_data.participants.data[0].name;
            }
            else if (count == 1)
            {
                //conv2.Content = conv_data.participants.data[0].name;
            }
        }

        private void get_messages(int index)
        {
            for(int i = 0; i < result.data[index].messages.data.Count; i++)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            count = 0;
            MainWindow home = new MainWindow();
            base.Content = home;
        }

        private void thread1_Click(object sender, RoutedEventArgs e)
        {
            get_messages(thread1_index);
        }
    }
}

