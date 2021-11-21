using PizzaSystem.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSystem.Models.Order
{

    public class PizzaIngredients
    {


        public Guid PizzaID { set; get; }
        public List<Guid> Toppings { set; get; }
        public List<Guid> Optionals { set; get; }
        public Guid Sauce { set; get; }
        public Guid Crust { set; get; }
        public Guid Size { set; get; }


        public double GetAmount(IIngredientRepository ingredientRepository,
           IPizzaRepository pizzaRepository)
        {
            var pizza = pizzaRepository.GetPizza(PizzaID);
            var sauce = ingredientRepository.GetIngredient(Sauce);
            var crust = ingredientRepository.GetIngredient(Crust);
            var size = ingredientRepository.GetIngredient(Size);

            var toppings = new List<Ingredient>();
            if (Toppings != null)
                foreach (var topping in Toppings)
                {
                    var toppingDetail = ingredientRepository.GetIngredient(topping);
                    if (toppingDetail == null)
                        throw new ArgumentException("Invalid Ingredient");
                    toppings.Add(toppingDetail);
                }

            var optionals = new List<Ingredient>();
            if (Optionals != null)
                foreach (var optional in Optionals)
                {
                    var optionalDetail = ingredientRepository.GetIngredient(optional);
                    if (optionalDetail == null)
                        throw new ArgumentException("Invalid Ingredient");
                    optionals.Add(optionalDetail);
                }

            if (pizza == null || sauce == null || crust == null)
                throw new ArgumentException("Invalid Ingredient");

            return pizza.BasePrice + sauce.Price + crust.Price + size.Price +
                toppings.Sum(m => m.Price) + optionals.Sum(m => m.Price);
        }

    }

}
