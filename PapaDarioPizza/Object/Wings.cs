using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza.Object
{
    public class Wings
    {
        public string Name { get; set; }
        public double Price { get { return 9.99 * Quantity / 5; } set { } }
        public int Quantity { get; set; }

        public Wings()
        {
            Name = "Wings";
            Price = 9.99;
            Quantity = 0;
        }
    }
}
