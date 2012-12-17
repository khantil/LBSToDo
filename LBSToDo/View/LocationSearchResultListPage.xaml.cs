using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using System.Diagnostics;



using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using LBSToDo.Resources;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;
using Microsoft.Phone.Maps.Services;
using LBSToDo.Model;
namespace LBSToDo
{
    public partial class LocationSearchResultListPage : PhoneApplicationPage
    {
        IList<MapLocation> resList = null;
        GeocodeQuery geoQ = null;

        public LocationSearchResultListPage()
        {
        
            InitializeComponent();
            geoQ = new GeocodeQuery();
            geoQ.QueryCompleted += geoQ_QueryCompleted;
            Debug.WriteLine("All construction done for GeoCoding");
        }
         
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (GroupedList.ItemsSource == null)
            {
                string searchText = this.NavigationContext.QueryString["searchtxt"];
                txtBlockSearchHeader.Text= @""""+searchText+@"""";
             
                if (geoQ.IsBusy == true)
                {
                    geoQ.CancelAsync();
                }
                // Set the full address query

                GeoCoordinate setMe = new GeoCoordinate(28.670106, 77.218575);
                setMe.HorizontalAccuracy = 1000000;

                geoQ.GeoCoordinate = setMe;             
                geoQ.SearchTerm = searchText;
                geoQ.MaxResultCount = 200;

                geoQ.QueryAsync();
             }
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
                }
            }
        }
       
        private void GroupedList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (GroupedList.SelectedItem is Model.SearchResultItem)
            {

                Model.SearchResultItem searchResultItem = (Model.SearchResultItem)GroupedList.SelectedItem;
              //  MessageBox.Show("You tapped on the Result item " + ((Model.SearchResultItem)GroupedList.SelectedItem));

                NavigationService.Navigate(new Uri(string.Format("/View/PinLocation.xaml?searchText=" + searchResultItem.Result + "&lat=" + searchResultItem.Latitude + "&long=" + searchResultItem.Longitude), UriKind.Relative));
            }

        }
    }
}