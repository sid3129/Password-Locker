using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PasswordLocker.ViewModels;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PasswordLocker
{
 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<LoginType> login_type = new List<LoginType>();
            login_type.Add(new LoginType() { Title = "Facebook", Icon = "/Assets/a.png" });
            login_type.Add(new LoginType() { Title = "Google", Icon = "/Assets/b.png" });
            login_type.Add(new LoginType() { Title = "Twitter", Icon = "/Assets/c.png" });
            login_type.Add(new LoginType() { Title = "Microsoft Account", Icon = "/Assets/d.png" });
            MainMenu.ItemsSource = login_type;

          //  SolidColorBrush b = new SolidColorBrush(Windows.UI.Colors.Blue);
           // MainMenu.Background = b;

        }

        private void MainMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainMenu.SelectedItem != null)
            {
                LoginType temp = MainMenu.SelectedItem as LoginType;
                if (temp != null)
                    this.Frame.Navigate(typeof(List_Page), temp);
                MainMenu.SelectedItem = null;
            }
        }
        /*
        private void MainMenu_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            SolidColorBrush b = new SolidColorBrush(Windows.UI.Colors.Blue);
            Background = b;
        }

        private void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            SolidColorBrush b = new SolidColorBrush(Windows.UI.Colors.Blue);
            Background = b;
        }
        */
    }
}
