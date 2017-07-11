using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Parser")]
    public class ParserController : Controller
    {
        private IRecipeService RecipeService { get; set; }
        public ParserController(IRecipeService recipeService) { RecipeService = recipeService; }

        [HttpPost, Route("standard")]
        public async Task<IActionResult> ParseStandardRecipe([FromBody]string input)
        {
           
            var parsedRecipe = await RecipeService.Parse(input);
            return Ok(parsedRecipe);
        }

 
    }
}