using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaSystem.API.Controllers;
using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace PizzaSystem.API.Test
{
    [Trait("Category", "Controllers")]
    public class IngredientsControllerTest
    {

        IngredientsController GenerateSUT(IIngredientService ingredientService)
        {
            return new IngredientsController(ingredientService ?? new Mock<IIngredientService>().Object);
        }

        [Theory(DisplayName = "Get: Should Return 500 when exception occured")]
        [InlineData(null)]
        [InlineData(IngredientType.CRUST)]
        public void Get_IngredientService_Throws_Exception_ReturnServerError(IngredientType? ingredientType)
        {
            //Arrange 
            var ingredientServiceMock = new Mock<IIngredientService>();
            ingredientServiceMock.Setup(m => m.GetIngredients(ingredientType)).Throws(new Exception());
            var sut = GenerateSUT(ingredientServiceMock.Object);
            //Act
            var result = sut.Get(ingredientType) as ObjectResult;
            //Assert
            ingredientServiceMock.Verify(m => m.GetIngredients(ingredientType), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact(DisplayName = "Get: Should Return Data on sucessfull service execution")]

        public void Get_IngredientService_ReturnsData()
        {
            //Arrange 
            var ingredientServiceMock = new Mock<IIngredientService>();

            var ingredients = new List<Ingredient>();

            ingredientServiceMock.Setup(m => m.GetIngredients(null)).Returns(ingredients);
            var sut = GenerateSUT(ingredientServiceMock.Object);
            //Act
            var result = sut.Get(null) as ObjectResult;
            //Assert
            ingredientServiceMock.Verify(m => m.GetIngredients(null), Times.Once);
            Assert.Equal(ingredients, result.Value);
        }


        [Fact(DisplayName = "Get(ID): Should Return Data on sucessfull service execution")]

        public void Get_WithID_IngredientService_ReturnsData()
        {
            //Arrange 
            var ingredientServiceMock = new Mock<IIngredientService>();

            var ingredient = new Ingredient();
            var requestId = Guid.NewGuid();

            ingredientServiceMock.Setup(m => m.GetIngredient(requestId)).Returns(ingredient);
            var sut = GenerateSUT(ingredientServiceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            ingredientServiceMock.Verify(m => m.GetIngredient(requestId), Times.Once);
            Assert.Equal(ingredient, result.Value);
        }



        [Fact(DisplayName = "Get(ID): Should Return 404 when data not found")]

        public void Get_WithID_IngredientService_ReturnsNotFound()
        {
            //Arrange 
            var ingredientServiceMock = new Mock<IIngredientService>();

            var requestId = Guid.NewGuid();

            ingredientServiceMock.Setup(m => m.GetIngredient(requestId)).Returns(default(Ingredient));
            var sut = GenerateSUT(ingredientServiceMock.Object);
            //Act
            var result = sut.Get(requestId) as StatusCodeResult;
            //Assert
            ingredientServiceMock.Verify(m => m.GetIngredient(requestId), Times.Once);
            Assert.Equal(404, result.StatusCode);
        }



        [Fact(DisplayName = "Get(ID): Should Return 500 when service failed")]
        public void Get_WithID_IngredientService_Throws_Exception_ReturnServerError()
        {
            //Arrange 
            var ingredientServiceMock = new Mock<IIngredientService>();
            var requestId = Guid.NewGuid();
            ingredientServiceMock.Setup(m => m.GetIngredient(requestId)).Throws(new Exception());
            var sut = GenerateSUT(ingredientServiceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            ingredientServiceMock.Verify(m => m.GetIngredient(requestId), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }
    }
}
