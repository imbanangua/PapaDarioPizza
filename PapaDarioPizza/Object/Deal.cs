using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza.Object
{
    public class Deal
    {
        public string Name { get; set; }
        public double Price { get { return 12.99 * Quantity; } set { } }
        public int Quantity { get; set; }

        public Deal()
        {
            Name = "Today's Deal";
            Price = 12.99;
            Quantity = 0;
        }
    }
}
