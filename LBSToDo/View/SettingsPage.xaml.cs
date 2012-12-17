using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LBSToDo
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void remainderTglSwitch_Checked(object sender, RoutedEventArgs e)
        {
            remainderToggleSwitch.Content = "on";
        }

        private void remainderTglSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            remainderToggleSwitch.Content = "off";
        }
    }
}