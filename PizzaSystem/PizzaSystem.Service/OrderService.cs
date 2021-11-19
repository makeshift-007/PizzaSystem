using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Interfaces.Services;
using PizzaSystem.Models.Order;
using System;

namespace PizzaSystem.Service
{

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IPizzaRepository pizzaRepository;
        private readonly IProductRepository productRepository;

        public OrderService(IOrderRepository orderRepository, IIngredientRepository ingredientRepository,
            IPizzaRepository pizzaRepository, IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.ingredientRepository = ingredientRepository;
            this.pizzaRepository = pizzaRepository;
            this.productRepository = productRepository;
        }

        public Guid PlaceOrder(Order order)
        {
            var confirmedOrder = new ConfirmedOrder(order, ingredientRepository, pizzaRepository, productRepository)
            {
                OrderID = Guid.NewGuid(),
                OrderTime = DateTime.Now
            };
            orderRepository.InsertNew(confirmedOrder);
            return confirmedOrder.OrderID;
        }

        public ConfirmedOrder GetOrder(Guid orderId)
        {          
            return orderRepository.GetOrder(orderId);            
        }

    }
}
