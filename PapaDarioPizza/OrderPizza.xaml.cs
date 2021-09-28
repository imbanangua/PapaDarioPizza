using PapaDarioPizza.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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
    public sealed partial class OrderPizza : Page
    {
        public OrderPizza()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(
                new Size(1920, 1080));
            Window.Current.Activate();

            if((Application.Current as App).g_member.Name != null)
            {
                textWelcomeName.Text = (Application.Current as App).g_member.Name;
                loginPart.Visibility = Visibility.Collapsed;
                logoutPart.Visibility = Visibility.Visible;
            }

            Dictionary<string, string> type = new Dictionary<string, string>();
            Dictionary<string, string> size = new Dictionary<string, string>();
            Dictionary<string, string> drink = new Dictionary<string, string>();
            Dictionary<string, string> side = new Dictionary<string, string>();
            Dictionary<string, string> wings = new Dictionary<string, string>();

            type.Add("Pepperoni", "Pepperoni");
            type.Add("Tropical Hawaiian", "Tropical Hawaiian");
            type.Add("Meat Supreme", "Meat Supreme");
            type.Add("Pesto Amore", "Pesto Amore");
            type.Add("Spicy BBQ Chicken", "Spicy BBQ Chicken");
            type.Add("Sausage Mushroom Melt", "Sausage Mushroom Melt");
            type.Add("Canadian", "Canadian");
            type.Add("Classic Super", "Classic Super");
            cbType.ItemsSource = type;
            cbType.SelectedValuePath = "Value";
            cbType.DisplayMemberPath = "Key";
            cbType.SelectedIndex = 0;

            size.Add("Small $12.99", "Small");
            size.Add("Medium $14.99", "Medium");
            size.Add("Large $17.49", "Large");
            size.Add("Extra Large $22.99", "Extra Large");
            cbSize.ItemsSource = size;
            cbSize.SelectedValuePath = "Value";
            cbSize.DisplayMemberPath = "Key";
            cbSize.SelectedIndex = 0;

            drink.Add("Coke Classic", "Coke Classic");
            drink.Add("Coke Zero", "Coke Zero");
            drink.Add("Sprite", "Sprite");
            drink.Add("Nestea", "Nestea");
            cbDrink.ItemsSource = drink;
            cbDrink.SelectedValuePath = "Value";
            cbDrink.DisplayMemberPath = "Key";
            cbDrink.SelectedIndex = 0;

            side.Add("Fries", "Fries");
            side.Add("Garlic Bread", "Garlic Bread");
            side.Add("Onion Rings", "Onion Rings");
            side.Add("Garden Salad", "Garden Salad");
            cbSide.ItemsSource = side;
            cbSide.SelectedValuePath = "Value";
            cbSide.DisplayMemberPath = "Key";
            cbSide.SelectedIndex = 0;

            wings.Add("5", "5");
            wings.Add("10", "10");
            wings.Add("15", "15");
            wings.Add("20", "20");
            cbWings.ItemsSource = wings;
            cbWings.SelectedValuePath = "Value";
            cbWings.DisplayMemberPath = "Key";
            cbWings.SelectedIndex = 0;

        }

        private void btnBuyDeal_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).g_shoppingcart.Deal.Quantity += 1;
        }

        private void btnBuyPizza_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizza = new Pizza();
            if(cbType.SelectedItem != null && cbSize.SelectedItem != null) 
            {
                //System.Diagnostics.Debug.WriteLine(checkTomatoes.IsChecked);
                pizza.Type = cbType.SelectedValue.ToString();
                pizza.Size = cbSize.SelectedValue.ToString();
                pizza.Topping[0] = checkMushrooms.IsChecked.Value;
                pizza.Topping[1] = checkTomatoes.IsChecked.Value;
                pizza.Topping[2] = checkOnions.IsChecked.Value;
                pizza.Topping[3] = checkHam.IsChecked.Value;
                pizza.Topping[4] = checkPepperoni.IsChecked.Value;
                pizza.Topping[5] = checkPickles.IsChecked.Value;
                pizza.Topping[6] = checkSausage.IsChecked.Value;
                pizza.Topping[7] = checkChicken.IsChecked.Value;
                pizza.Topping[8] = checkBeef.IsChecked.Value;

                pizza.Combo = (bool)checkCombo.IsChecked;
                if (checkCombo.IsChecked == true && cbDrink.SelectedItem != null && cbSide.SelectedItem != null)
                {
                    pizza.Drink = cbDrink.SelectedValue.ToString();
                    pizza.Side = cbSide.SelectedValue.ToString();
                }

                (Application.Current as App).g_shoppingcart.Pizza.Add(pizza);
            }                
        }

        private void btnBuyWings_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).g_shoppingcart.Wings.Quantity += Convert.ToInt32(cbWings.SelectedValue);          
        }

        private void btnBuySandwiches_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).g_shoppingcart.Sandwiches.Quantity += 1;
        }

        private void btnBuyPoutine_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).g_shoppingcart.Poutine.Quantity += 1;
        }

        private void LinkRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(RegistrationPage));
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            DatabaseAccess da = new DatabaseAccess();

            if(tbLoginName.Text == "")
            {
                var messageDialog = new MessageDialog("Login name is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if(tbLoginPassword.Password == "")
            {
                var messageDialog = new MessageDialog("Password is blank.");
                messageDialog.Commands.Add(new UICommand("Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await messageDialog.ShowAsync();
            }
            else if(da.IsExist((App.Current as App).ConnectionString, tbLoginName.Text))
            {
                if(da.IsPasswordCorrect((App.Current as App).ConnectionString, tbLoginName.Text, tbLoginPassword.Password))
                {
                    (Application.Current as App).g_member.Name = tbLoginName.Text;
                    (Application.Current as App).g_member.Password = tbLoginPassword.Password;

                    textWelcomeName.Text = (Application.Current as App).g_member.Name;
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).g_member = new Member();

            if ((Application.Current as App).g_member.Name == null)
            {
                loginPart.Visibility = Visibility.Visible;
                logoutPart.Visibility = Visibility.Collapsed;

                tbLoginName.Text = "";
                tbLoginPassword.Password = "";
            }
        }

        private void LinkSetProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(MyProfile));
        }
    }
}
