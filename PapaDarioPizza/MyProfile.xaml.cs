using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PapaDarioPizza
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyProfile : Page
    {
        DatabaseAccess da = new DatabaseAccess();

        public MyProfile()
        {
            this.InitializeComponent();

            tbResetPassword.Text = (Application.Current as App).g_member.Password;
            tbEmail.Text = da.SelectEmail((App.Current as App).ConnectionString, (Application.Current as App).g_member.Name);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(MainPage));
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            if (tbResetPassword.Text == "")
            {
                var messageDialog = new MessageDialog("Password is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if (tbEmail.Text == "")
            {
                var messageDialog = new MessageDialog("Email is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else
            {
                da.UpdateMember((App.Current as App).ConnectionString,
                    (Application.Current as App).g_member.Name, 
                    tbResetPassword.Text,
                    tbEmail.Text);

                textUpdateSuccess.Visibility = Visibility.Visible;
                (Application.Current as App).g_member.Password = tbResetPassword.Text;

            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
        }
    }
}
