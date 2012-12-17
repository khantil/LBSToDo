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
using Microsoft.Phone.Maps.Controls;

namespace LBSToDo
{
    public partial class SelectLocation : PhoneApplicationPage
    {
        public SelectLocation()
        {
            InitializeComponent();

            Map MyMap = new Map();
            MyMap.Center = new GeoCoordinate(47.6097, -122.3331);
            MyMap.ZoomLevel = 10;
            ContentPanel.Children.Add(MyMap);

        }
    }
}