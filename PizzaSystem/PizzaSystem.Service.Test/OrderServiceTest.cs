using Moq;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PizzaSystem.Service.Test
{
    [Trait("Category", "Services")]
    public class OrderServiceTest
    {

        OrderService GenerateSUT(IOrderRepository orderRepository, IIngredientRepository ingredientRepository,
            IPizzaRepository pizzaRepository, IProductRepository productRepository)
        {

            return new OrderService(
                orderRepository ?? new Mock<IOrderRepository>().Object,
                ingredientRepository ?? new Mock<IIngredientRepository>().Object,
                pizzaRepository ?? new Mock<IPizzaRepository>().Object,
                productRepository ?? new Mock<IProductRepository>().Object
                );
        }

        [Fact(DisplayName = "PlaceOrder: Should Return Data Guid(OrderId) on success")]

        public void PlaceOrder_DataSaved_Returns_OrderID()
        {
            //Arrange 
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var ingredientRepository = new Mock<IIngredientRepository>();
            var pizzaRepository = new Mock<IPizzaRepository>();
            var productRepository = new Mock<IProductRepository>();

            var request = new Order
            {
                Pizzas = new System.Collections.Generic.List<PizzaOrder>
                {
                    new PizzaOrder{
                        Quantity=1,
                        PizzaIngredients=new PizzaIngredients{
                            Crust=Guid.NewGuid(),
                            PizzaID=Guid.NewGuid(),
                            Optionals=new System.Collections.Generic.List<Guid>
                            {
                                Guid.NewGuid()
                            },
                            Sauce=Guid.NewGuid(),
                            Toppings=new System.Collections.Generic.List<Guid>
                            {
                                Guid.NewGuid()
                            },
                        }
                    }
                },
                Products = new System.Collections.Generic.List<ProductOrder>
                {
                    new ProductOrder
                    {
                        ProductID=  Guid.NewGuid(),
                        Quantity=1
                    }
                }
            };

            //Capture.In(args)

            var args = new List<ConfirmedOrder>();

            var pizza = request.Pizzas.FirstOrDefault();
            ingredientRepository.Setup(m => m.GetIngredient(It.IsAny<Guid>())).Returns(new Models.Ingredient { Price = 1 });
            pizzaRepository.Setup(m => m.GetPizza(It.IsAny<Guid>())).Returns(new Models.Pizza { BasePrice = 1 });
            productRepository.Setup(m => m.GetProduct(It.IsAny<Guid>())).Returns(new Models.Product { Price = 1 });
            orderRepositoryMock.Setup(m => m.InsertNew(Capture.In(args)));


            var sut = GenerateSUT(orderRepositoryMock.Object, ingredientRepository.Object,
                pizzaRepository.Object, productRepository.Object);


            //Act
            var result = sut.PlaceOrder(request);

            //Assert

            Assert.Equal(args.FirstOrDefault().OrderID, result);
        }

        [Fact(DisplayName = "GetOrder: Should Return Data ")]

        public void GetOrder_ReturnsData()
        {
            //Arrange 
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var ingredientRepository = new Mock<IIngredientRepository>();
            var pizzaRepository = new Mock<IPizzaRepository>();
            var productRepository = new Mock<IProductRepository>();

            
            var requestId = Guid.NewGuid();
            var order = new ConfirmedOrder();

            orderRepositoryMock.Setup(m => m.GetOrder(requestId)).Returns(order);


            var sut = GenerateSUT(orderRepositoryMock.Object, ingredientRepository.Object,
             pizzaRepository.Object, productRepository.Object);

            //Act
            var result = sut.GetOrder(requestId);
            //Assert            
            Assert.Equal(order, result);
        }


    }
}
