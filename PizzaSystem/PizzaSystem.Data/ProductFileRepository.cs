using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSystem.Data
{
    public class ProductFileRepository : IProductRepository
    {
        private readonly IFileDatabase fileDatabase;
        private const string repoPath = "ProductFile.json";
        private List<Product> GetSavedProducts()
        {
            var data = fileDatabase.Read<List<Product>>(repoPath);
            return data == null ? new List<Product>() : data;
        }
        private void SaveProducts(List<Product> products)
        {
            fileDatabase.Write(repoPath, products);
        }

        public ProductFileRepository(IFileDatabase fileDatabase)
        {
            this.fileDatabase = fileDatabase;
        }
        public void Delete(Guid id)
        {
            var products = GetSavedProducts();
            if (products.Any(m => m.ID == id))
            {
                products.Remove(products.FirstOrDefault(m => m.ID == id));
                SaveProducts(products);
            }
        }

        public Product GetProduct(Guid id)
        {
            var products = GetSavedProducts();
            return products.FirstOrDefault(m => m.ID == id);
        }

        public List<Product> GetProducts() => GetSavedProducts();

        public void InsertNew(Product product)
        {
            var products = GetSavedProducts();
            products.Add(product);
            SaveProducts(products);
        }

        public void Update(Product product)
        {
            var products = GetSavedProducts();
            if (products.Any(m => m.ID == product.ID))
            {
                products.Remove(products.FirstOrDefault(m => m.ID == product.ID));
            }
            products.Add(product);
            SaveProducts(products);
        }
    }
}
