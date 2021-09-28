using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PapaDarioPizza.Object
{
    public class HostViewModel : INotifyPropertyChanged
    {
        private double subtotal;
        private double tax;
        private double total;
        private bool buttonEnable;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public HostViewModel(ObservableCollection<ShoppingCartItem> item)
        {
            double num;
            num = (Application.Current as App).g_shoppingcart.Deal.Price
                + (Application.Current as App).g_shoppingcart.Wings.Price
                + (Application.Current as App).g_shoppingcart.Sandwiches.Price
                + (Application.Current as App).g_shoppingcart.Poutine.Price;
            foreach (Pizza p in (Application.Current as App).g_shoppingcart.Pizza)
            {
                num += p.Price;
            }

            if ((Application.Current as App).g_member.Name != null)
            {
                this.Subtotal = Math.Round(num*0.9, 2);
                this.Tax = Math.Round(this.Subtotal * 0.13, 2);
                this.Total = Math.Round(this.Subtotal * 1.13, 2);
            }
            else
            {
                this.Subtotal = Math.Round(num, 2);
                this.Tax = Math.Round(this.Subtotal * 0.13, 2);
                this.Total = Math.Round(this.Subtotal * 1.13, 2);
            }

            this.ButtonEnable = CheckBottonEnable(item);

        }

        public double Subtotal
        {
            get 
            {
                return this.subtotal;
            }
            set
            {
                this.subtotal = value;
                this.OnPropertyChanged();
            }
        }
        public double Tax
        {
            get
            {
                return this.tax;
            }
            set
            {
                this.tax = value;
                this.OnPropertyChanged();
            }
        }
        public double Total
        {
            get
            {
                return this.total;
            }
            set
            {
                this.total = value;
                this.OnPropertyChanged();
            }
        }

        public bool ButtonEnable
        {
            get { return this.buttonEnable; }
            set
            {
                this.buttonEnable = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CheckBottonEnable(ObservableCollection<ShoppingCartItem> item)
        {
            return item.Any();
        }

    }
}
