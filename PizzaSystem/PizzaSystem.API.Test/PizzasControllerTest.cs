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
    public class PizzasControllerTest
    {

        PizzasController GenerateSUT(IPizzaService pizzaService)
        {
            return new PizzasController(pizzaService ?? new Mock<IPizzaService>().Object);
        }

        [Fact(DisplayName = "Get: Should Return 500 when exception occured")]

        public void Get_Service_Throws_Exception_ReturnServerError()
        {
            //Arrange 
            var serviceMock = new Mock<IPizzaService>();
            serviceMock.Setup(m => m.GetPizzas()).Throws(new Exception());
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get() as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetPizzas(), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact(DisplayName = "Get: Should Return Data on sucessfull service execution")]

        public void Get_Service_ReturnsData()
        {
            //Arrange 
            var serviceMock = new Mock<IPizzaService>();

            var pizzas = new List<Pizza>();

            serviceMock.Setup(m => m.GetPizzas()).Returns(pizzas);

            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get() as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetPizzas(), Times.Once);
            Assert.Equal(pizzas, result.Value);
        }


        [Fact(DisplayName = "Get(ID): Should Return Data on sucessfull service execution")]

        public void Get_WithID_Service_ReturnsData()
        {
            //Arrange 
            var serviceMock = new Mock<IPizzaService>();
            
            var pizza = new Pizza();
            var requestId = Guid.NewGuid();

            serviceMock.Setup(m => m.GetPizza(requestId)).Returns(pizza);
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetPizza(requestId), Times.Once);
            Assert.Equal(pizza, result.Value);
        }



        [Fact(DisplayName = "Get(ID): Should Return 404 when data not found")]

        public void Get_WithID_Service_ReturnsNotFound()
        {
            //Arrange 
            
            var serviceMock = new Mock<IPizzaService>();

            var requestId = Guid.NewGuid();

            serviceMock.Setup(m => m.GetPizza(requestId)).Returns(default(Pizza));
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get(requestId) as StatusCodeResult;
            //Assert
            serviceMock.Verify(m => m.GetPizza(requestId), Times.Once);
            Assert.Equal(404, result.StatusCode);
        }



        [Fact(DisplayName = "Get(ID): Should Return 500 when service failed")]
        public void Get_WithID_Service_Throws_Exception_ReturnServerError()
        {
            //Arrange             
            var serviceMock = new Mock<IPizzaService>();
            var requestId = Guid.NewGuid();
            serviceMock.Setup(m => m.GetPizza(requestId)).Throws(new Exception());
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetPizza(requestId), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }
    }
}
