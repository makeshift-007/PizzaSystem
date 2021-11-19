using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Interfaces.Services;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;

namespace PizzaSystem.Service
{

    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public List<Ingredient> GetIngredients(IngredientType? type = null)
        {
            return ingredientRepository.GetIngredients(type);
        }
        public Ingredient GetIngredient(Guid id)
        {
            return ingredientRepository.GetIngredient(id);
        }
    }
}
