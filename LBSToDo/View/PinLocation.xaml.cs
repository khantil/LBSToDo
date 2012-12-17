using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LBSToDo.Resources;
using Microsoft.Phone.Maps.Controls;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Device.Location;
using System.Collections.ObjectModel;

using Microsoft.Phone.Maps.Services;
using System.Windows.Media.Imaging;

namespace LBSToDo
{
    public partial class PinLocation : PhoneApplicationPage
    {
        MapLayer markerLayer = null;
        GeocodeQuery geoQ = null;
        IList<MapLocation> resList = null;
        bool draggingNow = false;
        MapOverlay oneMarker = null;
        GeoCoordinate locationResult = null;
        string locationAddress = null;
        // Constructor
        public PinLocation()
        {
            InitializeComponent();

            geoQ = new GeocodeQuery();
            geoQ.QueryCompleted += geoQ_QueryCompleted;
            Debug.WriteLine("All construction done for GeoCoding");
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {            
            base.OnNavigatedTo(e);
        }

        void marker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("oneMarker_MouseLeftButtonDown");
            if (oneMarker != null)
            {
                draggingNow = true;
                map1.IsEnabled = false;
            }
        }
        void Touch_FrameReported(object sender, System.Windows.Input.TouchFrameEventArgs e)
        {

            if (draggingNow == true)
            {
                TouchPoint tp = e.GetPrimaryTouchPoint(map1);

                if (tp.Action == TouchAction.Move)
                {
                    if (oneMarker != null)
                    {
                        oneMarker.GeoCoordinate = map1.ConvertViewportPointToGeoCoordinate(tp.Position);
                        Debug.WriteLine("Lat: " + oneMarker.GeoCoordinate.Latitude + "; Long: " + oneMarker.GeoCoordinate.Longitude);
                    }
                }
                else if (tp.Action == TouchAction.Up)
                {
                    draggingNow = false;
                    map1.IsEnabled = true;
                }
            }

        }
        private void txtSearchLocationBox_ActionIconTapped(object sender, EventArgs e)
        {
            if (geoQ.IsBusy == true)
            {
                geoQ.CancelAsync();
            }
            // Set the full address query

            GeoCoordinate setMe = new GeoCoordinate(28.670106, 77.218575);
            setMe.HorizontalAccuracy = 1000000;

            geoQ.GeoCoordinate = setMe;
            geoQ.SearchTerm = txtSearchLocationBox.Text;
            geoQ.MaxResultCount = 200;

            geoQ.QueryAsync();
            //NavigationService.Navigate(new Uri(string.Format("/View/LocationSearchResultListPage.xaml?searchtxt={0}", txtSearchLocationBox.Text), UriKind.Relative));
            
            //NavigationService.Navigate(new Uri("/LocationSearchResultListPage.xaml", UriKind.Relative));

            //if (markerLayer != null)
            //{
            //    map1.Layers.Remove(markerLayer);
            //    markerLayer = null;
            //}

            //markerLayer = new MapLayer();
            //map1.Layers.Add(markerLayer);

            //if (geoQ.IsBusy == true)
            //{
            //    geoQ.CancelAsync();
            //}
            //// Set the full address query

            //GeoCoordinate setMe = new GeoCoordinate(map1.Center.Latitude, map1.Center.Longitude);
            //setMe.HorizontalAccuracy = 1000000;

            //geoQ.GeoCoordinate = setMe;
            //geoQ.SearchTerm = txtSearchLocationBox.Text;
            //geoQ.MaxResultCount = 200;

            //geoQ.QueryAsync();
            //Debug.WriteLine("GeocodeAsync started for: " + txtSearchLocationBox.Text);
            

        }
        void geoQ_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            // The result is a GeocodeResponse object
            resList = e.Result;

            Debug.WriteLine("Geo query, error: " + e.Error);
            Debug.WriteLine("Geo query, cancelled: " + e.Cancelled);
            Debug.WriteLine("Geo query, cancelled: " + e.UserState.ToString());
            Debug.WriteLine("Geo query, Result.Count(): " + resList.Count());

            System.Collections.Generic.List<Model.SearchResultItem> items =
                    new System.Collections.Generic.List<Model.SearchResultItem>();


