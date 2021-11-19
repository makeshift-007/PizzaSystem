using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Interfaces.Services;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;

namespace PizzaSystem.Service
{    
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {

            this.pizzaRepository = pizzaRepository;
        }

        public List<Pizza> GetPizzas()
        {
            return pizzaRepository.GetPizzas();
        }
        public Pizza GetPizza(Guid id)
        {
            return pizzaRepository.GetPizza(id);
        }

    }
}
