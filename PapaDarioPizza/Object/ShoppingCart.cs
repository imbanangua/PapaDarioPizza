using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza.Object
{
    public class ShoppingCart
    {
        public Deal Deal { get; set; }

        public Wings Wings { get; set; }
        public Sandwiches Sandwiches { get; set; }
        public Poutine Poutine { get; set; }

        public ArrayList Pizza { get; set; }

        public ShoppingCart()
        {
            Deal = new Deal();
            Pizza = new ArrayList();
            Wings = new Wings();
            Sandwiches = new Sandwiches();
            Poutine = new Poutine();
        }
    }
}
