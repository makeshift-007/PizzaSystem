using System;
using Microsoft.AspNetCore.Mvc;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Services;

namespace PizzaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(productService.GetProducts());
            }
            catch (Exception ex)
            {
                //Logger Code
                return StatusCode(500, "Something went wrong!!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                var product = productService.GetProduct(id);

                if (product != null)
                    return Ok(product);
                else
                    return StatusCode(404);
            }
            catch (Exception ex)
            {
                //Logger Code
                return StatusCode(500, "Something went wrong!!");
            }
        }

    }
}
