using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace PizzaSystem.Service
{
   
    public class ProductService : IProductService
    {

        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<Product> GetProducts()
        {
            return productRepository.GetProducts();
        }
        public Product GetProduct(Guid id)
        {
            return productRepository.GetProduct(id);
        }

    }
}
