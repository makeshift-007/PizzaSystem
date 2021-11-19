using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSystem.Data
{
    public class IngredientFileRepository : IIngredientRepository
    {
        private readonly IFileDatabase fileDatabase;
        private const string repoPath = "IngredientFile.json";

        private List<Ingredient> GetIngredients()
        {
            var data = fileDatabase.Read<List<Ingredient>>(repoPath);
            return data == null ? new List<Ingredient>() : data;
        }
        private void SaveIngredients(List<Ingredient> ingredients)
        {
            fileDatabase.Write(repoPath, ingredients);
        }

        public IngredientFileRepository(IFileDatabase fileDatabase)
        {
            this.fileDatabase = fileDatabase;
        }
        public void Delete(Guid id)
        {
            var ingredients = GetIngredients();
            if (ingredients.Any(m => m.ID == id))
            {
                ingredients.Remove(ingredients.FirstOrDefault(m => m.ID == id));
                SaveIngredients(ingredients);
            }
        }

        public Ingredient GetIngredient(Guid id)
        {
            var ingredients = GetIngredients();
            return ingredients.FirstOrDefault(m => m.ID == id);
        }

        public List<Ingredient> GetIngredients(IngredientType? type = null)
        {
            var ingredients = GetIngredients();
            return ingredients.Where(m => type == null || m.Type == type).ToList();
        }

        public void InsertNew(Ingredient ingredient)
        {
            var ingredients = GetIngredients();
            ingredients.Add(ingredient);
            SaveIngredients(ingredients);
        }

        public void Update(Ingredient ingredient)
        {
            var ingredients = GetIngredients();
            if (ingredients.Any(m => m.ID == ingredient.ID))
            {
                ingredients.Remove(ingredients.FirstOrDefault(m => m.ID == ingredient.ID));
            }
            ingredients.Add(ingredient);
            SaveIngredients(ingredients);
        }
    }
}
