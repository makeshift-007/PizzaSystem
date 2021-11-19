using Moq;
using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;
using Xunit;

namespace PizzaSystem.Data.Test
{
    [Trait("Category", "Data")]
    public class IngredientFileRepositoryTest
    {

        IngredientFileRepository GenerateSUT(IFileDatabase fileDatabase)
        {

            return new IngredientFileRepository(
                fileDatabase ?? new Mock<IFileDatabase>().Object
                );
        }

        [Fact(DisplayName = "Delete: Should delete data")]
        public void Delete_Should_Delete_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var ingredient = new Ingredient
            {
                ID = Guid.NewGuid()
            };
            var ingredients = new List<Ingredient>()
            {
                ingredient
            };
            fileDatabase.Setup(m => m.Read<List<Ingredient>>(It.IsAny<string>())).Returns(ingredients);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<Ingredient>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Delete(ingredient.ID);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Ingredient>>()), Times.Once);

        }

        [Fact(DisplayName = "GetIngredients: Should Get data")]
        public void GetIngredients_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var ingredients = new List<Ingredient>();
            fileDatabase.Setup(m => m.Read<List<Ingredient>>(It.IsAny<string>())).Returns(ingredients);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetIngredients();

            //Assert
            Assert.Equal(ingredients, result);

        }

        [Fact(DisplayName = "GetOGetIngredient: Should Get data")]
        public void GetIngredient_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var ingredient = new Ingredient
            {
                ID = Guid.NewGuid()
            };
            var ingredients = new List<Ingredient>()
            {
                ingredient
            };
            fileDatabase.Setup(m => m.Read<List<Ingredient>>(It.IsAny<string>())).Returns(ingredients);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetIngredient(ingredient.ID);

            //Assert
            Assert.Equal(ingredient, result);

        }

        [Fact(DisplayName = "InsertNew: Should Insert data")]
        public void InsertNew_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var ingredient = new Ingredient
            {
                ID = Guid.NewGuid()
            };
            var ingredients = new List<Ingredient>()
            {
                ingredient
            };
            fileDatabase.Setup(m => m.Read<List<Ingredient>>(It.IsAny<string>())).Returns(ingredients);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<object>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.InsertNew(ingredient);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Ingredient>>()), Times.Once);

        }

        [Fact(DisplayName = "Update: Should Update data")]
        public void Update_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var ingredient = new Ingredient
            {
                ID = Guid.NewGuid()
            };
            var ingredients = new List<Ingredient>()
            {
                ingredient
            };
            fileDatabase.Setup(m => m.Read<List<Ingredient>>(It.IsAny<string>())).Returns(ingredients);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<Ingredient>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Update(ingredient);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Ingredient>>()), Times.Once);

        }
    }
}
