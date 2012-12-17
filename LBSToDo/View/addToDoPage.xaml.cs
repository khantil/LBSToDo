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

namespace LBSToDo
{
    public class locations
    {
        public string Name
        {
            get;
            set;
        }
    }

    public partial class addToDoPage : PhoneApplicationPage
    {
        public addToDoPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
            List<locations> source = new List<locations>();
            source.Add(new locations() { Name = "location 1" });
            source.Add(new locations() { Name = "location 2" });
            source.Add(new locations() { Name = "location 3" });
            source.Add(new locations() { Name = "location 4" });
            source.Add(new locations() { Name = "location 5" });
            source.Add(new locations() { Name = "location 6" });
            this.locationListPicker.ItemsSource = source;
        }

        /*void locationListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectLocationTextBlock.Text = "";
            if (this.locationListPicker.SelectedItems != null && this.locationListPicker.SelectionMode == SelectionMode.Multiple)
            {
                for (int i = 0; i < this.locationListPicker.SelectedItems.Count; i++)
                {
                    string str = ((locations)(this.locationListPicker.SelectedItems[i])).Name;
                    if (i == 0)
                    {
                        selectLocationTextBlock.Text = "Selected Item(s) is " + str;
                    }
                    else
                    {
                        selectLocationTextBlock.Text = selectLocationTextBlock.Text + "," + str;
                    }
                }
            }
            else if (this.locationListPicker.SelectionMode == SelectionMode.Single)
            {
                selectLocationTextBlock.Text = "Selected Item is " + ((locations)this.locationListPicker.SelectedItem).Name;
            }
        }*/

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/images/saveIcon.png", UriKind.RelativeOrAbsolute));
            appBarButton.Text = "save";
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem("menu 1");
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

    }
}