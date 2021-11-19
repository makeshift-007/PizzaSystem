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
    public class ProductServiceTest
    {

        ProductService GenerateSUT(
            IProductRepository productRepository)
        {

            return new ProductService(productRepository ?? new Mock<IProductRepository>().Object);
        }
        [Fact(DisplayName = "GetProducts: Should Return Data on success")]

        public void GetProducts_Returns_Data()
        {
            //Arrange 

            var productRepository = new Mock<IProductRepository>();
            var data = new List<Product>();
            productRepository.Setup(m => m.GetProducts()).Returns(data);


            var sut = GenerateSUT(productRepository.Object);


            //Act
            var result = sut.GetProducts();

            //Assert

            Assert.Equal(data, result);
        }

        [Fact(DisplayName = "GetProduct: Should Return Data ")]

        public void GetProduct_ReturnsData()
        {
            //Arrange 

            var productRepository = new Mock<IProductRepository>();
            var requestId = Guid.NewGuid();
            var data = new Product();

            productRepository.Setup(m => m.GetProduct(requestId)).Returns(data);


            var sut = GenerateSUT(productRepository.Object);

            //Act
            var result = sut.GetProduct(requestId);
            //Assert            
            Assert.Equal(data, result);
        }
    }
}
