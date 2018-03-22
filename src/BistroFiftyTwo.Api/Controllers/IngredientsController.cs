using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ingredients")]
    public class IngredientsController : Controller
    {
        public IngredientsController(IRecipeIngredientRepository recipeIngredientRepository)
        {
            RecipeIngredientRepository = recipeIngredientRepository;
        }

        protected IRecipeIngredientRepository RecipeIngredientRepository { get; set; }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var ingredientList = new List<Ingredient>();
                var ingredients = await RecipeIngredientRepository.GetAllAsync();
                var rand = new Random(1);

                foreach (var ingredient in ingredients)
                {
                    var r1 = rand.Next(10);
                    var r2 = rand.NextDouble();

                    ingredientList.Add(new Ingredient
                    {
                        Id = GenerateId(),
                        Name = ingredient.Ingredient,
                        Description = $"{ingredient.Ingredient}",
                        Price = Math.Round(r1 * (decimal) r2, 2)
                    });
                }

                return Ok(new {rows = ingredientList});
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        private string GenerateId()
        {
            long i = 1;
            foreach (var b in Guid.NewGuid().ToByteArray())
                i *= b + 1;
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}