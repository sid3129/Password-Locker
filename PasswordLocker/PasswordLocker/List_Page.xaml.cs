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
using Windows.Security.Cryptography;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PasswordLocker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class List_Page : Page
    {
        private MobileServiceCollection<PasswordData, PasswordData> items;
        private IMobileServiceTable<PasswordData> password_table = App.MobileService.GetTable<PasswordData>();
      
        
        public List_Page()
        {
           
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
           LoginType temp = (LoginType)e.Parameter;
           await Authenticate(temp);
           QueryData();
        }


        public async System.Threading.Tasks.Task Authenticate(LoginType Service_name)
        {
            while(App.MobileService.CurrentUser==null)
            {
                string message;
                try
                {
                    switch (Service_name.Title)
                    {
                        case "Facebook": { await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Facebook); break; }
                        case "Google": { await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Google); break; }
                        case "Twitter": { await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Twitter); break; }
                        case "Microsoft Account": { await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount); break; }
                    }
                    message = string.Format("User Authenticated - {0}",App.MobileService.CurrentUser);
                }
                catch(MobileServiceInvalidOperationException e)
                {
                    message = "login unsuccesful";
                    this.Frame.Navigate(typeof(MainPage), null);
                   
                }

                var message_dialog_box = new MessageDialog(message, "Login Status");
                await message_dialog_box.ShowAsync();
            }
        }

        public async void QueryData()
        {
            items = await password_table.ToCollectionAsync();
            MainMenu.ItemsSource = items;
        }

        private void Add_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Add_Page), null);
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }

        private void MainMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainMenu.SelectedItem != null)
            {
                PasswordData temp = MainMenu.SelectedItem as PasswordData;
                if (temp != null)
                    this.Frame.Navigate(typeof(Detail_Page), temp);
                MainMenu.SelectedItem = null;
            }
        }
    }
}
