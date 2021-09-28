using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza.Object
{
    public class Pizza
    {
        public string Type { get; set; }
        public string Size { get; set; }
        public bool[] Topping { get; set; }
        public bool Combo { get; set; }
        public string Drink { get; set; }
        public string Side { get; set; }
        public double Price { 
            get 
            {
                double price = 0;
                switch (Size)
                {
                    case "Small": price += 12.99; break;
                    case "Medium": price += 14.99; break;
                    case "Large": price += 17.49; break;
                    case "Extra Large": price += 22.99; break;
                    default: break;
                }
                for (int i = 0; i < 9; i++)
                {
                    if (Topping[i] == true)
                    {
                        switch (i)
                        {
                            case 0: price += 1.99; break;
                            case 1: price += 1.99; break;
                            case 2: price += 1.99; break;
                            case 3: price += 2.99; break;
                            case 4: price += 2.99; break;
                            case 5: price += 1.99; break;
                            case 6: price += 2.99; break;
                            case 7: price += 2.99; break;
                            case 8: price += 2.99; break;
                            default: break;
                        }
                    }
                }
                if(Combo == true)
                {
                    price += 3.99;
                }
                return price; 
            } 
            set { } 
        }

        public Pizza()
        {
            Topping = new bool[9];
        }
    }
}
