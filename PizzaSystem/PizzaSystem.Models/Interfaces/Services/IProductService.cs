﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProduct(Guid id);
    }
}
