using PizzaSystem.Models.Interfaces.Data;
using System;

namespace PizzaSystem.Models.Order
{
    public class ProductOrder : OrderBase
    {
       
        public Guid ProductID { set; get; }

        public double GetAmount(IProductRepository productRepository)
        {
            var product = productRepository.GetProduct(ProductID);
            if (product == null)
                throw new ArgumentException("Product Not Found");
            return product.Price * Quantity;
        }
    }




}
