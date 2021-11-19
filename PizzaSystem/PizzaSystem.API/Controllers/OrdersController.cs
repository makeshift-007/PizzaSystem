using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Services;
using PizzaSystem.Models.Order;

namespace PizzaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }


        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            try
            {
                var orderId = orderService.PlaceOrder(order);
                return StatusCode(201, new { OrderId = orderId });
            }
            catch (ArgumentException ae)
            {
                return StatusCode(400, ae.Message);
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
                var order = orderService.GetOrder(id);

                if (order != null)
                    return Ok(order);
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
