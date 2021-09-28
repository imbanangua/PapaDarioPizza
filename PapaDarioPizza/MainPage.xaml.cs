using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PapaDarioPizza
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(
                new Size(1920, 1080));
            Window.Current.Activate();

            MyFrame.Navigate(typeof(OrderPizza));

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Order";
        }

        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MyFrame.CanGoBack)
        //    {
        //        MyFrame.GoBack();
        //    }
        //}

        //private void ForwardButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MyFrame.CanGoForward)
        //    {
        //        MyFrame.GoForward();
        //    }
        //}

        //private void NavigateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(AboutUs));
        //}

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(OrderPizza));

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Order";
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Feedback));

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Feedback";
        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AboutUs));

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "About Us";
        }

        private void Shoppingcart_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(ShoppingCartPage));

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Shopping Cart";
        }

        private void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AdminLogin));

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Administration";
        }
    }
}
