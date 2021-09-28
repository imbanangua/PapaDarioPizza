using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza.Object
{
    public class ShoppingCartItem
    {
        public string Name {  get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public int Index { get; set; }
        public int PizzaIndex { get; set; }
    }
}
