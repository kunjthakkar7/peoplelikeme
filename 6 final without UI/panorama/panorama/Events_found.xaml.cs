using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using panorama.Resources;
using System.Windows.Media;
using SQLite;
using Windows.Storage;
using System.IO;

namespace panorama
{
    public partial class Events_found : PhoneApplicationPage
    {
        public string event_type_search, time_search, cost_search;
        
        public Events_found()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            event_type_search = NavigationContext.QueryString["event_type"];
            time_search = NavigationContext.QueryString["time"];
            cost_search = NavigationContext.QueryString["cost"];
            List<Event_db> retrieved_allevents;
   /*     
            // Retrieve the task list from the database.
            List<my_event_db> retrieved_myevent = dbConn.Table<my_event_db>().ToList<my_event_db>();
            // Clear the list box that will show all the tasks.
            search_result.Children.Clear();

            List<Event_db> retrieved_allevents = dbConn.Query<Event_db>("select * from Event_db where cost <= " + cost_search + " and date <= " + time_search + " and event_type = " + event_type_search);// and (id not in retrieved_myevent) 

            foreach (var t in retrieved_allevents)
            {
                Canvas canvas = new Canvas();
                TextBlock title = new TextBlock();
                title.Text = t.Type;
                title.TextAlignment = TextAlignment.Center;

                TextBlock text = new TextBlock();
                text.Text = t.description;
                title.TextAlignment = TextAlignment.Center;

                canvas.Name = t.Type;
                canvas.Height = 100;
                canvas.Width = 400;
                canvas.Margin = new System.Windows.Thickness(10);
                canvas.Background = new SolidColorBrush(Colors.Blue);
 //               canvas.Tap += event_type_tap;
                Canvas.SetTop(title, 0);
                Canvas.SetLeft(title, 0);
                canvas.Children.Add(title);
                Canvas.SetTop(text, 0);
                Canvas.SetLeft(text, 0);
                canvas.Children.Add(text);
                search_result.Children.Add(canvas);
            }*/
        }
            
    }
}