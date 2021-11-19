using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Data
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetIngredients(IngredientType? type = null);
        Ingredient GetIngredient(Guid id);
        void InsertNew(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Delete(Guid id);
    }
}
