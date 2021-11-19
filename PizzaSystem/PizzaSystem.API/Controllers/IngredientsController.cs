using System;
using Microsoft.AspNetCore.Mvc;
using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Services;

namespace PizzaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }


        [HttpGet()]
        public ActionResult Get(IngredientType? ingredientType = null)
        {
            try
            {
                return Ok(ingredientService.GetIngredients(ingredientType));
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
                var ingredient = ingredientService.GetIngredient(id);

                if (ingredient != null)
                    return Ok(ingredient);
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
