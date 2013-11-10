using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.LocalizedResources;
using Microsoft.Phone.Shell;


namespace panorama
{
    public partial class event_filters : PhoneApplicationPage
    {
        string current_event_type;
        ListPicker date_list = new ListPicker();
        ListPicker cost_list = new ListPicker();
        public event_filters()
        {
            InitializeComponent();
            
            date_list.Width = 400;
            date_list.Margin = new System.Windows.Thickness(20);
            date_list.Items.Add("any time");
            date_list.Items.Add("within 3 days");
            date_list.Items.Add("within a week");
            date_list.Items.Add("after a week");
            
            cost_list.Width = 400;
            cost_list.Margin = new System.Windows.Thickness(20);
            cost_list.Items.Add("any cost");
            cost_list.Items.Add("less than 1000");
            cost_list.Items.Add("1000-5000");
            cost_list.Items.Add("more than 5000");
            

            Button filter_submit = new Button();
            filter_submit.Content="Go";
            filter_submit.Tap +=filter_submit_Tap;


            filter_stackpanel.Children.Add(date_list);

            filter_stackpanel.Children.Add(cost_list);

            filter_stackpanel.Children.Add(filter_submit);

       }

        void filter_submit_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string date = date_list.SelectedIndex.ToString();
            string cost = cost_list.SelectedIndex.ToString();
            NavigationService.Navigate(new Uri("/event_result.xaml?event_type=" + current_event_type +"&cost="+cost+"&time="+date, UriKind.Relative));
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            current_event_type = NavigationContext.QueryString["event_type"];
        }
    }
}