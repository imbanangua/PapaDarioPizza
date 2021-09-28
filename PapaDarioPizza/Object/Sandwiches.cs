using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza.Object
{
    public class Sandwiches
    {
        public string Name { get; set; }
        public double Price { get { return 7.99 * Quantity; } set { } }
        public int Quantity { get; set; }

        public Sandwiches()
        {
            Name = "Sandwiches";
            Price = 7.99;
            Quantity = 0;
        }
    }
}
