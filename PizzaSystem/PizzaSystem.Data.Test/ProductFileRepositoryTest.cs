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
    public class ProductFileRepositoryTest
    {

        ProductFileRepository GenerateSUT(IFileDatabase fileDatabase)
        {

            return new ProductFileRepository(
                fileDatabase ?? new Mock<IFileDatabase>().Object
                );
        }

        [Fact(DisplayName = "Delete: Should delete data")]
        public void Delete_Should_Delete_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var product = new Product
            {
                ID = Guid.NewGuid()
            };
            var products = new List<Product>()
            {
                product
            };
            fileDatabase.Setup(m => m.Read<List<Product>>(It.IsAny<string>())).Returns(products);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<Product>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Delete(product.ID);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Product>>()), Times.Once);

        }

        [Fact(DisplayName = "GetProducts: Should Get data")]
        public void GetProducts_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var products = new List<Product>();
            fileDatabase.Setup(m => m.Read<List<Product>>(It.IsAny<string>())).Returns(products);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetProducts();

            //Assert
            Assert.Equal(products, result);

        }

        [Fact(DisplayName = "GetProduct: Should Get data")]
        public void GetProduct_Should_Get_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var product = new Product
            {
                ID = Guid.NewGuid()
            };
            var products = new List<Product>()
            {
                product
            };
            fileDatabase.Setup(m => m.Read<List<Product>>(It.IsAny<string>())).Returns(products);


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            var result = sut.GetProduct(product.ID);

            //Assert
            Assert.Equal(product, result);

        }

        [Fact(DisplayName = "InsertNew: Should Insert data")]
        public void InsertNew_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var product = new Product
            {
                ID = Guid.NewGuid()
            };
            var products = new List<Product>()
            {
                product
            };
            fileDatabase.Setup(m => m.Read<List<Product>>(It.IsAny<string>())).Returns(products);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<Product>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.InsertNew(product);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Product>>()), Times.Once);

        }

        [Fact(DisplayName = "Update: Should Update data")]
        public void Update_Should_Insert_Data()
        {
            //Arrange 

            var fileDatabase = new Mock<IFileDatabase>();
            var product = new Product
            {
                ID = Guid.NewGuid()
            };
            var products = new List<Product>()
            {
                product
            };
            fileDatabase.Setup(m => m.Read<List<Product>>(It.IsAny<string>())).Returns(products);
            fileDatabase.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<List<Product>>()));


            var sut = GenerateSUT(fileDatabase.Object);


            //Act
            sut.Update(product);

            //Assert
            fileDatabase.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<List<Product>>()), Times.Once);

        }
    }
}
