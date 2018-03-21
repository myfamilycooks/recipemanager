using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/Recipe")]
    [CustomExceptionFilter]
    public class RecipeController : Controller
    {
        private IRecipeService RecipeService { get; set; }
        public RecipeController(IRecipeService recipeService) { RecipeService = recipeService; }

        [Authorize, Route("{id:guid}"), HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var recipe = await RecipeService.GetByIdAsync(id);
                return Ok(recipe);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize, Route("{key}"), HttpGet]
        public async Task<IActionResult> GetByKey(string key)
        {
            try
            {
                var recipe = await RecipeService.GetByKeyAsync(key);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody]Recipe recipe)
        {
            try
            {
                var createdRecipe = await RecipeService.CreateAsync(recipe);
                return Ok(createdRecipe);
            }
            catch (BistroFiftyTwoDuplicateRecipeException)
            {
                return StatusCode(409, new { Error = "A recipe with the same title has already been created by you.  Please use a different title"});
            }

        }

    }
}
