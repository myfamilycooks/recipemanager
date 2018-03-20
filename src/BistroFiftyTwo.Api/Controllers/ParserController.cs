using System.IO;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/parser")]
    [CustomExceptionFilter]
    public class ParserController : Controller
    {
        private IRecipeService RecipeService { get; set; }
        public ParserController(IRecipeService recipeService) { RecipeService = recipeService; }

        [HttpPost, Route("standard")]
        public async Task<IActionResult> ParseStandardRecipe()
        {
            var input = await new StreamReader(Request.Body).ReadToEndAsync();
            var parsedRecipe = await RecipeService.ParseFull(input);
            return Ok(parsedRecipe);
        }

        [HttpPost, Route("simple")]
        public async Task<IActionResult> ParseSimpleRecipe()
        {
            var input = await new StreamReader(Request.Body).ReadToEndAsync();
            var parsedRecipe = await RecipeService.ParseFull(input);
            return Ok(parsedRecipe.Output);
        }

    }
}