using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using panorama.Resources;
using System.Windows.Media;
using SQLite;
using Windows.Storage;
using System.IO;
using System.Windows.Input;

namespace panorama
{
    public partial class event_result :PhoneApplicationPage
    {
        public SQLiteConnection dbConn;

        public event_result()
        {
            InitializeComponent();
            dbConn = new SQLiteConnection(MainPage.DB_PATH);
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string event_type_search = NavigationContext.QueryString["event_type"];
            string time_search = NavigationContext.QueryString["time"];
            string cost_search = NavigationContext.QueryString["cost"];

            int min_cost = 0, max_cost = 999999;
            if (cost_search == "1")
            {
                min_cost = 0;
                max_cost = 1000;
            }
            else if (cost_search == "2")
            {
                min_cost = 1000;
                max_cost = 5000;
            }
            else if (cost_search == "3")
            {
                min_cost = 5000;
                max_cost = 999999;
            }
            DateTime today = DateTime.Now;
            DateTime min_date =today , max_date = today.AddDays(365);
            
            if (time_search == "1")
            {
                max_date = today.AddDays(3);
            }
            else if (time_search == "2")
            {
                min_date = today.AddDays(3);
                max_date = today.AddDays(7);
            }
            else if (time_search == "3")
            {
                min_date = today.AddDays(7);
            }
            string mindate = min_date.GetDateTimeFormats('d')[0];
            string maxdate = max_date.GetDateTimeFormats('d')[0];
 

            // Retrieve the task list from the database.
            List<my_event_db> retrieved_myevent = dbConn.Table<my_event_db>().ToList<my_event_db>();
            // Clear the list box that will show all the tasks.
            event_list.Children.Clear();

            //List<Event_db> retrieved_allevents = dbConn.Query<Event_db>("select * from Event_db where cost <= " + max_cost.ToString() + " and cost >=" + min_cost.ToString() + " and date <= \"" + maxdate + "\" and date >=\" " + mindate + "\" and Type = \"" + event_type_search + "\" ");// and (id not in retrieved_myevent)
            List<Event_db> retrieved_allevents = dbConn.Query<Event_db>("select * from Event_db where cost <= " + max_cost.ToString() + " and cost >= " + min_cost.ToString() + " and Type = \"" + event_type_search + "\" ");
            var retrieved_allevents1 = new List<Event_db>();
            foreach (var t in retrieved_allevents)
            {
                int flag_for_copy = 0;
                foreach (var s in retrieved_myevent)
                {
                    //       Console.WriteLine(t.Id);
                    if (t.Id == s.event_id)
                    {
                        flag_for_copy = 1;

                        break;
                    }
                }
                if (flag_for_copy == 0)
                {
                    retrieved_allevents1.Add(t);
                }
            }

            foreach (var t in retrieved_allevents1)
            {
                Canvas canvas = new Canvas();
                
                //TextBlock title = new TextBlock();
                //title.Text = t.Type;
                //title.TextAlignment = TextAlignment.Center;

                TextBlock text = new TextBlock();
                text.Text = t.description;
                text.TextAlignment = TextAlignment.Center;

                canvas.Name = t.Id.ToString();
                canvas.Height = 100;
                canvas.Width = 400;
                canvas.Margin = new System.Windows.Thickness(10);
                canvas.Background = new SolidColorBrush(Colors.Blue);
                //               canvas.Tap += event_type_tap;
                //Canvas.SetTop(title, 0);
                //Canvas.SetLeft(title, 0);
                
                Canvas.SetTop(text, 0);
                Canvas.SetLeft(text, 0);

                canvas.Children.Add(text);
                //canvas.Children.Add(title);
                canvas.Tap += canvas_Tap;

                
                event_list.Children.Add(canvas);
            }
        }

        void canvas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var canvas = (sender as Canvas);
            string event_id = canvas.Name;
            event_list.Children.Remove(canvas);
            List<Event_db> retrieved_allevents = dbConn.Query<Event_db>("select * from Event_db where Id=" + event_id);
            foreach (var t in retrieved_allevents)
            {
                var s = dbConn.Insert(new my_event_db()
                {
                    event_id=t.Id,
                    user_id=t.host_id
                });
            }
            if (event_list.Children.Count() == 0)
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
            {
                /// Close the database connection.
                dbConn.Close();
            }
        }
    }
}