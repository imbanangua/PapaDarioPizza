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
    public sealed partial class AdminView : Page
    {
        DatabaseAccess da = new DatabaseAccess();
        int feedbackId;

        public AdminView()
        {
            this.InitializeComponent();

            MemberList ml = new MemberList();
            MemberList.ItemsSource = ml.GetMembers((App.Current as App).ConnectionString);

            FeedbackList fl = new FeedbackList();
            FeedbackList.ItemsSource = fl.GetFeedbacks((App.Current as App).ConnectionString);
        }

        private void MemberListItem_Click(object sender, ItemClickEventArgs e)
        {
            var memberSelect = (MemberList)e.ClickedItem;

            tbName.Text = memberSelect.Name;
            tbPassword.Text = memberSelect.Password;
            tbEmail.Text = memberSelect.Email;
        }

        private void CommandInvokedHandler(IUICommand command)
        {
        }

        private async void AddMember_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "")
            {
                var messageDialog = new MessageDialog("Login name is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if (tbPassword.Text == "")
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
                da.AddMember((App.Current as App).ConnectionString,
                    tbName.Text, tbPassword.Text,
                    tbEmail.Text);

                tbName.Text = "";
                tbPassword.Text = "";
                tbEmail.Text = "";
                textOperateSuccess.Text = "Add member successful!";

                MemberList ml = new MemberList();
                MemberList.ItemsSource = ml.GetMembers((App.Current as App).ConnectionString);
            }
        }

        private async void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "")
            {
                var messageDialog = new MessageDialog("Login name is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if (!da.IsExist((App.Current as App).ConnectionString, tbName.Text))
            {
                var messageDialog = new MessageDialog("Login name is not exist.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else
            {
                da.DeleteMember((App.Current as App).ConnectionString, tbName.Text);

                tbName.Text = "";
                tbPassword.Text = "";
                tbEmail.Text = "";
                textOperateSuccess.Text = "Delete member successful!";

                MemberList ml = new MemberList();
                MemberList.ItemsSource = ml.GetMembers((App.Current as App).ConnectionString);
            }    
        }

        private async void UpdateMember_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "")
            {
                var messageDialog = new MessageDialog("Login name is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if (!da.IsExist((App.Current as App).ConnectionString, tbName.Text))
            {
                var messageDialog = new MessageDialog("Login name is not exist.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else
            {
                da.UpdateMember((App.Current as App).ConnectionString, tbName.Text, tbPassword.Text, tbEmail.Text);

                tbName.Text = "";
                tbPassword.Text = "";
                tbEmail.Text = "";
                textOperateSuccess.Text = "Update member successful!";

                MemberList ml = new MemberList();
                MemberList.ItemsSource = ml.GetMembers((App.Current as App).ConnectionString);
            }
        }

        private void ClearMember_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = "";
            tbPassword.Text = "";
            tbEmail.Text = "";
            textOperateSuccess.Text = "";
        }

        private void FeedbackListItem_Click(object sender, ItemClickEventArgs e)
        {
            var feedbackSelect = (FeedbackList)e.ClickedItem;
            feedbackId = feedbackSelect.Id;

            btnDeleteFeedback.IsEnabled = true;
            textDeletEFeedbackSuccess.Text = "";
        }

        private void DeleteFeedback_Click(object sender, RoutedEventArgs e)
        {
            da.DeleteFeedback((App.Current as App).ConnectionString, feedbackId);
            textDeletEFeedbackSuccess.Text = "Delete feedback success!";

            FeedbackList fl = new FeedbackList();
            FeedbackList.ItemsSource = fl.GetFeedbacks((App.Current as App).ConnectionString);
        }
    }
}
