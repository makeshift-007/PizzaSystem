using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Data
{
    public interface IPizzaRepository
    {
        List<Pizza> GetPizzas();
        Pizza GetPizza(Guid id);
        void InsertNew(Pizza pizza);
        void Update(Pizza pizza);
        void Delete(Guid id);
    }
}
