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
    public class PizzaServiceTest
    {

        PizzaService GenerateSUT(
            IPizzaRepository pizzaRepository)
        {

            return new PizzaService(pizzaRepository ?? new Mock<IPizzaRepository>().Object);
        }
        [Fact(DisplayName = "GetPizzas: Should Return Data on success")]

        public void GetPizzas_Returns_Data()
        {
            //Arrange 

            var pizzaRepository = new Mock<IPizzaRepository>();
            var data = new List<Pizza>();
            pizzaRepository.Setup(m => m.GetPizzas()).Returns(data);


            var sut = GenerateSUT(pizzaRepository.Object);


            //Act
            var result = sut.GetPizzas();

            //Assert

            Assert.Equal(data, result);
        }

        [Fact(DisplayName = "GetPizzas: Should Return Data ")]

        public void GetPizzas_ReturnsData()
        {
            //Arrange 

            var pizzaRepository = new Mock<IPizzaRepository>();
            var requestId = Guid.NewGuid();
            var data = new Pizza();

            pizzaRepository.Setup(m => m.GetPizza(requestId)).Returns(data);


            var sut = GenerateSUT(pizzaRepository.Object);

            //Act
            var result = sut.GetPizza(requestId);
            //Assert            
            Assert.Equal(data, result);
        }
    }
}
