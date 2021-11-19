using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Services
{
    public interface IIngredientService
    {
        List<Ingredient> GetIngredients(IngredientType? type = null);
        Ingredient GetIngredient(Guid id);
    }
}
