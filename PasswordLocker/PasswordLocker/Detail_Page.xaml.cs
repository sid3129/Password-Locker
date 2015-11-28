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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

     
    public sealed partial class Detail_Page : Page
    {
        private MobileServiceCollection<PasswordData, PasswordData> items;
        private IMobileServiceTable<PasswordData> password_table = App.MobileService.GetTable<PasswordData>();

        private static PasswordData temp = new PasswordData();
   
        public Detail_Page()
        {
            this.InitializeComponent();

        }

        public async void update_data(PasswordData changed_item)
        {
            await password_table.UpdateAsync(changed_item);
        }


        public async void delete_data(PasswordData deleted_item)
        {
            await password_table.DeleteAsync(deleted_item);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           temp = (PasswordData)e.Parameter;
           UpdateUI(temp);
        }

        public void UpdateUI(PasswordData details)
        {
            TitleBox.DataContext = details.Title;
            UserIdBox.DataContext = details.User_Id;
            PasswordBox.DataContext = details.Password;
            EmailBox.DataContext = details.Email;
            Phone_noBox.DataContext = details.Phone_no;
            Other_infoBox.DataContext = details.OtherInfo;
        }

        public void ClearUI()
        {
            TitleBox.DataContext = null;
            UserIdBox.DataContext = null;
            PasswordBox.DataContext = null;
            EmailBox.DataContext = null;
            Phone_noBox.DataContext = null;
            Other_infoBox.DataContext = null;
        }


        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(List_Page), null);
        }
    

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ClearUI();
            UpdateUI(temp);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("Do you want to save the changes?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Save", new UICommandInvokedHandler(this.CommandInvokedHandler_saveButton)));
            messageDialog.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler_saveButton)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler_saveButton(IUICommand command)
        {
            if (command.Label == "Save")
            {
                temp.Title = TitleBox.Text;
                temp.User_Id = UserIdBox.Text;
                temp.Password = PasswordBox.Text;
                temp.Email = EmailBox.Text;
                temp.Phone_no = Phone_noBox.Text;
                temp.OtherInfo = Other_infoBox.Text;
                update_data(temp);
            }

        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("Do you want to delete this information?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Delete", new UICommandInvokedHandler(this.CommandInvokedHandler_deleteButton)));
            messageDialog.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler_deleteButton)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler_deleteButton(IUICommand command)
        {
            if (command.Label == "Delete")
            {
                delete_data(temp);
                ClearUI();

                //for (int i = 0; i < App.full_list.Count; i++)
                //{
                //    if (App.full_list[i].Id == App.item.Id)
                //    {
                //        App.full_list.RemoveAt(i);
                //        break;
                //    }
                //}
                this.Frame.Navigate(typeof(List_Page), null);
            }
        }
    }
}
