using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LBSToDo.Resources;
using Microsoft.Phone.Tasks;

namespace LBSToDo
{
    public class Locations
    {
        public string Location
        {
            get;
            set;
        }

        public string Region
        {
            get;
            set;
        }

        public string Details
        {
            get;
            set;
        }
    }

    public partial class HomeParanomaPage : PhoneApplicationPage
    {
        List<Locations> lst_Location;

        // Constructor
        public HomeParanomaPage()
        {
            InitializeComponent();
            
            // Set the data context of the listbox control to the sample data
            //DataContext = App.ViewModel;

            lst_Location = new List<Locations>();
            lst_Location.Add(new Locations() { Location = "Location 1", Region = "Karve Nagar", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 2", Region = "Aundh", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 3", Region = "Sanghvi", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 4", Region = "Warje", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 5", Region = "Pune Station", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 6", Region = "Swargate", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 7", Region = "Market Yar78d", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 8", Region = "Yerwada", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 9", Region = "Corporation", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 10", Region = "Deccan", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 11", Region = "Magarpatta", Details = "Task Short Details...." });
            lst_Location.Add(new Locations() { Location = "Location 12", Region = "Hadapsar", Details = "Task Short Details...." });

            this.currentLocationLongListSelector.ItemsSource = lst_Location;

            /*if ((currentLocationLongListSelector.ItemsSource[0] == null))
            {
                locationtodosPanorama.DefaultItem = locationtodosPanorama.Items[1];
            }*/
        }

        // Load data for the ViewModel Items
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
            //if (!App.ViewModel.IsDataLoaded)
            //{
                //App.ViewModel.LoadData();
            //}
        //}

        private void currentLocationLongListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/SelectionChanged.xaml", UriKind.Relative));
        }

        private void WebsiteHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri("http://www.e-zest.net/");
            webBrowserTask.Show();
        }

        private void SupportHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailTask = new EmailComposeTask(); 
            emailTask.To = "shrilesh.kale@e-zest.in";  
            emailTask.Subject = "Subject line text"; 
            emailTask.Body = "Body text goes here...";
            emailTask.Show();
        }

        private void menuAllToDosTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

        private void menuLocationsTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/LocationListView.xaml", UriKind.Relative));
        }

        private void menuSettingsTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.Relative));
        }
    }
}