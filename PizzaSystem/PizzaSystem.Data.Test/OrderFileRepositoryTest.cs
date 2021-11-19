using Moq;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;
using Xunit;

namespace PizzaSystem.Data.Test
{
    [Trait("Category", "Data")]
    public class OrderFileRepositoryTest
    {

        OrderFileRepository GenerateSUT(IFileDatabase fileDatabase)
        {

            return new OrderFileRepository(
                fileDatabase ?? new Mock<IFileDatabase>().Object
                );
        }

        [Fact(DisplayName = "Delete: Should delete data")]
        public void Delete_Should_Delete_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var order = new ConfirmedOrder
            {
                OrderID = Guid.NewGuid()
            };
            var orders = new List<ConfirmedOrder>()
            {
                order
            };
            fileDatabase.Setup(m => m.Read<List<ConfirmedOrder>>(It.IsAny<string>())).Returns(orders);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<ConfirmedOrder>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Delete(order.OrderID);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<ConfirmedOrder>>()), Times.Once);

        }

        [Fact(DisplayName = "GetOrders: Should Get data")]
        public void GetOrders_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var orders = new List<ConfirmedOrder>();
            fileDatabase.Setup(m => m.Read<List<ConfirmedOrder>>(It.IsAny<string>())).Returns(orders);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetOrders();

            //Assert
            Assert.Equal(orders, result);

        }

        [Fact(DisplayName = "GetOrder: Should Get data")]
        public void GetOrder_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var order = new ConfirmedOrder
            {
                OrderID = Guid.NewGuid()
            };
            var orders = new List<ConfirmedOrder>()
            {
                order
            };
            fileDatabase.Setup(m => m.Read<List<ConfirmedOrder>>(It.IsAny<string>())).Returns(orders);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetOrder(order.OrderID);

            //Assert
            Assert.Equal(order, result);

        }

        [Fact(DisplayName = "InsertNew: Should Insert data")]
        public void InsertNew_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var order = new ConfirmedOrder
            {
                OrderID = Guid.NewGuid()
            };
            var orders = new List<ConfirmedOrder>()
            {
                order
            };
            fileDatabase.Setup(m => m.Read<List<ConfirmedOrder>>(It.IsAny<string>())).Returns(orders);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<object>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.InsertNew(order);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<object>()), Times.Once);

        }

        [Fact(DisplayName = "Update: Should Update data")]
        public void Update_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var order = new ConfirmedOrder
            {
                OrderID = Guid.NewGuid()
            };
            var orders = new List<ConfirmedOrder>()
            {
                order
            };
            fileDatabase.Setup(m => m.Read<List<ConfirmedOrder>>(It.IsAny<string>())).Returns(orders);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<ConfirmedOrder>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Update(order);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<ConfirmedOrder>>()), Times.Once);

        }
    }
}
