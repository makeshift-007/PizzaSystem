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
    public class PizzaFileRepositoryTest
    {

        PizzaFileRepository GenerateSUT(IFileDatabase fileDatabase)
        {

            return new PizzaFileRepository(
                fileDatabase ?? new Mock<IFileDatabase>().Object
                );
        }

        [Fact(DisplayName = "Delete: Should delete data")]
        public void Delete_Should_Delete_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var pizza = new Pizza
            {
                ID = Guid.NewGuid()
            };
            var pizzas = new List<Pizza>()
            {
                pizza
            };
            fileDatabase.Setup(m => m.Read<List<Pizza>>(It.IsAny<string>())).Returns(pizzas);

            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<object>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Delete(pizza.ID);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<object>()), Times.Once);

        }

        [Fact(DisplayName = "GetIngredients: Should Get data")]
        public void GetIngredients_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var pizzas = new List<Pizza>();
            fileDatabase.Setup(m => m.Read<List<Pizza>>(It.IsAny<string>())).Returns(pizzas);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetPizzas();

            //Assert
            Assert.Equal(pizzas, result);

        }

        [Fact(DisplayName = "GetPizza: Should Get data")]
        public void GetPizza_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var pizza = new Pizza
            {
                ID = Guid.NewGuid()
            };
            var pizzas = new List<Pizza>()
            {
                pizza
            };
            fileDatabase.Setup(m => m.Read<List<Pizza>>(It.IsAny<string>())).Returns(pizzas);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetPizza(pizza.ID);

            //Assert
            Assert.Equal(pizza, result);

        }

        [Fact(DisplayName = "InsertNew: Should Insert data")]
        public void InsertNew_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var pizza = new Pizza
            {
                ID = Guid.NewGuid()
            };
            var pizzas = new List<Pizza>()
            {
                pizza
            };
            fileDatabase.Setup(m => m.Read<List<Pizza>>(It.IsAny<string>())).Returns(pizzas);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<Pizza>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.InsertNew(pizza);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Pizza>>()), Times.Once);

        }

        [Fact(DisplayName = "Update: Should Update data")]
        public void Update_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var pizza = new Pizza
            {
                ID = Guid.NewGuid()
            };
            var pizzas = new List<Pizza>()
            {
                pizza
            };
            fileDatabase.Setup(m => m.Read<List<Pizza>>(It.IsAny<string>())).Returns(pizzas);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<Pizza>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Update(pizza);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Pizza>>()), Times.Once);

        }
    }
}
