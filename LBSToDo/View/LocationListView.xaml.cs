﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LBSToDo.Resources;
using LBSToDo.Model;

namespace LBSToDo
{
    public partial class LocationListPage : PhoneApplicationPage
    {
        // Constructor
        public LocationListPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            LocationItem newToDoItem = new LocationItem
            {
                LocationName = "Location4",
                LocationRange = 10
            };
            App.ViewModel.AddToDoItem(newToDoItem);

        }

        private void AddApplicationBar_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddLocation.xaml", UriKind.Relative));
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

    }
}