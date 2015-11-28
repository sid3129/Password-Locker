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
using Microsoft.Azure.KeyVault;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PasswordLocker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Add_Page : Page
    {
        private MobileServiceCollection<PasswordData, PasswordData> items;
        private IMobileServiceTable<PasswordData> password_table = App.MobileService.GetTable<PasswordData>();

        public Add_Page()
        {
            this.InitializeComponent();

        }

     
        public void ClearUI()
        {
            TitleBox.Text = "";
            UserIdBox.Text = "";
            PasswordBox.Text = "";
            EmailBox.Text = "";
            Phone_noBox.Text = "";
            Other_infoBox.Text = "";
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(List_Page), null);
        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("Are you sure you have entered your credentials correctly?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler_saveButton)));
            messageDialog.Commands.Add(new UICommand("Oops", new UICommandInvokedHandler(this.CommandInvokedHandler_saveButton)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();

        }

         private async void CommandInvokedHandler_saveButton(IUICommand command)
        {
            if(command.Label=="Yes")
            {
                PasswordData item = new PasswordData {
                    Title = TitleBox.Text,
                    User_Id = UserIdBox.Text,
                    Password = PasswordBox.Text,
                    Email = EmailBox.Text,
                    Phone_no = Phone_noBox.Text,
                    OtherInfo = Other_infoBox.Text
                };

                string message;
                bool success = false;

                try
                {
                    await password_table.InsertAsync(item);
                    message = "Your Information has been saved";
                    success = true;
                }
                catch (MobileServiceInvalidOperationException e)
                {
                    message = e.Message;

                }
                var message_dialog_box = new MessageDialog(message, "Database Status");
                await message_dialog_box.ShowAsync();
                
                if(success==true)
                {
                    this.Frame.Navigate(typeof(List_Page), null);
                }
                
            }
        
        }

         private void Reset_Button_Click(object sender, RoutedEventArgs e)
         {
             ClearUI();
         }

        

    }

}
