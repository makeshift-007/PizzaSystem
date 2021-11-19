using PizzaSystem.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSystem.Models.Order
{
    public class Order
    {
        public List<PizzaOrder> Pizzas { set; get; }
        public List<ProductOrder> Products { set; get; }
        public double GetAmount(IIngredientRepository ingredientRepository,
            IPizzaRepository pizzaRepository, IProductRepository productRepository)
        {
            return (Pizzas != null ? Pizzas.Sum(m => m.GetAmount(ingredientRepository, pizzaRepository)) : 0)
                     + (Products != null ? Products.Sum(m => m.GetAmount(productRepository)) : 0);
        }
    }

    public class ConfirmedOrder
    {
        public Order Order { set; get; }
        public Guid OrderID { set; get; }
        public DateTime OrderTime { set; get; }
        public double Amount { set; get; }
        public ConfirmedOrder()
        {

        }
        public ConfirmedOrder(Order order, IIngredientRepository ingredientRepository,
            IPizzaRepository pizzaRepository, IProductRepository productRepository)
        {
            Order = order;
            Amount = order.GetAmount(ingredientRepository, pizzaRepository, productRepository);
        }
    }
}
