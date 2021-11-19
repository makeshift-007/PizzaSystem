using PizzaSystem.Models.Interfaces.Data;

namespace PizzaSystem.Models.Order
{
    public class PizzaOrder : OrderBase
    {
        public PizzaIngredients PizzaIngredients { set; get; }        
        public double GetAmount(IIngredientRepository ingredientRepository,
           IPizzaRepository pizzaRepository)
        {
            return PizzaIngredients.GetAmount(ingredientRepository, pizzaRepository) * Quantity;
        }

    }




}
