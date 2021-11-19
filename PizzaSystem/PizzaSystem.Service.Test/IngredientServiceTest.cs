using Moq;
using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PizzaSystem.Service.Test
{
    [Trait("Category", "Services")]
    public class IngredientServiceTest
    {

        IngredientService GenerateSUT(IIngredientRepository ingredientRepository)
        {

            return new IngredientService(
                ingredientRepository ?? new Mock<IIngredientRepository>().Object
                );
        }

        [Fact(DisplayName = "GetIngredients: Should Return Data on success")]

        public void GetIngredients_Returns_Data()
        {
            //Arrange 

            var ingredientRepository = new Mock<IIngredientRepository>();
            var data = new List<Ingredient>();
            ingredientRepository.Setup(m => m.GetIngredients(null)).Returns(data);


            var sut = GenerateSUT(ingredientRepository.Object);


            //Act
            var result = sut.GetIngredients();

            //Assert

            Assert.Equal(data, result);
        }

        [Fact(DisplayName = "GetIngredient: Should Return Data ")]

        public void GetGetIngredient_ReturnsData()
        {
            //Arrange 

            var ingredientRepository = new Mock<IIngredientRepository>();
            var requestId = Guid.NewGuid();
            var data = new Ingredient();

            ingredientRepository.Setup(m => m.GetIngredient(requestId)).Returns(data);


            var sut = GenerateSUT(ingredientRepository.Object);

            //Act
            var result = sut.GetIngredient(requestId);
            //Assert            
            Assert.Equal(data, result);
        }


    }
}
