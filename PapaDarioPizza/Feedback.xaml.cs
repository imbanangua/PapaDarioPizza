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
    public sealed partial class Feedback : Page
    {
        DatabaseAccess da = new DatabaseAccess();

        public Feedback()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private async void SendFeedback_Click(object sender, RoutedEventArgs e)
        {
            if(tbFeedbackEmail.Text == "")
            {
                var messageDialog = new MessageDialog("Please enter a email.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if(tbFeedback.Text.Trim() == "")
            {
                var messageDialog = new MessageDialog("Please enter feedbacks.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else
            {
                da.AddFeedback((App.Current as App).ConnectionString,
                    tbFeedbackEmail.Text, tbFeedback.Text);

                tbFeedbackEmail.Text = "";
                tbFeedback.Text = "";
                textSendSuccess.Visibility = Visibility.Visible;
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
        }
    }
}
