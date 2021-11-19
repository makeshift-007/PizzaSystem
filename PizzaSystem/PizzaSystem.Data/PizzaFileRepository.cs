using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSystem.Data
{
    public class PizzaFileRepository : IPizzaRepository
    {
        private readonly IFileDatabase fileDatabase;
        private const string repoPath = "PizzaFile.json";
        private List<Pizza> GetSavedPizzas()
        {
            var data = fileDatabase.Read<List<Pizza>>(repoPath);
            return data == null ? new List<Pizza>() : data;
        }
        private void SavePizzas(List<Pizza> pizzas)
        {
            fileDatabase.Write(repoPath, pizzas);
        }

        public PizzaFileRepository(IFileDatabase fileDatabase)
        {
            this.fileDatabase = fileDatabase;
        }

        public void Delete(Guid id)
        {
            var pizzas = GetSavedPizzas();
            if (pizzas.Any(m => m.ID == id))
            {
                pizzas.Remove(pizzas.FirstOrDefault(m => m.ID == id));
                SavePizzas(pizzas);
            }
        }

        public Pizza GetPizza(Guid id)
        {
            var pizzas = GetSavedPizzas();
            return pizzas.FirstOrDefault(m => m.ID == id);
        }

        public List<Pizza> GetPizzas() => GetSavedPizzas();

        public void InsertNew(Pizza pizza)
        {
            var pizzas = GetSavedPizzas();
            pizzas.Add(pizza);
            SavePizzas(pizzas);
        }

        public void Update(Pizza pizza)
        {
            var pizzas = GetSavedPizzas();
            if (pizzas.Any(m => m.ID == pizza.ID))
            {
                pizzas.Remove(pizzas.FirstOrDefault(m => m.ID == pizza.ID));
            }
            pizzas.Add(pizza);
            SavePizzas(pizzas);
        }
    }
}
