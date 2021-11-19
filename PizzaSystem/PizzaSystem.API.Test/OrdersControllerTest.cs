using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaSystem.API.Controllers;
using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces.Services;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;
using Xunit;

namespace PizzaSystem.API.Test
{
    [Trait("Category", "Controllers")]
    public class OrdersControllerTest
    {

        OrdersController GenerateSUT(IOrderService orderService)
        {
            return new OrdersController(orderService ?? new Mock<IOrderService>().Object);
        }


        [Fact(DisplayName = "Get(ID): Should Return Data on sucessfull service execution")]

        public void Get_WithID_Service_ReturnsData()
        {
            //Arrange 
            var orderServiceMock = new Mock<IOrderService>();

            var order = new ConfirmedOrder();
            var requestId = Guid.NewGuid();

            orderServiceMock.Setup(m => m.GetOrder(requestId)).Returns(order);
            var sut = GenerateSUT(orderServiceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            orderServiceMock.Verify(m => m.GetOrder(requestId), Times.Once);
            Assert.Equal(order, result.Value);
        }



        [Fact(DisplayName = "Get(ID): Should Return 404 when data not found")]

        public void Get_WithID_Service_ReturnsNotFound()
        {
            //Arrange 
            var orderServiceMock = new Mock<IOrderService>();

            var requestId = Guid.NewGuid();

            orderServiceMock.Setup(m => m.GetOrder(requestId)).Returns(default(ConfirmedOrder));
            var sut = GenerateSUT(orderServiceMock.Object);
            //Act
            var result = sut.Get(requestId) as StatusCodeResult;
            //Assert
            orderServiceMock.Verify(m => m.GetOrder(requestId), Times.Once);
            Assert.Equal(404, result.StatusCode);
        }



        [Fact(DisplayName = "Get(ID): Should Return 500 when service failed")]
        public void Get_WithID_Service_Throws_Exception_ReturnServerError()
        {
            //Arrange 
            var orderServiceMock = new Mock<IOrderService>();
            var requestId = Guid.NewGuid();
            orderServiceMock.Setup(m => m.GetOrder(requestId)).Throws(new Exception());
            var sut = GenerateSUT(orderServiceMock.Object);
            //Act
            var result = sut.Get(requestId) as ObjectResult;
            //Assert
            orderServiceMock.Verify(m => m.GetOrder(requestId), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact(DisplayName = "POST: Should Return 201 when Data inserted sucessfully")]
        public void Post_Service_Saves_Data_Returns_Created()
        {
            //Arrange 
            var orderServiceMock = new Mock<IOrderService>();
            var resultId = Guid.NewGuid();
            var order = new Order();
            orderServiceMock.Setup(m => m.PlaceOrder(order)).Returns(resultId);
            var sut = GenerateSUT(orderServiceMock.Object);
            //Act
            var result = sut.Post(order) as ObjectResult;
            //Assert
            orderServiceMock.Verify(m => m.PlaceOrder(order), Times.Once);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact(DisplayName = "POST: Should Return 400 when Bad Data sent")]
        public void Post_Service_Throws_Exception_ReturnsBadRequest()
        {
            //Arrange 
            var orderServiceMock = new Mock<IOrderService>();
            var resultId = Guid.NewGuid();
            var order = new Order();
            orderServiceMock.Setup(m => m.PlaceOrder(order)).Throws(new ArgumentException("DUMMY"));
            var sut = GenerateSUT(orderServiceMock.Object);
            //Act
            var result = sut.Post(order) as ObjectResult;
            //Assert
            orderServiceMock.Verify(m => m.PlaceOrder(order), Times.Once);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact(DisplayName = "POST: Should Return 500 when Bad Data sent")]
        public void Post_Service_Throws_Exception_ReturnsServerExecption()
        {
            //Arrange 
            var orderServiceMock = new Mock<IOrderService>();
            var resultId = Guid.NewGuid();
            var order = new Order();
            orderServiceMock.Setup(m => m.PlaceOrder(order)).Throws(new Exception("DUMMY"));
            var sut = GenerateSUT(orderServiceMock.Object);
            //Act
            var result = sut.Post(order) as ObjectResult;
            //Assert
            orderServiceMock.Verify(m => m.PlaceOrder(order), Times.Once);
            Assert.Equal(500, result.StatusCode);
        }
    }
}
