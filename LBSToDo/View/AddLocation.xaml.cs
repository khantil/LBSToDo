using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LBSToDo.Resources;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Maps.Services;
using System.Diagnostics;
using LBSToDo.Model;
namespace LBSToDo
{
    public partial class AddLocation : PhoneApplicationPage
    {
       

        // Reverse geocode query
        private ReverseGeocodeQuery MyReverseGeocodeQuery = null;
        
        public static GeoCoordinate LocationResult = null;
        public static string LocationAddress;
        string searchText;
        /// <summary>
        /// Accuracy of my current location in meters;
        /// </summary>
    //    private double _accuracy = 0.0;
        // Constructor
        public AddLocation()
        {
            InitializeComponent();


            //Map MyMap = new Map();
            //MyMap.Center = new GeoCoordinate(47.6097, -122.3331);
            //MyMap.ZoomLevel = 10;
            //ContentPanel.Children.Add(MyMap);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

       
        protected  override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                 //  return;
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }

            if (null != locationNameResult)
            {             
                txtBlockLocation.Text = locationNameResult;
            }
       

        }


        private async void listPickerSetLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != this.listPickerSetLocation)
            {
                if (null != this.listPickerSetLocation.SelectedItem)
                {
                    string selectedItem = (string)this.listPickerSetLocation.SelectedItem;

                    Debug.WriteLine(selectedItem);
                    if ("Pin on Map".Equals(selectedItem))
                        NavigationService.Navigate(new Uri("/View/PinLocation.xaml", UriKind.Relative));

                    if ("Set my current location".Equals(selectedItem))
                    {
                        txtBlockLocation.Text = "";
                        progressbarCurentLocation.IsIndeterminate = true;
                        Debug.WriteLine("OnNavigatedTo():-1>Declear the Task");
                        Task<String> getCurrentLocationTask = getCurrentLocation();
                        Debug.WriteLine("OnNavigatedTo():-2>await the Task");
                        String taskResult = await getCurrentLocationTask;
                        Debug.WriteLine("OnNavigatedTo():-3>Task completed");
                    }
                }
            }
        }

        public async Task<String>  getCurrentLocation()
        {
             Debug.WriteLine("getCurrentLocation():- 1> Start");
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
                     
            try
            {
                Debug.WriteLine("getCurrentLocation():- 1> await for current latlong");
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(0.5),
                    timeout: TimeSpan.FromSeconds(5)
                    );
                Debug.WriteLine("getCurrentLocation():- 1> got the current latlong");
               // txtBlockLocation.Text = "Latitude:" + geoposition.Coordinate.Latitude.ToString("0.00") + " Longitude:" + geoposition.Coordinate.Longitude.ToString("0.00");

                LocationResult = new GeoCoordinate(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);

                if (MyReverseGeocodeQuery == null || !MyReverseGeocodeQuery.IsBusy)
                {
                    MyReverseGeocodeQuery = new ReverseGeocodeQuery();
                    MyReverseGeocodeQuery.GeoCoordinate = new GeoCoordinate(LocationResult.Latitude, LocationResult.Longitude);
                    MyReverseGeocodeQuery.QueryCompleted += ReverseGeocodeQuery_QueryCompleted;
                    Debug.WriteLine("getCurrentLocation():- 1> Start Reverse Geocoding");
                    MyReverseGeocodeQuery.QueryAsync();
                    Debug.WriteLine("getCurrentLocation():- 1> Completed Reverse Geocoding");
                    
                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    txtBlockLocation.Text = txtBlockLocation.Text + "Status: " + "Location  is disabled in phone settings";
                }                
            }
            return "";
        }
 
            /// <summary>
        /// Event handler for reverse geocode query.
        /// </summary>
        /// <param name="e">Results of the reverse geocode query - list of locations</param>
        private void ReverseGeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                if (e.Result.Count > 0)
                {
                    Debug.WriteLine("ReverseGeocodeQuery_QueryCompleted():- 1> Start Reverse Geocoding");
                    String addressTxt="";
                    MapAddress address = e.Result[0].Information.Address;
                     if (!"".Equals( address.BuildingRoom))
                    {
                        addressTxt += address.BuildingRoom;
                    }

                    if (!"".Equals(address.BuildingFloor))
                    {
                        addressTxt += ", " +address.BuildingFloor;
                    }
                     if(!"".Equals( address.BuildingName))
                    {
                        addressTxt += ", "+address.BuildingName;
                    }
                     if (!"".Equals(address.BuildingName))
                    {
                        addressTxt += ", " + address.BuildingName;
                    }
                    if (!"".Equals(address.Street))
                    {
                        addressTxt += ", " + address.Street;
                    }
                    if (!"".Equals(address.District))
                    {
                        addressTxt += ", " + address.District;
                    }
                     if (!"".Equals(address.City))
                    {
                        addressTxt += ", " + address.City;
                    }                 
                     if (!"".Equals(address.State))
                    {
                        addressTxt += ", " + address.State;
                    }
                     if (!"".Equals(address.Country))
                    {
                        addressTxt += ", " + address.Country;
                    }
                     if (addressTxt.StartsWith(","))
                     {
                         addressTxt = addressTxt.Substring(1);
                     }                     
                    txtBlockLocation.Text = addressTxt.Trim();
                    progressbarCurentLocation.IsIndeterminate = false;
                    LocationAddress = addressTxt;
                }

                Debug.WriteLine("ReverseGeocodeQuery_QueryCompleted():- 1> Completed Reverse Geocoding");
            }
        }


        private void deleteLocationApplicationBar_Click(object sender, EventArgs e)
        {

        }

        private void SaveLocationApplicationBar_Click(object sender, EventArgs e)
        {
           
            if (!progressbarCurentLocation.IsIndeterminate)
            {
                if ("".Equals(txtboxLocationUserName.Text))
                {
                    MessageBox.Show("Please enter the Location name!!");
                }
                else if (null == LocationResult && null != LocationAddress)
                {
                    MessageBox.Show("Please select the the Location!!");
                }
                else
                {
                    MessageBox.Show(txtboxLocationUserName.Text + " at " + LocationAddress+ " is saved");

                    LocationItem newToDoItem = new LocationItem
                    {
                        LocationName = txtboxLocationUserName.Text,
                        LocationAddress = LocationAddress,
                        LocationRange = Convert.ToInt32(1),
                        LocationLatitude = LocationResult.Latitude,
                        LocationLongitude = LocationResult.Longitude,                        
                    };
                    App.ViewModel.AddToDoItem(newToDoItem);
                }
            }
            else
            {
                MessageBox.Show("Please wait while retriving your current location");
            }
        }

     
        
        }


    }

