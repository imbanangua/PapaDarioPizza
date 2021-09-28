using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza.Object
{
    public class Poutine
    {
        public string Name { get; set; }
        public double Price { get { return 6.99 * Quantity; } set { } }
        public int Quantity { get; set; }

        public Poutine()
        {
            Name = "Poutine";
            Price = 6.99;
            Quantity = 0;
        }
    }
}
