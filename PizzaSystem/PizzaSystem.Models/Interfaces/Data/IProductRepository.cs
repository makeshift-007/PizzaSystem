using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Data
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProduct(Guid id);
        void InsertNew(Product product);
        void Update(Product product);
        void Delete(Guid id);
    }
}
