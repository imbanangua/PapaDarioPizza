using PapaDarioPizza.Object;
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
    public sealed partial class AdminLogin : Page
    {

        DatabaseAccess da = new DatabaseAccess();

        public AdminLogin()
        {
            this.InitializeComponent();

            if ((Application.Current as App).g_admin.Name != null)
            {
                loginPart.Visibility = Visibility.Collapsed;
                logoutPart.Visibility = Visibility.Visible;

                MyFrame.Navigate(typeof(AdminView));
            }
        }

        private async void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            if(tbAdminName.Text == "")
            {
                var messageDialog = new MessageDialog("Login name is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if(tbAdminPassword.Password == "")
            {
                var messageDialog = new MessageDialog("Password is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if(da.IsAdminExist((App.Current as App).ConnectionString, tbAdminName.Text))
            {
                if (da.IsAdminPasswordCorrect((App.Current as App).ConnectionString, tbAdminName.Text, tbAdminPassword.Password))
                {
                    MyFrame.Navigate(typeof(AdminView));

                    (Application.Current as App).g_admin.Name = tbAdminName.Text;
                    (Application.Current as App).g_admin.Password = tbAdminPassword.Password;

                    loginPart.Visibility = Visibility.Collapsed;
                    logoutPart.Visibility = Visibility.Visible;
                }
                else
                {
                    var messageDialog = new MessageDialog("Login name or password is incorrect.");
                    messageDialog.Commands.Add(new UICommand("Close",
                        new UICommandInvokedHandler(this.CommandInvokedHandler)));
                    await messageDialog.ShowAsync();
                }
                
            }
            else
            {
                var messageDialog = new MessageDialog("Login name or password is incorrect.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
        }

        private void AdminLogout_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).g_admin = new Admin();

            MyFrame.Navigate(typeof(BlankPage));

            logoutPart.Visibility = Visibility.Collapsed;
            loginPart.Visibility = Visibility.Visible;
        }
    }
}
