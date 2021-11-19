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
    public class ProductsControllerTest
    {

        ProductsController GenerateSUT(IProductService productService)
        {
            return new ProductsController(productService ?? new Mock<IProductService>().Object);
        }

        [Fact(DisplayName = "Get: Should Return 500 when exception occured")]

        public void Get_Service_Throws_Exception_ReturnServerError()
        {
            //Arrange 
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(m => m.GetProducts()).Throws(new Exception());
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get() as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetProducts(), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact(DisplayName = "Get: Should Return Data on sucessfull service execution")]

        public void Get_Service_ReturnsData()
        {
            //Arrange 
            var serviceMock = new Mock<IProductService>();

            var products = new List<Product>();

            serviceMock.Setup(m => m.GetProducts()).Returns(products);

            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get() as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetProducts(), Times.Once);
            Assert.Equal(products, result.Value);
        }


        [Fact(DisplayName = "Get(ID): Should Return Data on sucessfull service execution")]

        public void Get_WithID_Service_ReturnsData()
        {
            //Arrange 
            var serviceMock = new Mock<IProductService>();

            var product = new Product();
            var requestId = Guid.NewGuid();

            serviceMock.Setup(m => m.GetProduct(requestId)).Returns(product);
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetProduct(requestId), Times.Once);
            Assert.Equal(product, result.Value);
        }



        [Fact(DisplayName = "Get(ID): Should Return 404 when data not found")]

        public void Get_WithID_Service_ReturnsNotFound()
        {
            //Arrange 

            var serviceMock = new Mock<IProductService>();

            var requestId = Guid.NewGuid();

            serviceMock.Setup(m => m.GetProduct(requestId)).Returns(default(Product));
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get(requestId) as StatusCodeResult;
            //Assert
            serviceMock.Verify(m => m.GetProduct(requestId), Times.Once);
            Assert.Equal(404, result.StatusCode);
        }



        [Fact(DisplayName = "Get(ID): Should Return 500 when service failed")]
        public void Get_WithID_Service_Throws_Exception_ReturnServerError()
        {
            //Arrange             
            var serviceMock = new Mock<IProductService>();
            var requestId = Guid.NewGuid();
            serviceMock.Setup(m => m.GetProduct(requestId)).Throws(new Exception());
            var sut = GenerateSUT(serviceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            serviceMock.Verify(m => m.GetProduct(requestId), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }
    }
}
