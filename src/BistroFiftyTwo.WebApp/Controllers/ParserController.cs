using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BistroFiftyTwo.WebApp.Controllers
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
            var parsedRecipe = RecipeService.ParseFull(input);
            return Ok(parsedRecipe);
        }

 
    }

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
#if DEBUG
            context.Result = new JsonResult(new { Message = exception.Message, StackTrace = exception.StackTrace });
#else
            context.Result = new JsonResult(exception.Message);
#endif
        }
    }
}