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
    public sealed partial class RegistrationPage : Page
    {
        DatabaseAccess da = new DatabaseAccess();

        public RegistrationPage()
        {
            this.InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(MainPage));
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if(tbRegisterName.Text == "")
            {
                var messageDialog = new MessageDialog("Login name is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if(tbRegisterPassword.Password == "")
            {
                var messageDialog = new MessageDialog("Password is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if(tbRegisterEmail.Text == "")
            {
                var messageDialog = new MessageDialog("Email is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else
            {
                da.AddMember((App.Current as App).ConnectionString,
                    tbRegisterName.Text, tbRegisterPassword.Password,
                    tbRegisterEmail.Text);

                tbRegisterName.Text = "";
                tbRegisterPassword.Password = "";
                tbRegisterEmail.Text = "";
                textRegisterSuccess.Visibility = Visibility.Visible;
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
        }
    }
}
