using PizzaSystem.Data;
using PizzaSystem.Models;
using PizzaSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSystem.DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileDatabase = new FileDatabase();
            var productRepo = new ProductFileRepository(fileDatabase);
            var orderRepo = new OrderFileRepository(fileDatabase);
            var pizzaRepo = new PizzaFileRepository(fileDatabase);
            var ingredientRepo = new IngredientFileRepository(fileDatabase);

            GetProducts().ForEach(m => productRepo.InsertNew(m));
            var ingredients = GetIngredients();
            ingredients.ForEach(m => ingredientRepo.InsertNew(m));

            var crust = ingredients.FirstOrDefault(m => m.Type == IngredientType.CRUST);
            var sauce = ingredients.FirstOrDefault(m => m.Type == IngredientType.SAUCE);
            var size = ingredients.FirstOrDefault(m => m.Type == IngredientType.PIZZA_SIZE);

            GetPizzas(crust.ID, sauce.ID, size.ID).ForEach(m => pizzaRepo.InsertNew(m));
        }

        static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    ID=Guid.NewGuid(),
                    Name="French Fries",
                    Price=10,
                    ImageName="FrenchFries.jpg"
                },
                new Product
                {
                    ID=Guid.NewGuid(),
                    Name="Garlic Bread",
                    Price=10,
                    ImageName="GarlicBread.jpg"
                },
            };

        }
        static List<Ingredient> GetIngredients()
        {
            return new List<Ingredient>
            {
                new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="New Hand Tossed",
                    Price=169,
                    ImageName="NewHandTossed.jpg",
                    Type=IngredientType.CRUST
                },
                  new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="100% Wheat Thin Crust",
                    Price=169,
                    ImageName="WheatThinCrust.jpg",
                    Type=IngredientType.CRUST
                },
                  new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Regular",
                    Price=100,
                    ImageName="RegularSize.jpg",
                    Type=IngredientType.PIZZA_SIZE
                },
                  new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Medium",
                    Price=200,
                    ImageName="MediumSize.jpg",
                    Type=IngredientType.PIZZA_SIZE
                },
                  new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Large",
                    Price=300,
                    ImageName="LargeSize.jpg",
                    Type=IngredientType.PIZZA_SIZE
                },
                  new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Onion",
                    Price=10,
                    ImageName="Onion.jpg",
                    Type=IngredientType.TOPPING
                },
                   new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Tomato",
                    Price=10,
                    ImageName="Tomato.jpg",
                    Type=IngredientType.TOPPING
                },
                       new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Marinara",
                    Price=20,
                    ImageName="Marinara.jpg",
                    Type=IngredientType.SAUCE
                },
                       new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Cheese",
                    Price=20,
                    ImageName="Cheese.jpg",
                    Type=IngredientType.SAUCE
                }
                       ,
                       new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Ranch",
                    Price=20,
                    ImageName="Ranch.jpg",
                    Type=IngredientType.SAUCE
                } ,
                       new Ingredient
                {
                    ID=Guid.NewGuid(),
                    Name="Extra Cheese",
                    Price=20,
                    ImageName="Cheese.jpg",
                    Type=IngredientType.OPTIONAL
                }
            };

        }

        static List<Pizza> GetPizzas(Guid crustId, Guid sauce, Guid size)
        {
            return new List<Pizza>
            {
                new Pizza
                {
                    ID=Guid.NewGuid(),
                    Name="Peppy Panner",
                    BasePrice=100,
                    ImageName="PeppyPanner.jpg",
                    Crust=crustId,
                    Description="Flavorful trio of juicy paneer, crisp capsicum with spicy red paprika",
                    IsVegPizza=true,
                    Sauce=sauce,
                    Size=size
                },
                 new Pizza
                {
                    ID=Guid.NewGuid(),
                    Name="Farmhouse",
                    BasePrice=100,
                    ImageName="Farmhouse.jpg",
                    Crust=crustId,
                    Description="Delightful combination of onion, capsicum, tomato & grilled mushroom",
                    IsVegPizza=true,
                    Sauce=sauce,
                    Size=size
                },
                  new Pizza
                {
                    ID=Guid.NewGuid(),
                    Name="Margherita",
                    BasePrice=100,
                    ImageName="Margherita.jpg",
                    Crust=crustId,
                    Description="Classic delight with 100% real mozzarella cheese",
                    IsVegPizza=true,
                    Sauce=sauce,
                    Size=size
                }
            };

        }
    }
}
