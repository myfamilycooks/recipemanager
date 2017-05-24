using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BistroFiftyTwo.WebApp.Controllers
{

    [Produces("application/json")]
    [Route("api/Recipe")]

    public class RecipeController : Controller
    {
        private IRecipeService RecipeService { get; set; }
        public RecipeController(IRecipeService recipeService) { RecipeService = recipeService; }

        [Authorize, Route("{id:guid}")]
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

        [Authorize, Route("{key}")]
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
    }
}