            if (resList.Count() > 0)
            {
                for (int i = 0; i < resList.Count(); i++)
                {
                    String addressTxt = "";
                    MapAddress address = resList[i].Information.Address;

                    Debug.WriteLine("Result no.: " + i);
                    if (!"".Equals(address.HouseNumber))
                    {
                        addressTxt += ", " + address.HouseNumber;
                    }
                    if (!"".Equals(address.BuildingRoom))
                    {
                        addressTxt += address.BuildingRoom;
                    }

                    if (!"".Equals(address.BuildingFloor))
                    {
                        addressTxt += ", " + address.BuildingFloor;
                    }
                    if (!"".Equals(address.BuildingName))
                    {
                        addressTxt += ", " + address.BuildingName;
                    }
                    if (!"".Equals(address.BuildingZone))
                    {
                        addressTxt += ", " + address.BuildingZone;
                    }
                    if (!"".Equals(address.Neighborhood))
                    {
                        addressTxt += ", " + address.Neighborhood;
                    }
                    if (!"".Equals(address.Province))
                    {
                        addressTxt += ", " + address.Province;
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
                    if (!"".Equals(address.Continent))
                    {
                        addressTxt += ", " + address.Continent;
                    }
                    if (!"".Equals(address.PostalCode))
                    {
                        addressTxt += ", " + address.PostalCode;
                    }
                    if (!"".Equals(address.Township))
                    {
                        addressTxt += ", " + address.Township;
                    }
                    if (addressTxt.StartsWith(","))
                    {
                        addressTxt = addressTxt.Substring(1);
                    }

                    Model.SearchResultItem searchResultItem = new Model.SearchResultItem(addressTxt, resList[i].GeoCoordinate.Latitude, resList[i].GeoCoordinate.Longitude);
                    items.Add(searchResultItem);
                    Debug.WriteLine(addressTxt);
                    /* Debug.WriteLine("Name: " + resList[i].Information.Name);
                     Debug.WriteLine("Address.ToString: " + resList[i].Information.Address.ToString());
                     Debug.WriteLine("Address.District: " + resList[i].Information.Address.District);
                     Debug.WriteLine("Address.Country: " + resList[i].Information.Address.CountryCode + ": " + resList[i].Information.Address.Country);
                     Debug.WriteLine("Address.County: " + resList[i].Information.Address.County);
                     Debug.WriteLine("Address.Neighborhood: " + resList[i].Information.Address.Neighborhood);
                     Debug.WriteLine("Address.Street: " + resList[i].Information.Address.Street);
                     Debug.WriteLine("Address.PostalCode: " + resList[i].Information.Address.PostalCode);
                     Debug.WriteLine("Address.Continent: " + resList[i].Information.Address.Continent);
                    
                     Debug.WriteLine("GeoCoordinate.Latitude: " + resList[i].GeoCoordinate.Latitude.ToString());
                     Debug.WriteLine("GeoCoordinate.Longitude: " + resList[i].GeoCoordinate.Longitude.ToString());
                     */
                    string numNum = "0" + i;
                    if (i > 9)
                    {
                        numNum = "" + i;
                    }
                    GroupedList.ItemsSource = items;
                    GroupedList.Visibility = Visibility.Visible;
                    map1.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void GroupedList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            

            if (GroupedList.SelectedItem is Model.SearchResultItem)
            {

                Model.SearchResultItem searchResultItem = (Model.SearchResultItem)GroupedList.SelectedItem;
                //  MessageBox.Show("You tapped on the Result item " + ((Model.SearchResultItem)GroupedList.SelectedItem));
                GroupedList.Visibility = Visibility.Collapsed;
                map1.Visibility = Visibility.Visible;
                AddResultToMap(searchResultItem.Result, new GeoCoordinate(searchResultItem.Latitude, searchResultItem.Longitude));
                
            }
        }

        private void AddResultToMap(String locationAddress, GeoCoordinate location)
        {

            System.Windows.Input.Touch.FrameReported += Touch_FrameReported;
            // set class variable
            this.locationAddress = locationAddress;
            this.locationResult = location;
            MapLayer oneMarkerLayer = new MapLayer();
            oneMarker = new MapOverlay();

            Canvas canCan = new Canvas();

            TextBlock MarkerTxt = new TextBlock { Text = "Drag" };
            MarkerTxt.Foreground = new SolidColorBrush(Colors.Black);
            MarkerTxt.HorizontalAlignment = HorizontalAlignment.Center;
            Canvas.SetLeft(MarkerTxt, 10);
            Canvas.SetTop(MarkerTxt, -20);
            Canvas.SetZIndex(MarkerTxt, 5);

            canCan.Children.Add(MarkerTxt);

            Uri imgUri = new Uri("/Assets/icons/pushpin.png", UriKind.RelativeOrAbsolute);
            BitmapImage imgSourceR = new BitmapImage(imgUri);
            ImageBrush imgBrush = new ImageBrush() { ImageSource = imgSourceR };
            //canCan.Children.Add(imgSourceR)
            Image image = new Image
            {
                Height = 64,
                Width = 64,
                Source = imgSourceR

            };
            canCan.Children.Add(image);

            oneMarker.Content = new Rectangle()
            {
                Fill = imgBrush,

            };
            oneMarker.Content = canCan;

            oneMarker.PositionOrigin = new Point(0.5, 0.5);
            oneMarker.GeoCoordinate = location;
            MarkerTxt.MouseLeftButtonDown += marker_MouseLeftButtonDown;


            oneMarkerLayer.Add(oneMarker);
            map1.Layers.Add(oneMarkerLayer);
            map1.Center = oneMarker.GeoCoordinate;
        }



        private void SearchApplicationBar_Click(object sender, EventArgs e)
        {

        }

        private void SaveApplicationBar_Click(object sender, EventArgs e)
        {
            if (null != searchText && null != locationResult)
            {
               // NavigationService.Navigate(new Uri(string.Format("/View/AddLocation.xaml?searchText=" + searchText + "&lat=" + latitude + "&long=" + longitude), UriKind.Relative));
                AddLocation.LocationResult = locationResult;
                AddLocation.LocationAddress = locationAddress;
                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }

            }
            else
                MessageBox.Show("Please select a location");

        }

        void textt_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("textt_MouseLeftButtonUp");
            TextBlock textt = sender as TextBlock;
            if (textt != null && (resList != null))
            {
                int hint = int.Parse(textt.Text);

                if (hint >= 0 && hint < resList.Count())
                {

                    string showString = resList[hint].Information.Name;
                    showString = showString + "\nAddress: ";
                    showString = showString + "\n" + resList[hint].Information.Address.HouseNumber + " " + resList[hint].Information.Address.Street;
                    showString = showString + "\n" + resList[hint].Information.Address.PostalCode + " " + resList[hint].Information.Address.City;
                    showString = showString + "\n" + resList[hint].Information.Address.Country + " " + resList[hint].Information.Address.CountryCode;
                    showString = showString + "\nDescription: ";
                    showString = showString + "\n" + resList[hint].Information.Description.ToString();

                    String addressTxt = "";
                    MapAddress address = resList[hint].Information.Address;
                    // Debug.WriteLine("Result no.: " + i);
                    if (!"".Equals(address.HouseNumber))
                    {
                        addressTxt += ", " + address.HouseNumber;
                    }
                    if (!"".Equals(address.BuildingRoom))
                    {
                        addressTxt += address.BuildingRoom;
                    }

                    if (!"".Equals(address.BuildingFloor))
                    {
                        addressTxt += ", " + address.BuildingFloor;
                    }
                    if (!"".Equals(address.BuildingName))
                    {
                        addressTxt += ", " + address.BuildingName;
                    }
                    if (!"".Equals(address.BuildingZone))
                    {
                        addressTxt += ", " + address.BuildingName;
                    }
                    if (!"".Equals(address.Neighborhood))
                    {
                        addressTxt += ", " + address.Neighborhood;
                    }
                    if (!"".Equals(address.Province))
                    {
                        addressTxt += ", " + address.Province;
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
                    if (!"".Equals(address.Continent))
                    {
                        addressTxt += ", " + address.Continent;
                    }
                    if (!"".Equals(address.PostalCode))
                    {
                        addressTxt += ", " + address.PostalCode;
                    }
                    if (!"".Equals(address.Township))
                    {
                        addressTxt += ", " + address.Township;
                    }
                    if (addressTxt.StartsWith(","))
                    {
                        addressTxt = addressTxt.Substring(1);
                    }
                    // Debug.WriteLine(addressTxt);

                    MessageBox.Show(addressTxt);
                }
            }

        }
    }
}