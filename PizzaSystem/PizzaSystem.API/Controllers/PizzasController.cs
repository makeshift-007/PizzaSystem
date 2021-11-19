using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Services;

namespace PizzaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaService pizzaService;

        public PizzasController(IPizzaService pizzaService)
        {
            this.pizzaService = pizzaService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(pizzaService.GetPizzas());
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
                var pizza = pizzaService.GetPizza(id);

                if (pizza != null)
                    return Ok(pizza);
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
