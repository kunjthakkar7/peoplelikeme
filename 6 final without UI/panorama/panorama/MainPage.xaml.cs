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
    public partial class MainPage : PhoneApplicationPage
    {

        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.sqlite"));

        public SQLiteConnection dbConn;
        ListPicker type_of_event;
        TextBox date_input, cost_input, description_input, location_input;
        // Constructor       
        public MainPage()
        {
            InitializeComponent();
/*            EventType_db task = new EventType_db()
            {
                Title = "Adventure",
                Text =  "meet adrenaline junkies like yourself......"
                //CreationDate = DateTime.Now
            };*/
            /// Insert the new task in the Task table.
            dbConn = new SQLiteConnection(DB_PATH);
            dbConn.CreateTable<EventType_db>();
            dbConn.CreateTable<member_db>();
            dbConn.CreateTable<my_event_db>();
            dbConn.CreateTable<Event_db>();

            List<EventType_db> retrieved_myevent = dbConn.Table<EventType_db>().ToList<EventType_db>();
            if (retrieved_myevent.Count() == 0)
            {

                var s = dbConn.Insert(new EventType_db()
                {
                    Title = "Adventure",
                    Text = "meet adrenaline junkies like yourself......"
                    //CreationDate = DateTime.Now
                });
                s = dbConn.Insert(new EventType_db()
                {
                    Title = "Social",
                    Text = "Make new connections, climb the ladder ....."
                    //CreationDate = DateTime.Now
                });
                s = dbConn.Insert(new member_db()
                {
                    name = "Rohit",
                    number = 9652115096,
                    email = "jain.rohit625@gmail.com"
                    //CreationDate = DateTime.Now
                });
                s = dbConn.Insert(new Event_db()
                {
                    host_id = 1,
                    Type = "Social",
                    date = DateTime.Now.GetDateTimeFormats('d')[0],
                    cost = 1200,
                    description = "Halloween themed Party at Hard Rock",
                    location = "Hyderabad"
                });
                s = dbConn.Insert(new Event_db()
                {
                    host_id = 1,
                    Type = "Adventure",
                    date = DateTime.Now.AddDays(2).GetDateTimeFormats('d')[0],
                    cost = 800,
                    description = "Day long Forest Treking",
                    location = "Hyderabad"
                });
                s = dbConn.Insert(new Event_db()
                {
                    host_id = 1,
                    Type = "Social",
                    date = DateTime.Now.GetDateTimeFormats('d')[0],
                    cost = 12000,
                    description = "Charity Function for Orphans",
                    location = "Hyderabad"
                });

                
                               /*
                EventType_db task1 = new EventType_db()
                {
                    Title = "Social",
                    Text = "Make new connections, climb the ladder ....."
                    //CreationDate = DateTime.Now
                };
                /// Insert the new task in the Task table.
                dbConn.Insert(task1);*/
            }
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Create the database connection.
            dbConn = new SQLiteConnection(DB_PATH);
            // Create the table Task, if it doesn't exist.
            dbConn.CreateTable<EventType_db>();
            // Retrieve the task list from the database.
            List<EventType_db> retrieved_event_type = dbConn.Table<EventType_db>().ToList<EventType_db>();
            // Clear the list box that will show all the tasks.
            EventType.Children.Clear();
            //TaskListBox.Items.Clear();
            foreach (var t in retrieved_event_type)
            {
                Canvas canvas = new Canvas();
                TextBlock text = new TextBlock();
                text.Text = t.Title;
                text.TextAlignment = TextAlignment.Center;
                canvas.Name = t.Title;
                canvas.Height = 100;
                canvas.Width = 400;
                canvas.Margin = new System.Windows.Thickness(10);
                canvas.Background = new SolidColorBrush(Colors.Blue);
                canvas.Tap += event_type_tap;
                Canvas.SetTop(text,0);
                Canvas.SetLeft(text, 0);
                canvas.Children.Add(text);
                EventType.Children.Add(canvas);
            }

                        
          
            List<my_event_db> my_events = dbConn.Table<my_event_db>().ToList<my_event_db>();
            
            // Clear the list box that will show all the tasks.
            my_events_stackpanel.Children.Clear();
            //TaskListBox.Items.Clear();
            
            foreach (var t in my_events)
            {
                List<Event_db> event_details = dbConn.Query<Event_db>("select * from Event_db where Id=" + t.event_id.ToString());
                
                foreach (var d in event_details)
                {
                    Canvas canvas = new Canvas();
                    canvas.Height = 100;
                    canvas.Width = 400;
                    canvas.Background = new SolidColorBrush(Colors.Cyan);
                    canvas.Margin = new System.Windows.Thickness(10);
                    
                    
                    
                    TextBlock text = new TextBlock();
                    text.Text = d.description;
                    text.TextAlignment = TextAlignment.Center;
                    canvas.Children.Add(text);
                    my_events_stackpanel.Children.Add(canvas);
                }
                
            }
            add_event.Children.Clear();
            
            type_of_event = new ListPicker();
            type_of_event.Width = 400;
            retrieved_event_type = dbConn.Table<EventType_db>().ToList<EventType_db>();
            type_of_event.Items.Add("--Enter type of event---");
            foreach (var t in retrieved_event_type)
                type_of_event.Items.Add(t.Title);
            add_event.Children.Add(type_of_event);

            date_input = new TextBox();
            date_input.Text = "MM/DD/YY";
            add_event.Children.Add(date_input);

            cost_input = new TextBox();
            cost_input.Text = "Appx. cost for participation";
            add_event.Children.Add(cost_input);

            description_input = new TextBox();
            description_input.Height = 200;
            description_input.Text = "Enter a brief description of the Event";
            add_event.Children.Add(description_input);

            location_input = new TextBox();
            location_input.Height = 150;
            location_input.Text = "Location for Event";
            add_event.Children.Add(location_input);

            Button submit_event = new Button();
            submit_event.Content = "Submit";
            submit_event.Tap += submit_event_Tap;
            add_event.Children.Add(submit_event);
        }

        void submit_event_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var s = dbConn.Insert(new Event_db()
            {
                host_id = 1,
                Type = type_of_event.SelectedItem.ToString(),
                date = date_input.Text,
                cost = Convert.ToInt32(cost_input.Text),
                description = description_input.Text,
                location = location_input.Text
            });
        }


        private void event_type_tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var canvas = (sender as Canvas);

            string event_type = canvas.Name;
            NavigationService.Navigate(new Uri("/event_filters.xaml?event_type="+event_type, UriKind.Relative));
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
            {
                /// Close the database connection.
                dbConn.Close();
            }
        }

/*       private void Insert_Click_1(object sender, RoutedEventArgs e)
        {
            // Create a new task.
            EventType task = new EventType()
            {
                Title = TitleField.Text,
                Text = TextField.Text,
                //CreationDate = DateTime.Now
            };
            /// Insert the new task in the Task table.
            dbConn.Insert(task);
            /// Retrieve the task list from the database.
            List<EventType> retrievedTasks = dbConn.Table<EventType>().ToList<EventType>();
            /// Clear the list box that will show all the tasks.
            TaskListBox.Items.Clear();
            foreach (var t in retrievedTasks)
            {
                TaskListBox.Items.Add(t);
            }
        }*/
    }
 
}