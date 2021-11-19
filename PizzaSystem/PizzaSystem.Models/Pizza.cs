using System;
using System.Linq;

namespace PizzaSystem.Models
{

    public class Pizza : ItemBase
    {
        public double BasePrice { set; get; }
        public Guid Size { set; get; }
        public Guid Sauce { set; get; }
        public Guid Crust { set; get; }
        public string Description { set; get; }
        public bool IsVegPizza { set; get; }
    }





}
