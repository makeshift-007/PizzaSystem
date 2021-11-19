using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Services
{
    public interface IPizzaService
    {
        List<Pizza> GetPizzas();
        Pizza GetPizza(Guid id);
    }
}
