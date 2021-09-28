using PapaDarioPizza.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
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
    public sealed partial class ShoppingCartPage : Page
    {
        private ObservableCollection<ShoppingCartItem> item;
        public ObservableCollection<ShoppingCartItem> Item
        {
            get
            {
                if (item == null)
                {
                    item = new ObservableCollection<ShoppingCartItem>();
                }
                return item;
            }
            set
            {
                item = value;
            }
        }
        public HostViewModel ViewModel { get; set; }
        private int index;
        private int pizzaIndex;

        public ShoppingCartPage()
        {
            this.InitializeComponent();

            if ((Application.Current as App).g_member.Name != null)
            {
                enterEmailPart.Visibility = Visibility.Collapsed;
            }
            item = new ObservableCollection<ShoppingCartItem>();
            ListViewBinding();
            this.ViewModel = new HostViewModel(item);
            if((Application.Current as App).g_member.Name != null)
            {
                tbOriginalSubtotal.Text = Math.Round((ViewModel.Subtotal / 0.9), 2).ToString();
                textOriginalSubtotal.Visibility = Visibility.Visible;
                originalPart.Visibility = Visibility.Visible;
                text10discount.Visibility = Visibility.Visible;
            }
            
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(((ShoppingCartItem)((HyperlinkButton)sender).Tag).Index);
            int i = 0;
            foreach (ShoppingCartItem sci in item)
            {
                if(sci.Index == ((ShoppingCartItem)((HyperlinkButton)sender).Tag).Index)
                {
                    if(item.ElementAt(i).Name == (Application.Current as App).g_shoppingcart.Deal.Name)
                    {
                        (Application.Current as App).g_shoppingcart.Deal.Quantity = 0;
                    }
                    else if (item.ElementAt(i).Name == (Application.Current as App).g_shoppingcart.Wings.Name)
                    {
                        (Application.Current as App).g_shoppingcart.Wings.Quantity = 0;
                    }
                    else if (item.ElementAt(i).Name == (Application.Current as App).g_shoppingcart.Sandwiches.Name)
                    {
                        (Application.Current as App).g_shoppingcart.Sandwiches.Quantity = 0;
                    }
                    else if (item.ElementAt(i).Name == (Application.Current as App).g_shoppingcart.Poutine.Name)
                    {
                        (Application.Current as App).g_shoppingcart.Poutine.Quantity = 0;
                    }
                    else
                    {
                        (Application.Current as App).g_shoppingcart.Pizza.RemoveAt(
                            ((ShoppingCartItem)((HyperlinkButton)sender).Tag).PizzaIndex);
                    }
                    //item.RemoveAt(i);
                    break;
                }
                i++;
            }

            item.Clear();
            ListViewBinding();

            this.ViewModel = new HostViewModel(item);
            tbSubtotal.Text = ViewModel.Subtotal.ToString();
            tbTax.Text = ViewModel.Tax.ToString();
            tbTotal.Text = ViewModel.Total.ToString();
            tbOriginalSubtotal.Text = Math.Round((ViewModel.Subtotal / 0.9), 2).ToString();

            if (!item.Any())
            {
                btnCheck.IsEnabled = false;
            }
        }

        private void ListViewBinding()
        {
            index = 0;
            pizzaIndex = 0;
            

            if ((Application.Current as App).g_shoppingcart.Deal.Quantity != 0)
            {
                ShoppingCartItem iDeal = new ShoppingCartItem();
                iDeal.Name = (Application.Current as App).g_shoppingcart.Deal.Name;
                iDeal.Price = "$" + (Application.Current as App).g_shoppingcart.Deal.Price.ToString();
                iDeal.Quantity = (Application.Current as App).g_shoppingcart.Deal.Quantity.ToString();
                iDeal.Index = index++;

                item.Add(iDeal);
            }

            if ((Application.Current as App).g_shoppingcart.Pizza.Count != 0)
            {
                foreach (Pizza p in (Application.Current as App).g_shoppingcart.Pizza)
                {
                    ShoppingCartItem iPizza = new ShoppingCartItem();
                    iPizza.Name = "Pizza: " + p.Type + "\n      " + p.Size + "\n";
                    string topping = "";
                    bool isTopping = false;
                    for (int i = 0; i < 9; i++)
                    {
                        if (p.Topping[i] == true)
                        {
                            if (isTopping == true)
                                topping += ", ";
                            else
                                topping += "      Toppings:  ";
                            isTopping = true;
                            switch (i)
                            {
                                case 0: topping += "Mushrooms "; break;
                                case 1: topping += "Tomatoes "; break;
                                case 2: topping += "Onions "; break;
                                case 3: topping += "Ham "; break;
                                case 4: topping += "Pepperoni "; break;
                                case 5: topping += "Pickles "; break;
                                case 6: topping += "Italian Sausage "; break;
                                case 7: topping += "Chicken "; break;
                                case 8: topping += "Ground Beef "; break;
                                default: break;
                            }
                        }
                    }
                    iPizza.Name += topping;
                    if (isTopping == true)
                        iPizza.Name += "\n";
                    if (p.Combo == true)
                    {
                        iPizza.Name += "      Combo: " + p.Drink + ", " + p.Side + "\n";
                    }
                    iPizza.Price = "$" + p.Price;
                    iPizza.Quantity = "1";
                    iPizza.Index = index++;
                    iPizza.PizzaIndex = pizzaIndex++;

                    item.Add(iPizza);
                }
            }

            if ((Application.Current as App).g_shoppingcart.Wings.Quantity != 0)
            {
                ShoppingCartItem iWings = new ShoppingCartItem();
                iWings.Name = (Application.Current as App).g_shoppingcart.Wings.Name;
                iWings.Price = "$" + (Application.Current as App).g_shoppingcart.Wings.Price.ToString();
                iWings.Quantity = (Application.Current as App).g_shoppingcart.Wings.Quantity.ToString();
                iWings.Index = index++;

                item.Add(iWings);
            }

            if ((Application.Current as App).g_shoppingcart.Sandwiches.Quantity != 0)
            {
                ShoppingCartItem iSandwiches = new ShoppingCartItem();
                iSandwiches.Name = (Application.Current as App).g_shoppingcart.Sandwiches.Name;
                iSandwiches.Price = "$" + (Application.Current as App).g_shoppingcart.Sandwiches.Price.ToString();
                iSandwiches.Quantity = (Application.Current as App).g_shoppingcart.Sandwiches.Quantity.ToString();
                iSandwiches.Index = index++;

                item.Add(iSandwiches);
            }

            if ((Application.Current as App).g_shoppingcart.Poutine.Quantity != 0)
            {
                ShoppingCartItem iPoutine = new ShoppingCartItem();
                iPoutine.Name = (Application.Current as App).g_shoppingcart.Poutine.Name;
                iPoutine.Price = "$" + (Application.Current as App).g_shoppingcart.Poutine.Price.ToString();
                iPoutine.Quantity = (Application.Current as App).g_shoppingcart.Poutine.Quantity.ToString();
                iPoutine.Index = index++;

                item.Add(iPoutine);
            }
        }

        private async void Checkout_Click(object sender, RoutedEventArgs e)
        {
            
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string pizzaName = "";

            DatabaseAccess da = new DatabaseAccess();

            if((Application.Current as App).g_member.Name != null)
            {
                string invoice = "Welcome Papa Dario's Pizza\n\n";
                foreach (ShoppingCartItem spi in item)
                {
                    invoice += spi.Name + " --- " + spi.Quantity + " --- " + spi.Price + "\n\n";

                    if (spi.Name.Contains("Pizza"))
                    {
                        pizzaName += spi.Name + " | ";
                    }
                }
                invoice += "\nOriginal Subtotal: $" + Math.Round((ViewModel.Subtotal / 0.9), 2).ToString()
                    + "\nSubtotal(-10%): $" + ViewModel.Subtotal.ToString()
                    + "\nTax: $" + ViewModel.Tax.ToString()
                    + "\nTotal: $" + ViewModel.Total.ToString()
                    + "\n\nDate: " + date;

                var savePicker = new FileSavePicker();
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
                savePicker.SuggestedFileName = "ReceiptToEmail";
                StorageFile textFile = await savePicker.PickSaveFileAsync();
                StorageApplicationPermissions.FutureAccessList.Add(textFile);
                await FileIO.WriteTextAsync(textFile, invoice);

                da.AddReceipt((App.Current as App).ConnectionString,
                    (Application.Current as App).g_member.Name,
                    da.SelectEmail((App.Current as App).ConnectionString, (Application.Current as App).g_member.Name),
                    (Application.Current as App).g_shoppingcart.Deal.Quantity,
                    pizzaName,
                    (Application.Current as App).g_shoppingcart.Wings.Quantity,
                    (Application.Current as App).g_shoppingcart.Sandwiches.Quantity,
                    (Application.Current as App).g_shoppingcart.Poutine.Quantity,
                    Convert.ToDecimal(ViewModel.Total.ToString()),
                    date);

                (Application.Current as App).g_shoppingcart = new ShoppingCart();
                item.Clear();
                this.ViewModel = new HostViewModel(item);
                tbSubtotal.Text = ViewModel.Subtotal.ToString();
                tbTax.Text = ViewModel.Tax.ToString();
                tbTotal.Text = ViewModel.Total.ToString();
                btnCheck.IsEnabled = false;
                textCheckoutSuccess.Visibility = Visibility.Visible;
            }
            else
            {
                if(tbCheckoutEmail.Text == "")
                {
                    var messageDialog = new MessageDialog("Please enter a email.");
                    messageDialog.Commands.Add(new UICommand("Close",
                        new UICommandInvokedHandler(this.CommandInvokedHandler)));
                    await messageDialog.ShowAsync();
                }
                else
                {
                    string invoice = "Welcome Papa Dario's Pizza\n\n";
                    foreach (ShoppingCartItem spi in item)
                    {
                        invoice += spi.Name + " --- " + spi.Quantity + " --- " + spi.Price + "\n\n";

                        if (spi.Name.Contains("Pizza"))
                        {
                            pizzaName += spi.Name + " | ";
                        }
                    }
                    invoice += "\nSubtotal: $" + ViewModel.Subtotal.ToString()
                        + "\nTax: $" + ViewModel.Tax.ToString()
                        + "\nTotal: $" + ViewModel.Total.ToString()
                        + "\n\nDate: " + date;

                    var savePicker = new FileSavePicker();
                    savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
                    savePicker.SuggestedFileName = "ReceiptToEmail";
                    StorageFile textFile = await savePicker.PickSaveFileAsync();
                    StorageApplicationPermissions.FutureAccessList.Add(textFile);
                    await FileIO.WriteTextAsync(textFile, invoice);

                    string email = tbCheckoutEmail.Text;

                    da.AddReceipt((App.Current as App).ConnectionString,
                        null,
                        email,
                        (Application.Current as App).g_shoppingcart.Deal.Quantity,
                        pizzaName,
                        (Application.Current as App).g_shoppingcart.Wings.Quantity,
                        (Application.Current as App).g_shoppingcart.Sandwiches.Quantity,
                        (Application.Current as App).g_shoppingcart.Poutine.Quantity,
                        Convert.ToDecimal(ViewModel.Total.ToString()),
                        date);

                    (Application.Current as App).g_shoppingcart = new ShoppingCart();
                    item.Clear();
                    this.ViewModel = new HostViewModel(item);
                    tbSubtotal.Text = ViewModel.Subtotal.ToString();
                    tbTax.Text = ViewModel.Tax.ToString();
                    tbTotal.Text = ViewModel.Total.ToString();
                    btnCheck.IsEnabled = false;
                    textCheckoutSuccess.Visibility = Visibility.Visible;
                    tbCheckoutEmail.Text = "";
                }    
            }

        }

        private void CommandInvokedHandler(IUICommand command)
        {
        }

        
    }
}
